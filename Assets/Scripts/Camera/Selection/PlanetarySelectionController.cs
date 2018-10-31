using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetarySelectionController : MonoBehaviour
{
  private RaycastHit hit;
  private Ray ray;
  private Transform planetTransform;

  // Zmiana konfiguracji siatki jest zależna od poruszania się rodzica kamery
  // który de facto ogranicza zmianę jej pozycji w grze
  private GridController gridRenderer;
  private int offsetX = 0, offsetZ = 0;

  void Start()
  {
    transform.parent = planetTransform = GameObject.Find("Planet").transform;
    gridRenderer = GameObject.Find("Grid").GetComponent<GridController>();
    gridRenderer.AdjustGrid(transform.parent.position, offsetX, offsetZ);
  }

  void Update()
  {
    if (Input.GetMouseButtonDown(0))
    {
      PlanetaryCameraMovement.IsLocked = true;
      ray = Camera.main.ScreenPointToRay(Input.mousePosition);

      if (Physics.Raycast(ray, out hit))
        transform.SetParent(hit.transform, false);
      else
        transform.SetParent(planetTransform, false);

      PlanetaryCameraMovement.IsLocked = false;
    }
    if (transform.parent.hasChanged)
    {
        if(offsetX - Mathf.RoundToInt(transform.parent.position.x) != 0 || offsetZ - Mathf.RoundToInt(transform.parent.position.z) != 0)
        {
            offsetX = Mathf.RoundToInt(transform.parent.position.x);
            offsetZ = Mathf.RoundToInt(transform.parent.position.z);
            gridRenderer.AdjustGrid(transform.parent.position, offsetX, offsetZ);
        }
        transform.parent.hasChanged = false;
    }
  }
}
