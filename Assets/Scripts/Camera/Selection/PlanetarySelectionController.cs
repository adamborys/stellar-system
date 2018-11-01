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
    transform.parent = planetTransform = GameObject.Find("Planet").transform;
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
  }
}
