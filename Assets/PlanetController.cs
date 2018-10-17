using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetController : MonoBehaviour {

	[Range(1,5)]
	public int GameSpeed = 1;
	private GameObject systemOrigin;
	private SystemCreator systemCreator;
	private StellarSystem system;
	private GameObject prefab;

	// Use this for initialization
	void Awake () {
		systemOrigin = GameObject.Find("SystemOrigin");
		systemCreator = systemOrigin.GetComponent<SystemCreator>();

		system = new StellarSystem(systemCreator, systemOrigin);

		for(int i = 0; i < systemCreator.PlanetQuantity; i++)
			StartCoroutine("AnimateOrbit",i);
	}

	public void SetPosition(Ellipse orbitalPath, Transform planetTransform, float progress) {
		Vector2 position = orbitalPath.Evaluate(progress);
		planetTransform.localPosition = new Vector3(position.x, position.y,0);
	}

	IEnumerator AnimateOrbit(int index) {
		if(system.OrbitalPeriods[index] < 0.1f) {
			system.OrbitalPeriods[index] = 0.1f;
		}
		while(true)
		{
			float orbitalSpeed = 1f / system.OrbitalPeriods[index] * GameSpeed/100;
			system.PlanetProgresses[index] += Time.deltaTime * orbitalSpeed;
			system.PlanetProgresses[index] %= 1f;
			SetPosition(system.OrbitalPaths[index], system.PlanetTransforms[index], system.PlanetProgresses[index]);
			yield return null;
		}
	}
}
