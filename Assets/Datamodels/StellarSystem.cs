using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StellarSystem {

	public List<Ellipse> OrbitalPaths;
	public List<Transform> PlanetTransforms;
	public List<Planet> Planets;

	public StellarSystem(SystemCreator creator, GameObject origin) {
		this.OrbitalPaths = new List<Ellipse>();
		this.Planets = new List<Planet>();
		this.PlanetTransforms = new List<Transform>();

		System.Random randomizer = new System.Random();
		for(int i = 0; i < creator.PlanetQuantity; i++) {
			Planets.Add(new Planet(i, randomizer));

			OrbitProvider orbitProvider = creator.Orbits[i].GetComponent<OrbitProvider>();
			Ellipse orbitPath = orbitProvider.OrbitShape;
			this.OrbitalPaths.Add(orbitPath);

			GameObject planet = GameObject.CreatePrimitive(PrimitiveType.Sphere);
			planet.name = "Planet";
			planet.transform.localScale = new Vector3(this.Planets[i].Size,this.Planets[i].Size,this.Planets[i].Size);

			this.PlanetTransforms.Add(planet.transform);
			this.PlanetTransforms[i].parent = creator.Orbits[i].transform;
		}
	}
}
