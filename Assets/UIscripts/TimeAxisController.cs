using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeAxisController : MonoBehaviour {
	private Toggle isTimeAxisEnabled;
	private Slider timeAxisSlider;
	private Text timeStopped;
	private Text timeLeft;
	private PlanetController planetController;
	private GameObject gameEnd;
	
	void Start () {
		isTimeAxisEnabled = GameObject.Find("Toggle").GetComponent<Toggle>();
		timeAxisSlider = GameObject.Find("Slider").GetComponent<Slider>();
		timeStopped = GameObject.Find("TimeStopped").GetComponent<Text>();
		timeLeft = GameObject.Find("TimeLeft").GetComponent<Text>();

		timeLeft.text = (StellarSystem.GameDuration/60).ToString() + ":00";

		isTimeAxisEnabled.onValueChanged.AddListener(delegate{toggleChange();});
		timeAxisSlider.onValueChanged.AddListener(delegate{sliderChange();});
		timeAxisSlider.value = 0;
		timeStopped.enabled = false;
		timeLeft.enabled = false;
		timeAxisSlider.gameObject.SetActive(false);
		planetController = GameObject.Find("SystemOrigin").GetComponent<PlanetController>();
		gameEnd =  GameObject.Find("GameEnd");
		gameEnd.SetActive(false);
		StartCoroutine(startTime());
		for(int i = 0; i < planetController.System.Planets.Count; i++) {
			StartCoroutine(planetController.AnimateOrbit(i));
		}
	}
	
	void toggleChange() {
		if(isTimeAxisEnabled.isOn) {
			TimeSpan time =  TimeSpan.FromSeconds(StellarSystem.GameTime);
			timeStopped.text = time.Minutes.ToString("00") + ':' + time.Seconds.ToString("00");
			timeStopped.enabled = true;
			timeLeft.enabled = true;
			timeAxisSlider.gameObject.SetActive(true);
		} else {
			timeAxisSlider.value = 0;
			timeStopped.enabled = false;
			timeLeft.enabled = false;
			timeAxisSlider.gameObject.SetActive(false);
		}
	}
	void sliderChange() {
		
	}

	private IEnumerator startTime() {
		while(StellarSystem.GameTime < StellarSystem.GameDuration)
		{
			StellarSystem.GameTime += Time.deltaTime;
			yield return null;
		}
		timeAxisSlider.value = 0;
		timeStopped.enabled = false;
		timeLeft.enabled = false;
		timeAxisSlider.gameObject.SetActive(false);
		gameEnd.SetActive(true);
	}
}
