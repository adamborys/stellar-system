using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionController : MonoBehaviour
{
    private Vector3 translation;
    private RaycastHit hit;
    private Ray ray;
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit)) {
                translation = hit.transform.position - CameraMovement.ObservedTransform.position;
                Camera.main.transform.Translate(translation);
                CameraMovement.ObservedTransform = hit.transform;
                Camera.main.transform.LookAt(CameraMovement.ObservedTransform); // do przeniesienia do CameraMovement 
                CameraMovement.Offset = new Vector3();
            }
        }
    }
}
