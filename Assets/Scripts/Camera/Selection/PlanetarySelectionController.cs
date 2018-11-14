using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetarySelectionController : MonoBehaviour
{
  public static GameObject Selection; 
  private RaycastHit hit;
  private Ray ray;
  private GameObject planet;

  void Start()
  {
    planet = GameObject.Find("Planet");
    transform.parent = planet.transform;
    Selection = planet.gameObject;
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
        transform.SetParent(planet.transform, false);

      //float distance = Vector3.Magnitude(transform.localPosition);
      
      
      PlanetaryCameraMovement.IsLocked = false;
    }
  }
}
