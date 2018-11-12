using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeAxisController : MonoBehaviour
{
  private Toggle timeAxisToggle;
  private Slider timeAxisSlider;
  private Text timeStopped;
  private Text timeEnd;
  private PlanetController planetController;
  private GameObject gameEnd;
  private float startSliderValue;
  private float[] stoppedProgresses;

  void Start()
  {
    timeAxisToggle = GameObject.Find("Toggle").GetComponent<Toggle>();
    timeAxisSlider = GameObject.Find("Slider").GetComponent<Slider>();
    timeStopped = GameObject.Find("TimeStopped").GetComponent<Text>();
    timeEnd = GameObject.Find("TimeEnd").GetComponent<Text>();

    timeStopped.text = "00:01";
    timeEnd.text = (StellarSystem.GameDuration / 60).ToString() + ":00";

    timeAxisToggle.onValueChanged.AddListener(delegate { toggleChange(); });
    timeAxisSlider.onValueChanged.AddListener(delegate { sliderChange(); });
    timeAxisSlider.value = 0;
    timeEnd.enabled = false;
    timeAxisSlider.gameObject.SetActive(false);
    planetController = GameObject.Find("SystemOrigin").GetComponent<PlanetController>();
    gameEnd = GameObject.Find("GameEnd");
    gameEnd.SetActive(false);
    PlanetController.StartTime = Time.time;
    PlanetController.LastUpdate = Time.time;
    StellarSystem.IsStarted = true;
    StartCoroutine(CountTime());
  }

  void toggleChange()
  {
    if (timeAxisToggle.isOn)
    {
      startSliderValue = StellarSystem.GameTime / StellarSystem.GameDuration;
      stoppedProgresses = new float[StellarSystem.PlanetQuantity];

      for(int i = 0; i < StellarSystem.PlanetQuantity; i++) {
        stoppedProgresses[i] = planetController.System.PlanetProgresses[i];
      }

      TimeSpan time = TimeSpan.FromSeconds(StellarSystem.GameTime);
      timeStopped.text = time.Minutes.ToString("00") + ':' + time.Seconds.ToString("00");
      timeAxisSlider.value = StellarSystem.GameTime / StellarSystem.GameDuration;
      timeAxisSlider.gameObject.SetActive(true);
    }
    else
    {
      timeAxisSlider.value = 0;
      timeAxisSlider.gameObject.SetActive(false);
    }
    timeEnd.enabled = !timeEnd.enabled;
    StellarSystem.IsPaused = !StellarSystem.IsPaused;
  }
  void sliderChange()
  {
    if (timeAxisSlider.value < startSliderValue)
      timeAxisSlider.value = startSliderValue;/*
    else if (StellarSystem.GameDuration == 900f && timeAxisSlider.value - startSliderValue > 0.33)
      timeAxisSlider.value = startSliderValue + 0.33f;
    else if (StellarSystem.GameDuration == 1800f && timeAxisSlider.value - startSliderValue > 0.25)
      timeAxisSlider.value = startSliderValue + 0.25f; */

    float stopTime = StellarSystem.GameDuration * startSliderValue;
    float projectedTime = StellarSystem.GameDuration - (1 - timeAxisSlider.value) * StellarSystem.GameDuration;
    TimeSpan projectedTimeSpan = TimeSpan.FromSeconds(projectedTime);
    timeStopped.text = projectedTimeSpan.Minutes.ToString("00") + ':' + projectedTimeSpan.Seconds.ToString("00");

    planetController.PredictPlanetPositions(stopTime, projectedTime, stoppedProgresses);
  }
  private IEnumerator CountTime()
  {
    while (StellarSystem.GameTime < StellarSystem.GameDuration)
    {
      StellarSystem.GameTime = PlanetController.LastUpdate - PlanetController.StartTime;
      if (!StellarSystem.IsPaused)
      {
        TimeSpan gameTime = TimeSpan.FromSeconds(StellarSystem.GameTime);
        timeStopped.text = gameTime.Minutes.ToString("00") + ':' + gameTime.Seconds.ToString("00");
      }
      yield return new WaitForSeconds(1);
    }
    timeStopped.enabled = false;
    timeEnd.enabled = false;
    timeAxisSlider.gameObject.SetActive(false);
    gameEnd.SetActive(true);
  }
}
