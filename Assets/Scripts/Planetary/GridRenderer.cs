using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridRenderer : MonoBehaviour
{
    public int LineDrawDistance;
    public Material Material;
    void Start()
    {
        for(int i = -LineDrawDistance; i < LineDrawDistance; i++)
        {
            GameObject lineX = new GameObject("LineX"+ i);
            lineX.transform.SetParent(transform);
            GameObject lineY = new GameObject("LineY"+ i);
            lineY.transform.SetParent(transform);
            LineRenderer lineXRenderer = lineX.AddComponent<LineRenderer>();
            LineRenderer lineYRenderer = lineY.AddComponent<LineRenderer>();
            if(i % 5 == 0)
                lineXRenderer.startWidth = lineYRenderer.startWidth = lineXRenderer.endWidth = lineYRenderer.endWidth = 0.3f;
            else
            {
                lineXRenderer.startWidth = lineYRenderer.startWidth = lineXRenderer.endWidth = lineYRenderer.endWidth = 0.15f;
            }
            lineXRenderer.material = lineYRenderer.material = Material;
            lineXRenderer.SetPositions(new Vector3[] {new Vector3(-LineDrawDistance,0,i), new Vector3(LineDrawDistance,0,i)});
            lineYRenderer.SetPositions(new Vector3[] {new Vector3(i,0,-LineDrawDistance), new Vector3(i,0,LineDrawDistance)});
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
