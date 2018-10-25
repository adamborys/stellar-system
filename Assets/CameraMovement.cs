using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
  private Transform systemOriginTransform;
  private float mouseSpeedX, mouseSpeedY, zoom;
  // Start is called before the first frame update
  void Start()
  {
    systemOriginTransform = GameObject.Find("SystemOrigin").transform;
    transform.LookAt(systemOriginTransform);
    zoom = Camera.main.fieldOfView;
  }

  // Update is called once per frame
  void Update()
  {
    if (Input.GetMouseButton(2))
    {
      mouseSpeedX = Input.GetAxis("Mouse X");
      transform.Translate(Vector3.right * -mouseSpeedX * 2);
      mouseSpeedY = Input.GetAxis("Mouse Y");
      transform.Translate(Vector3.up * -mouseSpeedY * 2);
      transform.LookAt(systemOriginTransform);
    }
    if (Input.GetAxis("Mouse ScrollWheel") != 0)
    {
        zoom -= Input.mouseScrollDelta.y * 2;
        zoom = Mathf.Clamp (zoom, 5f, 100f);
        Camera.main.fieldOfView = zoom;
    }
  }
}
