using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridController : MonoBehaviour
{
    public int lineDrawDistance;
    public int Interval = 5;
    public int NarrowLinesInterval = 5;
    public float NarrowLineWidth = 0.1f;
    public float ThinLineWidth = 0.05f;
    public Material Material;

    private int segmentsCount;
    void Start()
    {
        if(lineDrawDistance % Interval != 0)
            Debug.LogError("Interval should be divisor of lineDrawDistance!");
        int narrowLinesInterval = NarrowLinesInterval * Interval;
        for(int i = -lineDrawDistance; i <= lineDrawDistance; i += Interval)
        {
            GameObject lineX = new GameObject("LineX"+ (i + lineDrawDistance));
            lineX.transform.SetParent(transform);
            GameObject lineY = new GameObject("LineY"+ (i + lineDrawDistance));
            lineY.transform.SetParent(transform);
            LineRenderer lineXRenderer = lineX.AddComponent<LineRenderer>();
            LineRenderer lineYRenderer = lineY.AddComponent<LineRenderer>();

            if(i % narrowLinesInterval == 0)
                lineXRenderer.startWidth = lineYRenderer.startWidth = 
                lineXRenderer.endWidth = lineYRenderer.endWidth = NarrowLineWidth;
            else
            {
                lineXRenderer.startWidth = lineYRenderer.startWidth = 
                lineXRenderer.endWidth = lineYRenderer.endWidth = ThinLineWidth;
            }

            lineXRenderer.material = lineYRenderer.material = Material;
            segmentsCount = 2 * (lineDrawDistance / Interval) + 1;

            Vector3[] xPositions = new Vector3[segmentsCount];
            Vector3[] yPositions = new Vector3[segmentsCount];

            for(int j = -lineDrawDistance, k = 0; j <= lineDrawDistance; j += Interval, k++)
            {
                xPositions[k] = new Vector3(j, 0, i);
                yPositions[k] = new Vector3(i, 0, j);
            }
            
            lineXRenderer.positionCount = lineYRenderer.positionCount = segmentsCount;
            lineXRenderer.SetPositions(xPositions);
            lineYRenderer.SetPositions(yPositions);
        }
    }
}
