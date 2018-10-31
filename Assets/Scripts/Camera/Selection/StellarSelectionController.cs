using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StellarSelectionController : MonoBehaviour
{
    private RaycastHit hit;
    private Ray ray;
    private Transform systemOriginTransform;
    
    void Start()
    {
        gameObject.transform.parent = systemOriginTransform = GameObject.Find("SystemOrigin").transform;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            StellarCameraMovement.IsLocked = true;
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit)) {
                gameObject.transform.SetParent(hit.transform, false);
            }
            else
                gameObject.transform.SetParent(systemOriginTransform, false);
            StellarCameraMovement.IsLocked = false;
        }
    }
}
