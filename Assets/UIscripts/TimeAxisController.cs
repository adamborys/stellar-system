﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeAxisController : MonoBehaviour
{
  private Toggle isTimeAxisEnabled;
  private Slider timeAxisSlider;
  private Text timeStopped;
  private Text timeLeft;
  private PlanetController planetController;
  private GameObject gameEnd;
  private float now;

  void Start()
  {
    isTimeAxisEnabled = GameObject.Find("Toggle").GetComponent<Toggle>();
    timeAxisSlider = GameObject.Find("Slider").GetComponent<Slider>();
    timeStopped = GameObject.Find("TimeStopped").GetComponent<Text>();
    timeLeft = GameObject.Find("TimeLeft").GetComponent<Text>();

    timeLeft.text = (StellarSystem.GameDuration / 60).ToString() + ":00";

    isTimeAxisEnabled.onValueChanged.AddListener(delegate { toggleChange(); });
    timeAxisSlider.onValueChanged.AddListener(delegate { sliderChange(); });
    timeAxisSlider.value = 0;
    timeStopped.enabled = false;
    timeLeft.enabled = false;
    timeAxisSlider.gameObject.SetActive(false);
    planetController = GameObject.Find("SystemOrigin").GetComponent<PlanetController>();
    gameEnd = GameObject.Find("GameEnd");
    gameEnd.SetActive(false);
    StartCoroutine(CountTime());
  }

  void toggleChange()
  {
    if (isTimeAxisEnabled.isOn)
    {
			now = StellarSystem.GameTime / StellarSystem.GameDuration;
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
    timeStopped.enabled = !timeStopped.enabled;
    timeLeft.enabled = !timeLeft.enabled;
    StellarSystem.IsPaused = !StellarSystem.IsPaused;
  }
  void sliderChange()
  {
    if (timeAxisSlider.value < now)
      timeAxisSlider.value = now;
		else if (StellarSystem.GameDuration == 900f && timeAxisSlider.value - now > 0.33)
      timeAxisSlider.value = now + 0.33f;
		else if (StellarSystem.GameDuration == 1800f && timeAxisSlider.value - now > 0.25)
      timeAxisSlider.value = now + 0.25f;

    float projectedTime = StellarSystem.GameDuration - (1 - timeAxisSlider.value) * StellarSystem.GameDuration;
    TimeSpan projectedTimeSpan = TimeSpan.FromSeconds(projectedTime);
    timeStopped.text = projectedTimeSpan.Minutes.ToString("00") + ':' + projectedTimeSpan.Seconds.ToString("00");

		planetController.PredictPlanetPositions(projectedTime);
  }
  private IEnumerator CountTime()
  {
    while (StellarSystem.GameTime < StellarSystem.GameDuration)
    {
      StellarSystem.GameTime = planetController.LastUpdate - planetController.StartTime;
      yield return new WaitForSeconds(1);
    }
    timeAxisSlider.value = 0;
    timeStopped.enabled = false;
    timeLeft.enabled = false;
    timeAxisSlider.gameObject.SetActive(false);
    gameEnd.SetActive(true);
  }
}
