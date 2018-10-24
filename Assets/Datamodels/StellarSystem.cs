using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Collections;

[System.Serializable]
public class StellarSystem
{

  public List<OrbitProvider> Orbits;
  public List<Planet> Planets;
  public List<Transform> PlanetTransforms;
  public NativeList<float> PlanetProgresses;
  public static float GameSpeed = 0;
  public static float GameTime = 0;
  public static float GameDuration = 900f;
  public static bool IsStarted = false;
  public static bool IsPaused = false;

  public StellarSystem(SystemCreator creator, GameObject origin)
  {
    this.Orbits = new List<OrbitProvider>();
    this.Planets = new List<Planet>();
    this.PlanetTransforms = new List<Transform>();
    this.PlanetProgresses = new NativeList<float>(creator.PlanetQuantity, Allocator.Persistent);

    System.Random randomizer = new System.Random();
    for (int i = 0; i < creator.PlanetQuantity; i++)
    {
      Planets.Add(new Planet(i, randomizer));

      this.Orbits.Add(creator.Orbits[i].GetComponent<OrbitProvider>());

      GameObject planet = GameObject.CreatePrimitive(PrimitiveType.Sphere);
      planet.name = "Planet";
      planet.transform.localScale = new Vector3(this.Planets[i].Scale, this.Planets[i].Scale, this.Planets[i].Scale);

      this.PlanetTransforms.Add(planet.transform);
      this.PlanetTransforms[i].parent = creator.Orbits[i].transform;
      this.PlanetProgresses.Add((float)randomizer.NextDouble());
    }
  }

  ~StellarSystem(){
    this.PlanetProgresses.Dispose();
  }

  public float GetDistanceBetween(Transform firstPlanet, Transform secondPlanet)
  {
    return Vector3.Distance(firstPlanet.position, secondPlanet.position);
  }
}
