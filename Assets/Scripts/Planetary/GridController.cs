using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridController : MonoBehaviour
{
    public int LineDrawDistance = 1000;
    public Material Material;
    private List<LineRenderer> lineRenderers;
    void Start()
    {
        lineRenderers = new List<LineRenderer>();
        for(int i = -LineDrawDistance; i <= LineDrawDistance; i++)
        {
            GameObject lineX = new GameObject("LineX"+ i);
            lineX.transform.SetParent(transform);
            GameObject lineY = new GameObject("LineY"+ i);
            lineY.transform.SetParent(transform);

            // Co drugi renderer to linia należąca do tej samej osi
            LineRenderer lineXRenderer = lineX.AddComponent<LineRenderer>();
            lineRenderers.Add(lineXRenderer);
            LineRenderer lineYRenderer = lineY.AddComponent<LineRenderer>();
            lineRenderers.Add(lineYRenderer);
            if(i % 10 == 0)
                lineXRenderer.startWidth = lineYRenderer.startWidth = lineXRenderer.endWidth = lineYRenderer.endWidth = 0.1f;
            else
            {
                lineXRenderer.startWidth = lineYRenderer.startWidth = lineXRenderer.endWidth = lineYRenderer.endWidth = 0.05f;
            }
            lineXRenderer.material = lineYRenderer.material = Material;
        }
    }
    
    public void AdjustGrid(Vector3 cameraParentPosition, int offsetX, int offsetZ)
    {
        for(int i = 0; i < (2 * LineDrawDistance) + 1; i++)
        {
            if(i % 2 == 0)
            {
                lineRenderers[i].SetPositions(
                    new Vector3[] 
                    {
                        new Vector3(-LineDrawDistance + offsetX, 0, i - LineDrawDistance + offsetZ), 
                        new Vector3(LineDrawDistance + offsetX, 0, i - LineDrawDistance + offsetZ)
                    });
                
                if((i + offsetZ)% 10 == 0)
                    lineRenderers[i].startWidth = lineRenderers[i].endWidth = 0.2f;
                else
                {
                    lineRenderers[i].startWidth = lineRenderers[i].endWidth = 0.05f;
                }
                
            }
            else
            {
                lineRenderers[i].SetPositions(
                    new Vector3[] 
                    {
                        new Vector3(i - LineDrawDistance + offsetX, 0, -LineDrawDistance + offsetZ), 
                        new Vector3(i - LineDrawDistance + offsetX, 0, LineDrawDistance + offsetZ)
                    });
                
                if((i + offsetX) % 10 == 0)
                    lineRenderers[i].startWidth = lineRenderers[i].endWidth = 0.2f;
                else
                {
                    lineRenderers[i].startWidth = lineRenderers[i].endWidth = 0.05f;
                }
            }
        }
    }
}
