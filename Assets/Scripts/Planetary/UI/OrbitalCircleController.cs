using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitalCircleController : MonoBehaviour
{
    private float radius;
    private float segments;
    private LineRenderer outlineRenderer;

    public OrbitalCircleController(float radius, float segments)
    {
        this.radius = radius;
        this.segments = segments;
    }

    void Start()
    {
        outlineRenderer = gameObject.AddComponent<LineRenderer>();
        //drawCircle();
    }
    
/* 
    private void drawCircle()
    {
        outlineRenderer.widthMultiplier = lineWidth;

        float deltaTheta = (2f * Mathf.PI) / vertexCount;
        float theta = 0f;

        lineRenderer.positionCount = vertexCount;
        for (int i=0; i<lineRenderer.positionCount; i++)
        {
            Vector3 pos = new Vector3(radius * Mathf.Cos(theta), radius * Mathf.Sin(theta), 0f);
            lineRenderer.SetPosition(i, pos);
            theta += deltaTheta;
        }
    }

*/     
}
