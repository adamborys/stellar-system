using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemCreator : MonoBehaviour {

	[Range(2,10)]
	public int PlanetQuantity;

	[Range(3,64)]
	public int RenderedSegments;
	[Range(0f,0.2f)]
	public float SizeDispersion;
	[Range(0f,0.5f)]
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
		xAxis = yAxis = 8 + 10 * SizeDispersion;

		for(int i = 0; i < PlanetQuantity; i++) {
			
			// Stworzenie orbit oraz podpięcie transforma pod układ planetarny
			Orbits.Add(Instantiate(Prefab) as GameObject);
			Orbits[i].name = "Orbit" + i;
			Orbits[i].transform.parent = transform;
			
			// Randomizowane odkształcenie orbit
			float resize = (float)rand.NextDouble();
			float xAxisResized = xAxis + resize * SizeDispersion / (i+1);
			float yAxisResized = yAxis + resize * SizeDispersion * (i+1);

			// Randomizowane odchylanie orbit
			Quaternion currentRotation = Orbits[i].transform.rotation;
			double directionRandomizer = rand.NextDouble();
			if(directionRandomizer > 0.75f)
				Orbits[i].transform.rotation = new Quaternion(
					currentRotation.x + (float)rand.NextDouble() * AngleDispersion,
					currentRotation.y,
					currentRotation.z + (float)rand.NextDouble() * AngleDispersion,
					currentRotation.w
				);
			else if(directionRandomizer > 0.5f)
				Orbits[i].transform.rotation = new Quaternion(
					currentRotation.x - (float)rand.NextDouble() * AngleDispersion,
					currentRotation.y,
					currentRotation.z - (float)rand.NextDouble() * AngleDispersion,
					currentRotation.w
				);
			else if(directionRandomizer > 0.25f)
				Orbits[i].transform.rotation = new Quaternion(
					currentRotation.x + (float)rand.NextDouble() * AngleDispersion,
					currentRotation.y,
					currentRotation.z - (float)rand.NextDouble() * AngleDispersion,
					currentRotation.w
				);
			else
				Orbits[i].transform.rotation = new Quaternion(
					currentRotation.x - (float)rand.NextDouble() * AngleDispersion,
					currentRotation.y,
					currentRotation.z + (float)rand.NextDouble() * AngleDispersion,
					currentRotation.w
				);

			OrbitProvider orbitProvider = Orbits[i].GetComponent<OrbitProvider>();
			orbitProvider.OrbitShape = new Ellipse(xAxisResized,yAxisResized);
			orbitProvider.Segments = (RenderedSegments/2) + (RenderedSegments/2)*(i/(PlanetQuantity-1));

			// Zasymulowanie pierwszego prawa Keplera
			float focalDistance = orbitProvider.OrbitShape.FocalDistance;
			if(xAxisResized > yAxisResized) {
				Orbits[i].transform.Translate(new Vector3(
					focalDistance,
					0,
					0
				));
			}
			else {
				Orbits[i].transform.Translate(new Vector3(
					0,
					0,
					focalDistance
				));
			}
			
			// Wyświetlanie orbit
			orbitProvider.DisplayEllipse();

			xAxis += 6 + 20 * SizeDispersion;
			yAxis += 6 + 20 * SizeDispersion;
		}
		gameObject.AddComponent<PlanetController>();
	}
}
