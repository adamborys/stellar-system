using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionController : MonoBehaviour
{
    void Update()
    {
       
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        if (Input.GetMouseButton(0) && Physics.Raycast(ray, out hit)) {
            Debug.Log(hit.transform.gameObject.name);
            Camera.main.transform.LookAt(hit.transform);
        } 
    }
}
