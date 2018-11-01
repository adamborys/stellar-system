using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridController : MonoBehaviour
{
    public int LineDrawDistance = 1000;
    public int Interval = 5;
    public int NarrowLinesInterval = 5;
    public float NarrowLineWidth = 0.1f;
    public float ThinLineWidth = 0.05f;
    public Material Material;
    void Start()
    {
        if(LineDrawDistance % Interval != 0)
            Debug.LogError("Interval should be divisor of LineDrawDistance!");
        for(int i = -LineDrawDistance; i <= LineDrawDistance; i += Interval)
        {
            GameObject lineX = new GameObject("LineX"+ (i + LineDrawDistance));
            lineX.transform.SetParent(transform);
            GameObject lineY = new GameObject("LineY"+ (i + LineDrawDistance));
            lineY.transform.SetParent(transform);

            LineRenderer lineXRenderer = lineX.AddComponent<LineRenderer>();
            LineRenderer lineYRenderer = lineY.AddComponent<LineRenderer>();
            if(i % (NarrowLinesInterval * Interval) == 0)
                lineXRenderer.startWidth = lineYRenderer.startWidth = lineXRenderer.endWidth = lineYRenderer.endWidth = NarrowLineWidth;
            else
            {
                lineXRenderer.startWidth = lineYRenderer.startWidth = lineXRenderer.endWidth = lineYRenderer.endWidth = ThinLineWidth;
            }
            lineXRenderer.material = lineYRenderer.material = Material;

            int segmentsCount = 2 * (LineDrawDistance / Interval) + 1;
            Vector3[] xPositions = new Vector3[segmentsCount];
            Vector3[] yPositions = new Vector3[segmentsCount];

            for(int j = -LineDrawDistance, k = 0; j <= LineDrawDistance; j += Interval, k++)
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
