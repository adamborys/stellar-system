using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetaryCameraMovement : MonoBehaviour
{
  public static bool IsLocked = false;
  private GameObject dummyCamera;
  private GameObject SPHERE;
  private float distance;
  private GridController gridRenderer;
  
  void Start()
  {
    if (StellarSystem.IsStarted)
    {
      transform.position = SystemCreator.EditorCameraPosition;
      transform.rotation = SystemCreator.EditorCameraRotation;
    }
    transform.SetParent(GameObject.Find("Planet").transform);
    gridRenderer = GameObject.Find("Grid").GetComponent<GridController>();
    SPHERE = GameObject.Find("Sphere");
  }
  
  void Update()
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
      distance = Vector3.Distance(transform.localPosition, transform.parent.position);
      if ((scroll > 0 && distance > 5f) || (scroll < 0 && distance < 50f))
      {
        transform.localPosition += transform.forward * Input.mouseScrollDelta.y;
      }
    }
    if (transform.position.z > 100);
    SPHERE.transform.RotateAround(new Vector3(), Vector3.up, 10 * Time.deltaTime);
  }
}
