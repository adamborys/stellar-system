using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetaryCameraMovement : MonoBehaviour
{
  public static bool IsLocked = false;
  private GameObject dummyCamera;
  private GameObject planet;
  private GameObject shipyard;
  private float distance;
  private GameObject detailedGrid, grid;
  
  void Start()
  {
    if (StellarSystem.IsStarted)
    {
      transform.position = SystemCreator.EditorCameraPosition;
      transform.rotation = SystemCreator.EditorCameraRotation;
    }
    transform.SetParent(GameObject.Find("Planet").transform);
    detailedGrid = GameObject.Find("Detailed Grid");
    grid = GameObject.Find("Grid");
    planet = GameObject.Find("Planet");
    shipyard = GameObject.Find("Shipyard");

    detailedGrid.SetActive(false);
  }
  
  void Update()
  {
    shipyard.transform.RotateAround(new Vector3(), Vector3.up, Time.deltaTime);
  }

  void LateUpdate()
  {

    if (!IsLocked)
    {
      if (Input.GetMouseButton(2))
      {
        dummyCamera = Instantiate(transform.gameObject);
        Transform dummyTransform = dummyCamera.transform;
        dummyTransform.SetParent(transform.parent, false);
        dummyTransform.RotateAround(transform.parent.position, Vector3.up, Input.GetAxis("Mouse X") * 2f);
        dummyTransform.RotateAround(transform.parent.position, dummyTransform.right, Input.GetAxis("Mouse Y") * -2f);
        dummyTransform.rotation = Quaternion.Euler(dummyTransform.rotation.eulerAngles.x, dummyTransform.rotation.eulerAngles.y, 0f);
        
        float x = dummyTransform.rotation.eulerAngles.x;
        if((0 <= x && x <= 70) || (290 <= x && x < 360))
        {
          transform.LookAt(transform.parent);
          transform.localPosition = dummyTransform.localPosition;
          transform.localRotation = dummyTransform.localRotation;
        }
        Destroy(dummyCamera);
      }
      float scroll = Input.GetAxis("Mouse ScrollWheel");
      distance = Vector3.Magnitude(transform.localPosition);
      if (transform.parent.gameObject == planet) distance = Mathf.Pow(distance, 2);
      if ((scroll > 0 && distance > 1f) || 
          (scroll < 0 && distance < 10f))
      {
        transform.localPosition -= 0.1f * transform.localPosition * Input.mouseScrollDelta.y;
      }
    }

    if(transform.position.y < 100 && grid.activeSelf)
    {
      detailedGrid.SetActive(true);
      grid.SetActive(false);
    }
    else if(transform.position.y > 100 && detailedGrid.activeSelf)
    {
      detailedGrid.SetActive(false);
      grid.SetActive(true);
    }
  }
}
