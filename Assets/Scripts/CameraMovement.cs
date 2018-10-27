using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
  public static bool IsLocked = false;
  private GameObject dummyCamera;
  private float mouseRotationX, mouseRotationY, zoom;
  
  void Start()
  {
    if (StellarSystem.IsStarted)
    {
      transform.position = SystemCreator.EditorCameraPosition;
      transform.rotation = SystemCreator.EditorCameraRotation;
    }
  }
  
  void Update()
  {
    if (!IsLocked)
    {
      if (Input.GetMouseButton(2))
      {
        dummyCamera = Instantiate(transform.gameObject);
        Transform dummyTransform = dummyCamera.transform;
        mouseRotationX = Input.GetAxis("Mouse X");
        dummyTransform.Translate(Vector3.right * -mouseRotationX * 2);

        mouseRotationY = Input.GetAxis("Mouse Y");
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
      if ((Input.GetAxis("Mouse ScrollWheel") > 0 && Vector3.Distance(transform.localPosition, new Vector3(0, 0, 0)) > 10f) ||
      (Input.GetAxis("Mouse ScrollWheel") < 0 && Vector3.Distance(transform.localPosition, new Vector3(0, 0, 0)) < 200f))
      {
        transform.localPosition += transform.forward * Input.mouseScrollDelta.y * 2f;
      }
    }
  }
}
