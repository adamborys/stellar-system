using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Collections;

public class PlanetController : MonoBehaviour
{

  [Range(0, 100)]
  public StellarSystem System;
  public float GameSpeed = 1;
  public static float StartTime;
  public static float LastUpdate;
  public NativeList<float> dupa;

  private GameObject systemOrigin;
  private float now;
  private const float timeQuantum = 0.025f;


  void Start()
  {
    systemOrigin = GameObject.Find("SystemOrigin");
    SystemCreator systemCreator = systemOrigin.GetComponent<SystemCreator>();

    System = new StellarSystem(systemCreator, systemOrigin);

    for (int i = 0; i < systemCreator.PlanetQuantity; i++)
    {
      if (System.Planets[i].OrbitalPeriod < 0.01f)
      {
        System.Planets[i].OrbitalPeriod = 0.01f;
      }
      SetPosition(i, System.Planets[i].OrbitalProgress);
    }
  }

  void Update()
  {
    if (StellarSystem.IsStarted && !StellarSystem.IsPaused && (now = Time.time) - LastUpdate > timeQuantum)
    {
      UpdatePlanetPositions(now);
      LastUpdate = Time.time;
    }
  }

  public Vector3 GetPosition(int index, float progress)
  {
    Vector2 position = System.Orbits[index].OrbitShape.Evaluate(progress);
    return new Vector3(position.x, 0f, position.y);
  }

  public void SetPosition(int index, float progress)
  {
    Vector2 position = System.Orbits[index].OrbitShape.Evaluate(progress);
    System.PlanetTransforms[index].localPosition = new Vector3(position.x, 0f, position.y);
  }

  public void UpdatePlanetPositions(float targetTime)
  {
    for (int i = 0; i < System.Planets.Count; i++) {
      for (float time = LastUpdate; time < targetTime; time += timeQuantum)
      {
        float distanceFromStar = Vector3.Distance(System.PlanetTransforms[i].position, new Vector3());
        float orbitalSpeed = (GameSpeed / 100) * ((i + 1) / (System.Planets[i].OrbitalPeriod * distanceFromStar));
        System.Planets[i].OrbitalProgress += timeQuantum * orbitalSpeed;
        System.Planets[i].OrbitalProgress %= 1f;
      }
      SetPosition(i, System.Planets[i].OrbitalProgress);
		}
  }
  public void PredictPlanetPositions(float targetTime)
  {
    float[] predictedProgress = new float[System.Planets.Count];
    for (int i = 0; i < System.Planets.Count; i++)
    {
      predictedProgress[i] = System.Planets[i].OrbitalProgress;
      for (float time = LastUpdate; time < targetTime; time += timeQuantum)
      {
        float distanceFromStar = Vector3.Distance(GetPosition(i, predictedProgress[i]), new Vector3());
        float orbitalSpeed = (GameSpeed / 100) * ((i + 1) / (System.Planets[i].OrbitalPeriod * distanceFromStar));
        predictedProgress[i] += timeQuantum * orbitalSpeed;
        predictedProgress[i] %= 1f;
      }
      SetPosition(i, predictedProgress[i]);
    }
  }
}