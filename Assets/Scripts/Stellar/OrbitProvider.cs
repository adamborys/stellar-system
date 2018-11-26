using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class OrbitProvider : MonoBehaviour {

	public int Segments;
	public Ellipse OrbitShape;
	public Planet Planet;

	private LineRenderer lineRenderer;

	void Awake() {
		lineRenderer = GetComponent<LineRenderer>();
	}

	public void DisplayEllipse() {
		Vector3[] points = new Vector3[Segments + 1];

		for(int i = 0; i < Segments; i++) {
			Vector2 position = OrbitShape.Evaluate(((float)i/(float)Segments));
			points[i] = new Vector3(position.x, 0f, position.y);
		}

		points[Segments] = points[0];
		lineRenderer.positionCount = Segments + 1;
		lineRenderer.SetPositions(points);
	}
}