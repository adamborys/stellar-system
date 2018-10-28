using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetarySelectionController : MonoBehaviour
{
    private RaycastHit hit;
    private Ray ray;
    private Transform planetTransform;
    
    void Start()
    {
        gameObject.transform.parent = planetTransform = GameObject.Find("Planet").transform;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            CameraStellarMovement.IsLocked = true;
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit)) {
                gameObject.transform.SetParent(hit.transform, false);
            }
            else
                gameObject.transform.SetParent(planetTransform, false);
            CameraStellarMovement.IsLocked = false;
        }
    }
}
