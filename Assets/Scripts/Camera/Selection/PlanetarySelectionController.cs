using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlanetarySelectionController : MonoBehaviour
{
    public static GameObject Selection;
    private RaycastHit hit;
    private Ray ray;
    private GameObject planet;
    private Toggle camToggle;
    private Transform from;
    private int pointerID = -1;
    private void Awake()
    {
        // Checking platform for EventSystem.IsPointerOverGameObject()
        #if !UNITY_EDITOR
            pointerID = 0; 
        #endif
    }
    void Start()
    {
        planet = GameObject.Find("Planet");
        Selection = planet;
        camToggle = GameObject.Find("CamToggle").GetComponent<Toggle>();
        camToggle.isOn = false;

        from = GameObject.Find("Cam Parent").transform;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Fail-safe GUI blocking Raycast
            if (EventSystem.current.IsPointerOverGameObject(pointerID))
            {
                Debug.Log("GUI Hit!");
                return;
            }
            
            // Raycast interactable selection
            PlanetaryCameraMovement.IsLocked = true;
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                Selection = hit.transform.gameObject;
                float distance = Vector3.Magnitude(transform.localPosition);
                if(Selection == planet)
                    transform.localPosition =
                        Vector3.Normalize(transform.localPosition) * 500f;
                else
                    transform.localPosition =
                        Vector3.Normalize(transform.localPosition) * 25f;
            }
            PlanetaryCameraMovement.IsLocked = false;
        }
        if (!camToggle.isOn)
            clonePosition(Selection.transform, transform.parent);
    }

    private void clonePosition(Transform from, Transform to)
    {
        to.parent = from.parent;
        to.position = from.position;
        to.localPosition = from.localPosition;
    }
}
