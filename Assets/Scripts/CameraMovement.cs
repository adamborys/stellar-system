using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
  public static bool IsCameraLocked = false;
  public static Transform ObservedTransform;
  public static Vector3 Offset = new Vector3();
  private GameObject dummyCamera;
  private float mouseRotationX, mouseRotationY, zoom;
  
  void Start()
  {
    ObservedTransform = GameObject.Find("SystemOrigin").transform;
    Offset = ObservedTransform.position;
    transform.LookAt(ObservedTransform);
    if (StellarSystem.IsStarted)
    {
      transform.position = SystemCreator.EditorCameraPosition;
      transform.rotation = SystemCreator.EditorCameraRotation;
    }
  }
  
  void Update()
  {
    if (!IsCameraLocked)
    {
      if (Input.GetMouseButton(2))
      {
        dummyCamera = Instantiate(transform.gameObject);
        Transform dummyTransform = dummyCamera.transform;
        mouseRotationX = Input.GetAxis("Mouse X");
        dummyTransform.Translate(Vector3.right * -mouseRotationX * 2);

        mouseRotationY = Input.GetAxis("Mouse Y");
        dummyTransform.Translate(Vector3.up * -mouseRotationY * 2);
        dummyTransform.LookAt(ObservedTransform);
        
        float x = dummyTransform.rotation.eulerAngles.x;
        if((0 <= x && x <= 70) || (290 <= x && x < 360))
        {
          transform.position = dummyTransform.position;
          transform.rotation = dummyTransform.rotation;
        }
        Destroy(dummyCamera);
      }
      if ((Input.GetAxis("Mouse ScrollWheel") > 0 && Vector3.Distance(transform.position, new Vector3(0, 0, 0)) > 10f) ||
      (Input.GetAxis("Mouse ScrollWheel") < 0 && Vector3.Distance(transform.position, new Vector3(0, 0, 0)) < 200f))
      {
        transform.position += transform.forward * Input.mouseScrollDelta.y * 2f;
      }
    }
  }
}
