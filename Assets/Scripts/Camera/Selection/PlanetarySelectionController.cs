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
    private Transform selection;
    private Toggle camToggle;
    private Transform freeCamParent;
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
        transform.parent = planet.transform;
        Selection = planet.gameObject;
        camToggle = GameObject.Find("CamToggle").GetComponent<Toggle>();
        camToggle.isOn = false;
        camToggle.onValueChanged.AddListener(delegate { toggleChange(); });

        freeCamParent = GameObject.Find("Free Cam Parent").transform;
        freeCamCloneTransform(planet.transform);
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
                selection = hit.transform;
                if (!camToggle.isOn)
                {
                    transform.SetParent(selection, false);
                }
            }
            PlanetaryCameraMovement.IsLocked = false;
        }
    }

    private void toggleChange()
    {
        if (camToggle.isOn)
        {
            freeCamCloneTransform(transform.parent);
            transform.SetParent(freeCamParent, false);
        }
        else
        {
            transform.SetParent(selection, false);
        }
    }

    private void freeCamCloneTransform(Transform newTransform)
    {
        freeCamParent.parent = newTransform.parent;
        freeCamParent.position = newTransform.position;
        freeCamParent.localPosition = newTransform.localPosition;
        freeCamParent.rotation = newTransform.rotation;
        freeCamParent.localRotation = newTransform.localRotation;
        freeCamParent.localScale = new Vector3(25f, 25f, 25f);
    }
}
