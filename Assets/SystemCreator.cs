using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemCreator : MonoBehaviour {

	[Range(2,10)]
	public int PlanetQuantity;

	[Range(3,36)]
	public int RenderedSegments;
	[Range(0f,1f)]
	public float SizeDispersion;
	[Range(0f,1f)]
	public float AngleDispersion;

	public GameObject Prefab;
	public List<GameObject> Orbits;

	private float xAxis, yAxis;
	
	void Awake() {
		Orbits = new List<GameObject>();
		CalculateOrbits();
	}

	void OnValidate() {
		if(Application.isPlaying && Orbits != null) {
			CalculateOrbits();
		}
	}

	public void CalculateOrbits() {
		// Czyszczenie obiektów z poprzedniej instancji
		Destroy(gameObject.GetComponent<PlanetController>());
		Orbits.Clear();
		foreach (Transform child in transform) {
			if(child.transform.gameObject.name != "Star")
				GameObject.Destroy(child.gameObject);
		}
		
		System.Random rand = new System.Random();
		xAxis = yAxis = 2;

		for(int i = 0; i < PlanetQuantity; i++) {
			// Stworzenie orbit oraz podpięcie transforma pod układ planetarny
			Orbits.Add(Instantiate(Prefab) as GameObject);
			Orbits[i].name = "Orbit" + i;
			Orbits[i].transform.parent = transform;
			
			// Randomizowane odkształcenie orbit
			float xAxisResized = xAxis + (float)rand.NextDouble() * SizeDispersion / (i+1);
			float yAxisResized = yAxis + (float)rand.NextDouble() * SizeDispersion * (i+1);

			// Randomizowane odchylanie orbit
			Quaternion currentRotation = Orbits[i].transform.rotation;
			Orbits[i].transform.rotation = new Quaternion(
				currentRotation.x + (float)rand.NextDouble() * AngleDispersion,
				currentRotation.y + (float)rand.NextDouble() * AngleDispersion,
				currentRotation.z + (float)rand.NextDouble() * AngleDispersion / (i+1),
				currentRotation.w
			);

			OrbitProvider orbit = Orbits[i].GetComponent<OrbitProvider>();
			orbit.OrbitShape = new Ellipse(xAxisResized,yAxisResized);
			orbit.Segments = RenderedSegments;
			orbit.CalculateEllipse();

			if(xAxisResized > yAxisResized) {
				Orbits[i].transform.Translate(new Vector3(
					orbit.OrbitShape.FocalDistance,
					0,
					0
				));
			} else {
				Orbits[i].transform.Translate(new Vector3(
					0,
					orbit.OrbitShape.FocalDistance,
					0
				));
			}


			xAxis += 2;
			yAxis += 2;
		}
		gameObject.AddComponent<PlanetController>();
	}
}
