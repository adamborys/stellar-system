using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetController : MonoBehaviour {

	private GameObject system;
	private SystemCreator systemCreator;
	private List<Ellipse> orbitalPaths;
	private List<Transform> planetTransforms;
	private List<float> progresses;
	private List<float> periods;
	private GameObject prefab;

	// Use this for initialization
	void Awake () {
		system = GameObject.Find("StellarSystem");
		systemCreator = system.GetComponent<SystemCreator>();

		orbitalPaths = new List<Ellipse>();
		planetTransforms = new List<Transform>();
		progresses = new List<float>();
		periods = new List<float>();

		System.Random rand = new System.Random();
		for(int i = 0; i < systemCreator.PlanetQuantity; i++) {
			progresses.Add((float)rand.NextDouble());
		}
		
		prefab = Resources.Load("Prefabs/Planet") as GameObject;
		foreach(GameObject orbit in systemCreator.Orbits){
			OrbitProvider orbitProvider = orbit.GetComponent<OrbitProvider>();
			Ellipse orbitPath = orbitProvider.OrbitShape;
			orbitalPaths.Add(orbitPath);
			GameObject planet = (Instantiate(prefab) as GameObject);
			planet.name = "Planet";
			planetTransforms.Add(planet.transform);
		}

		for(int i = 0; i < orbitalPaths.Count; i++) {
			planetTransforms[i].parent = systemCreator.Orbits[i].transform;
			SetPosition(orbitalPaths[i], planetTransforms[i], progresses[i]);
		}
	}
	
	void Update () {
		for(int i = 0; i < orbitalPaths.Count; i++) {
			SetPosition(orbitalPaths[i], planetTransforms[i], progresses[i]);
		}
	}

	public void SetPosition(Ellipse orbitalPath, Transform planetTransform, float progress) {
		Vector2 position = orbitalPath.Evaluate(progress);
		planetTransform.localPosition = new Vector3(position.x, position.y,0);
	}
}
