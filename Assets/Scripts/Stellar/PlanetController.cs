using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetController : MonoBehaviour
{

  [Range(0, 100)]
  public StellarSystem System;
  public float GameSpeed = 1;
  public static float StartTime;
  public static float LastUpdate;
  public static int FixedFramesSinceStart;

  private GameObject systemOrigin;
  private float now;


  void Start()
  {
    systemOrigin = GameObject.Find("SystemOrigin");
    SystemCreator systemCreator = systemOrigin.GetComponent<SystemCreator>();

    System = new StellarSystem(systemCreator, systemOrigin);

    for (int i = 0; i < StellarSystem.PlanetQuantity; i++)
    {
      if (System.Planets[i].OrbitalPeriod < 0.01f)
      {
        System.Planets[i].OrbitalPeriod = 0.01f;
      }
      SetPosition(i, System.PlanetProgresses[i]);
    }
  }

  void Update()
  {
    if (!StellarSystem.IsPaused)
    {
      for (int i = 0; i < StellarSystem.PlanetQuantity; i++)
      {
        SetPosition(i, System.PlanetProgresses[i]);
      }
    }
  }

  void FixedUpdate()
  {
    if (StellarSystem.IsStarted)
    {
      UpdatePlanetPositions();
      FixedFramesSinceStart++;
      LastUpdate = Time.time;
    }
  }

  public Vector3 GetPosition(int index, float progress)
  {
    Vector2 position = System.Orbits[index].OrbitShape.Evaluate(progress);
    return new Vector3(position.x, 0, position.y);
  }

  public void SetPosition(int index, float progress)
  {
    Vector2 position = System.Orbits[index].OrbitShape.Evaluate(progress);
    System.PlanetTransforms[index].localPosition = new Vector3(position.x, 0, position.y);
  }

  public void UpdatePlanetPositions()
  {
    for (int i = 0; i < StellarSystem.PlanetQuantity; i++)
    {
      float distanceFromStar = Vector3.Distance(System.PlanetTransforms[i].position, Vector3.zero);
      float orbitalSpeed = (GameSpeed / 100) * ((i + 1) / (System.Planets[i].OrbitalPeriod * distanceFromStar));
      System.PlanetProgresses[i] += Time.fixedDeltaTime * orbitalSpeed;
      System.PlanetProgresses[i] %= 1;
    }
  }
  public void PredictPlanetPositions(float startTime, float targetTime, float[] stoppedProgresses)
  {
    float[] predictedProgresses = new float[StellarSystem.PlanetQuantity];
    int iterations = (int)((targetTime-startTime)*40);
    for (int i = 0; i < StellarSystem.PlanetQuantity; i++)
    {
      predictedProgresses[i] = stoppedProgresses[i];
      for (float j = 0; j < iterations; j++)
      {
        float distanceFromStar = Vector3.Distance(GetPosition(i, predictedProgresses[i]), new Vector3());
        float orbitalSpeed = (GameSpeed / 100) * ((i + 1) / (System.Planets[i].OrbitalPeriod * distanceFromStar));
        predictedProgresses[i] += Time.fixedDeltaTime * orbitalSpeed;
        predictedProgresses[i] %= 1;
      }
      SetPosition(i, predictedProgresses[i]);
    }
  }
}