﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetController : MonoBehaviour {

	[Range(0,100)]
	public float GameSpeed = 1;
	public StellarSystem System;
	
	private GameObject systemOrigin;

	private GameObject prefab;


	void Start () {
		systemOrigin = GameObject.Find("SystemOrigin");
	  SystemCreator systemCreator = systemOrigin.GetComponent<SystemCreator>();

		System = new StellarSystem(systemCreator, systemOrigin);

		for(int i = 0; i < systemCreator.PlanetQuantity; i++) {
			SetPosition(i, System.Planets[i].OrbitalProgress);
		}
	}

	public Vector3 GetPosition(int index, float progress) {
		Vector2 position = System.Orbits[index].OrbitShape.Evaluate(progress);
		return new Vector3(position.x, 0f, position.y);
	}

	public void SetPosition(int index, float progress) {
		Vector2 position = System.Orbits[index].OrbitShape.Evaluate(progress);
		System.PlanetTransforms[index].localPosition = new Vector3(position.x, 0f, position.y);
	}
	
	public IEnumerator AnimateOrbit(int index) {
		if(System.Planets[index].OrbitalPeriod < 0.01f) {
			System.Planets[index].OrbitalPeriod = 0.01f;
		}
		while(true)
		{
			float distanceFromStar = Vector3.Distance(System.PlanetTransforms[index].position, new Vector3());
			float orbitalSpeed = (GameSpeed/100) * ((index+1) / (System.Planets[index].OrbitalPeriod * distanceFromStar));
			System.Planets[index].OrbitalProgress += Time.deltaTime * orbitalSpeed;
			System.Planets[index].OrbitalProgress %= 1f;
			if(!StellarSystem.IsPaused)
				SetPosition(index, System.Planets[index].OrbitalProgress);
			yield return null;
		}
	}
}