using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StellarSystem {

	public List<Ellipse> OrbitalPaths;
	public List<Transform> PlanetTransforms;
	public List<float> PlanetSizes;
	public List<float> PlanetProgresses;
	public List<float> OrbitalPeriods;

	public StellarSystem(SystemCreator creator, GameObject origin) {
		this.OrbitalPaths = new List<Ellipse>();
		this.PlanetTransforms = new List<Transform>();
		this.PlanetSizes = new List<float>();
		this.PlanetProgresses = new List<float>();
		this.OrbitalPeriods = new List<float>();

		System.Random rand = new System.Random();
		for(int i = 0; i < creator.PlanetQuantity; i++) {
			this.PlanetSizes.Add((float)(0.6-(rand.NextDouble()/2)));
			this.PlanetProgresses.Add((float)rand.NextDouble());
			this.OrbitalPeriods.Add((float)(this.PlanetSizes[i] * Mathf.Sqrt((i+1))));
			
			OrbitProvider orbitProvider = creator.Orbits[i].GetComponent<OrbitProvider>();
			Ellipse orbitPath = orbitProvider.OrbitShape;
			this.OrbitalPaths.Add(orbitPath);
			GameObject planet = GameObject.CreatePrimitive(PrimitiveType.Sphere);
			planet.name = "Planet";
			planet.transform.localScale = new Vector3(this.PlanetSizes[i],this.PlanetSizes[i],this.PlanetSizes[i]);
			this.PlanetTransforms.Add(planet.transform);
			this.PlanetTransforms[i].parent = creator.Orbits[i].transform;
		}
	}
}
