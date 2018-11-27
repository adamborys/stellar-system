using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlanetarySelectionController : MonoBehaviour
{
    public static GameObject Selection;
    public static GameObject Planet;
    private RaycastHit hit;
    private Ray ray;
    private Toggle camToggle;
    private Text selectionText;
    private SelectionIndicator selectionIndicator;
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
        Planet = GameObject.Find("Planet");
        Selection = Planet;
        camToggle = GameObject.Find("Cam Toggle").GetComponent<Toggle>();
        selectionText = GameObject.Find("Selection Text").GetComponent<Text>();
        camToggle.isOn = false;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Fail-safe GUI blocking Raycast
            if (EventSystem.current.IsPointerOverGameObject(pointerID))
            {
                return;
            }
            
            // Raycast interactable selection
            PlanetaryCameraMovement.IsLocked = true;
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform == Selection) // IMPROVE AT MULTIPLE SELECTION!
                {
                    if (camToggle.isOn)
                        clonePosition(hit.transform, transform.parent);
                }
                else
                    Selection = hit.transform.gameObject;
                selectionText.text = "Selection:" + Selection.gameObject.name;
                if(!camToggle.isOn)
                {
                    if(isPlanetSelected())
                        transform.localPosition =
                            Vector3.Normalize(transform.localPosition) * 500;
                    else
                        transform.localPosition =
                            Vector3.Normalize(transform.localPosition) * 25;
                }
            }
            PlanetaryCameraMovement.IsLocked = false;
        }
        if (!camToggle.isOn) // IMPROVE AT MULTIPLE SELECTION!
            clonePosition(Selection.transform, transform.parent);
    }
    public static bool isPlanetSelected()
    {
        return Selection == Planet;
    }

    private void clonePosition(Transform from, Transform to)
    {
        to.parent = from.parent;
        to.position = from.position;
        to.localPosition = from.localPosition;
    }
}
