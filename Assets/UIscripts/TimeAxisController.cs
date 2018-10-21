using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeAxisController : MonoBehaviour {
	private Toggle isTimeAxisEnabled;
	private Slider timeAxisSlider;
	private Text timeStopped;
	private Text timeLeft;
	
	void Start () {
		isTimeAxisEnabled = GameObject.Find("Toggle").GetComponent<Toggle>();
		timeAxisSlider = GameObject.Find("Slider").GetComponent<Slider>();
		timeStopped = GameObject.Find("TimeStopped").GetComponent<Text>();
		timeLeft = GameObject.Find("TimeLeft").GetComponent<Text>();

		isTimeAxisEnabled.onValueChanged.AddListener(delegate{toggleChange();});
		timeAxisSlider.onValueChanged.AddListener(delegate{sliderChange();});
	}
	
	void toggleChange() {
		if(isTimeAxisEnabled.isOn) {

			timeAxisSlider.interactable = true;
		} else {
			timeAxisSlider.value = 0;
			timeAxisSlider.interactable = false;
		}
	}
	void sliderChange() {

	}
}
