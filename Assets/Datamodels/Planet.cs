using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Planet {
	public float Size;
	public float OrbitalPeriod;
	public float OrbitalProgress;

	public Planet(int orbitIndex, System.Random randomizer) {
		this.Size = (float)(Mathf.Sqrt(orbitIndex+1) * (0.6-(randomizer.NextDouble()/2)));
		this.OrbitalPeriod = (float)(Size * Mathf.Sqrt((orbitIndex+1)));
		this.OrbitalProgress = (float)randomizer.NextDouble();
	}
}
