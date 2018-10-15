using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetController : MonoBehaviour {

	[Range(0f,1f)]
	public float OrbitProgress;

	private GameObject system;
	private SystemCreator systemCreator;
	private List<Ellipse> orbitalPaths;
	private List<Transform> planetTransforms;
	private GameObject prefab;

	// Use this for initialization
	void Awake () {
		system = GameObject.Find("StellarSystem");
		systemCreator = system.GetComponent<SystemCreator>();

		orbitalPaths = new List<Ellipse>();
		planetTransforms = new List<Transform>();
		
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
			SetPosition(orbitalPaths[i], planetTransforms[i]);
		}
	}
	
	void Update () {
		for(int i = 0; i < orbitalPaths.Count; i++) {
			SetPosition(orbitalPaths[i], planetTransforms[i]);
		}
	}

	public void SetPosition(Ellipse orbitalPath, Transform planetTransform) {
		Vector2 position = orbitalPath.Evaluate(OrbitProgress);
		planetTransform.localPosition = new Vector3(position.x, position.y,0);
	}
}
