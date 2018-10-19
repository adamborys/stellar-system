﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Planet {
	public float Scale;
	public float OrbitalPeriod;
	public float OrbitalProgress;

	public Planet(int orbitIndex, System.Random randomizer) {
		this.Scale = (float)(Mathf.Sqrt(orbitIndex+1) * (0.6-(randomizer.NextDouble()/2)));
		this.OrbitalPeriod = (float)(Scale * Mathf.Sqrt((orbitIndex+1)));
		this.OrbitalProgress = (float)randomizer.NextDouble();
	}
}
