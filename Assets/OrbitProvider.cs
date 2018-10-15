using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class OrbitProvider : MonoBehaviour {

	[Range(3,36)]
	public int Segments;
	public Ellipse OrbitShape;

	private LineRenderer lineRenderer;

	void Awake() {
		lineRenderer = GetComponent<LineRenderer>();
		CalculateEllipse();
	}

	void OnValidate() {
		if(Application.isPlaying && lineRenderer != null)
			CalculateEllipse();
	}

	public void CalculateEllipse() {
		Vector3[] points = new Vector3[Segments + 1];

		for(int i = 0; i < Segments; i++) {
			Vector2 position = OrbitShape.Evaluate(((float)i/(float)Segments));
			points[i] = new Vector3(position.x,position.y,0f);
		}

		points[Segments] = points[0];
		lineRenderer.positionCount = Segments + 1;
		lineRenderer.SetPositions(points);
	}
}