using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetController : MonoBehaviour {

	private GameObject system;
	private SystemCreator systemCreator;
	private List<Ellipse> orbitalPaths;
	private List<Transform> planetTransforms;
	private List<float> planetSizes;
	private List<float> planetProgresses;
	private List<float> orbitalPeriods;
	private GameObject prefab;

	// Use this for initialization
	void Awake () {
		system = GameObject.Find("StellarSystem");
		systemCreator = system.GetComponent<SystemCreator>();

		orbitalPaths = new List<Ellipse>();
		planetTransforms = new List<Transform>();
		planetSizes = new List<float>();
		planetProgresses = new List<float>();
		orbitalPeriods = new List<float>();

		prefab = Resources.Load("Prefabs/Planet") as GameObject;

		System.Random rand = new System.Random();
		for(int i = 0; i < systemCreator.PlanetQuantity; i++) {
			planetSizes.Add((float)(0.6-(rand.NextDouble()/2)));
			planetProgresses.Add((float)rand.NextDouble());
			orbitalPeriods.Add((float)(planetSizes[i] * 100 * Mathf.Sqrt((i+1))));
			
			OrbitProvider orbitProvider = systemCreator.Orbits[i].GetComponent<OrbitProvider>();
			Ellipse orbitPath = orbitProvider.OrbitShape;
			orbitalPaths.Add(orbitPath);
			GameObject planet = (Instantiate(prefab) as GameObject);
			planet.name = "Planet";
			planet.transform.localScale = new Vector3(planetSizes[i],planetSizes[i],planetSizes[i]);
			planetTransforms.Add(planet.transform);
			planetTransforms[i].parent = systemCreator.Orbits[i].transform;
			StartCoroutine("AnimateOrbit",i);
		}
	}
	
	void Update () {/*
		for(int i = 0; i < orbitalPaths.Count; i++) {
			SetPosition(orbitalPaths[i], planetTransforms[i], planetProgresses[i]);
		} */
	}

	public void SetPosition(Ellipse orbitalPath, Transform planetTransform, float progress) {
		Vector2 position = orbitalPath.Evaluate(progress);
		planetTransform.localPosition = new Vector3(position.x, position.y,0);
	}

	IEnumerator AnimateOrbit(int index) {
		if(orbitalPeriods[index] < 0.1f) {
			orbitalPeriods[index] = 0.1f;
		}
		while(true)
		{
			float orbitalSpeed = 1f / orbitalPeriods[index];
			planetProgresses[index] += Time.deltaTime * orbitalSpeed;
			planetProgresses[index] %= 1f;
			SetPosition(orbitalPaths[index], planetTransforms[index], planetProgresses[index]);
			yield return null;
		}
	}
}
