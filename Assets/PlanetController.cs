using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetController : MonoBehaviour {

	[Range(0,100)]
	public float GameSpeed = 1;
	
	private GameObject systemOrigin;
	private SystemCreator systemCreator;
	private StellarSystem system;

	private GameObject prefab;

	// Use this for initialization
	void Awake () {
		systemOrigin = GameObject.Find("SystemOrigin");
		systemCreator = systemOrigin.GetComponent<SystemCreator>();

		system = new StellarSystem(systemCreator, systemOrigin);

		for(int i = 0; i < systemCreator.PlanetQuantity; i++) {
			SetPosition(system.OrbitalPaths[i], system.PlanetTransforms[i], system.Planets[i].OrbitalProgress);
			StartCoroutine("AnimateOrbit",i);
		}
	}

	public void SetPosition(Ellipse orbitalPath, Transform planetTransform, float progress) {
		Vector2 position = orbitalPath.Evaluate(progress);
		planetTransform.localPosition = new Vector3(position.x, position.y,0);
	}

	private IEnumerator AnimateOrbit(int index) {
		if(system.Planets[index].OrbitalPeriod < 0.1f) {
			system.Planets[index].OrbitalPeriod = 0.1f;
		}
		while(true)
		{
			float distanceFromStar = Vector3.Distance(system.PlanetTransforms[index].position, new Vector3());
			float orbitalSpeed = (GameSpeed/100) * ((index+1) / (system.Planets[index].OrbitalPeriod * distanceFromStar));
			system.Planets[index].OrbitalProgress += Time.deltaTime * orbitalSpeed;
			system.Planets[index].OrbitalProgress %= 1f;
			SetPosition(system.OrbitalPaths[index], system.PlanetTransforms[index], system.Planets[index].OrbitalProgress);
			yield return null;
		}
	}
}