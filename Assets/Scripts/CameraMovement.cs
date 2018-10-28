using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
  public static bool IsLocked = false;
  private GameObject dummyCamera;
  
  void Start()
  {
    if (StellarSystem.IsStarted)
    {
      transform.position = SystemCreator.EditorCameraPosition;
      transform.rotation = SystemCreator.EditorCameraRotation;
    }
    transform.SetParent(GameObject.Find("SystemOrigin").transform);
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
        float mouseRotationX = Input.GetAxis("Mouse X");
        dummyTransform.Translate(Vector3.right * -mouseRotationX * 2);

        float mouseRotationY = Input.GetAxis("Mouse Y");
        dummyTransform.Translate(Vector3.up * -mouseRotationY * 2);
        dummyTransform.LookAt(transform.parent);
        
        float x = dummyTransform.localRotation.eulerAngles.x;
        if((0 <= x && x <= 70) || (290 <= x && x < 360))
        {
          transform.localPosition = dummyTransform.localPosition;
          transform.localRotation = dummyTransform.localRotation;
        }
        Destroy(dummyCamera);
      }
      float scroll = Input.GetAxis("Mouse ScrollWheel");
      if ((scroll > 0 && Vector3.Distance(transform.localPosition, transform.parent.position) > 10f) ||
      (scroll < 0 && Vector3.Distance(transform.localPosition, transform.parent.position) < 200f))
      {
        transform.localPosition += transform.forward * Input.mouseScrollDelta.y * 2f;
      }
    }
  }
}
