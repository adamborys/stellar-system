using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetController : MonoBehaviour {

	[Range(0,100)]
	public float GameSpeed = 1;
	public StellarSystem System;
	
	private GameObject systemOrigin;
	private SystemCreator systemCreator;

	private GameObject prefab;


	void Start () {
		systemOrigin = GameObject.Find("SystemOrigin");
		systemCreator = systemOrigin.GetComponent<SystemCreator>();

		System = new StellarSystem(systemCreator, systemOrigin);

		for(int i = 0; i < systemCreator.PlanetQuantity; i++) {
			SetPosition(System.Orbits[i].OrbitShape, System.PlanetTransforms[i], System.Planets[i].OrbitalProgress);
		}
	}

	public void SetPosition(Ellipse orbitalPath, Transform planetTransform, float progress) {
		Vector2 position = orbitalPath.Evaluate(progress);
		planetTransform.localPosition = new Vector3(position.x, 0f, position.y);
	}
	
	public IEnumerator AnimateOrbit(int index) {
		if(System.Planets[index].OrbitalPeriod < 0.1f) {
			System.Planets[index].OrbitalPeriod = 0.1f;
		}
		while(true)
		{
			float distanceFromStar = Vector3.Distance(System.PlanetTransforms[index].position, new Vector3());
			float orbitalSpeed = (GameSpeed/100) * ((index+1) / (System.Planets[index].OrbitalPeriod * distanceFromStar));
			System.Planets[index].OrbitalProgress += Time.deltaTime * orbitalSpeed;
			System.Planets[index].OrbitalProgress %= 1f;
			SetPosition(System.Orbits[index].OrbitShape, System.PlanetTransforms[index], System.Planets[index].OrbitalProgress);
			yield return null;
		}
	}
}