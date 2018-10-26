using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
  private Transform systemOriginTransform;
  private Transform dummyTransform;
  private float mouseSpeedX, mouseSpeedY, zoom;
  private Vector3 previous;
  // Start is called before the first frame update
  void Start()
  {
    systemOriginTransform = GameObject.Find("SystemOrigin").transform;
    transform.LookAt(systemOriginTransform);
    if (StellarSystem.IsStarted)
    {
      transform.position = SystemCreator.EditorCameraPosition;
      transform.rotation = SystemCreator.EditorCameraRotation;
    }
  }

  // Update is called once per frame
  void Update()
  {
    if (Input.GetMouseButton(2))
    {
      mouseSpeedX = Input.GetAxis("Mouse X");
      transform.Translate(Vector3.right * -mouseSpeedX * 2);

      Vector3 temporaryPosition = transform.position;
      Quaternion temporaryRotation = transform.rotation;

      mouseSpeedY = Input.GetAxis("Mouse Y");
      transform.Translate(Vector3.up * -mouseSpeedY * 2);
      transform.LookAt(systemOriginTransform);
      if (transform.rotation.eulerAngles.x < 0 || 
      (transform.rotation.eulerAngles.x > 80 && transform.rotation.eulerAngles.x < 290))
      {
        transform.position = temporaryPosition;
        transform.rotation = temporaryRotation;
      }
      transform.LookAt(systemOriginTransform);
    }
    if (Input.GetAxis("Mouse ScrollWheel") > 0 && Vector3.Distance(transform.position, new Vector3(0, 0, 0)) > 10f)
    {
      transform.position += transform.forward * Input.mouseScrollDelta.y * 2f;
    }
    if (Input.GetAxis("Mouse ScrollWheel") < 0 && Vector3.Distance(transform.position, new Vector3(0, 0, 0)) < 200f)
    {
      transform.position += transform.forward * Input.mouseScrollDelta.y * 2f;
    }
  }
}
