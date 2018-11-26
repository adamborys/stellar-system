using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SelectionIndicator
{
    public GameObject IndicatorsHolder;
    private GameObject nWHolder, nEHolder, sWHolder, sEHolder,
    nWHolderBottom, nEHolderBottom, sWHolderBottom, sEHolderBottom;
    private MeshFilter nWFilter, nEFilter, sWFilter, sEFilter;
    private MeshRenderer nWRenderer, nERenderer, sWRenderer, sERenderer;
    private GameObject indicatedObject;
    private Material material;
    private float distance = 0.5f, width = 0.25f, x, y;

    public SelectionIndicator(GameObject indicatedObject)
    {
        material = Resources.Load(@"Planetary\Materials\Selection") as Material;
        this.indicatedObject = indicatedObject;

        Bounds bounds = indicatedObject.GetComponent<MeshCollider>().bounds;
        x = bounds.size.x / 2;
        y = bounds.size.z / 2;

        int[] triangles = new int[12] {4, 0, 2, 5, 4, 2, 5, 3, 4, 3, 1, 4};
        int[] trianglesInverse = new int[12] {4, 2, 0, 5, 2, 4, 5, 4, 3, 3, 4, 1};

        initializeIndicatorGameObjects();
        initializeIndicator(ref nWHolder, ref nWFilter, ref nWRenderer, triangles, -1, 1);
        initializeIndicator(ref nEHolder, ref nEFilter, ref nERenderer, trianglesInverse, 1, 1);
        initializeIndicator(ref sWHolder, ref sWFilter, ref sWRenderer, trianglesInverse, -1, -1);
        initializeIndicator(ref sEHolder, ref sEFilter, ref sERenderer, triangles, 1, -1);
        IndicatorsHolder = new GameObject("SelectionIndicator");
        nWHolder.transform.SetParent(IndicatorsHolder.transform, false);
        nEHolder.transform.SetParent(IndicatorsHolder.transform, false);
        sWHolder.transform.SetParent(IndicatorsHolder.transform, false);
        sEHolder.transform.SetParent(IndicatorsHolder.transform, false);
        nWHolderBottom = GameObject.Instantiate(nWHolder, IndicatorsHolder.transform);
        nEHolderBottom = GameObject.Instantiate(nEHolder, IndicatorsHolder.transform);
        sWHolderBottom = GameObject.Instantiate(sWHolder, IndicatorsHolder.transform);
        sEHolderBottom = GameObject.Instantiate(sEHolder, IndicatorsHolder.transform);
        nWHolderBottom.transform.localScale = new Vector3(1,-1,1);
        nEHolderBottom.transform.localScale = new Vector3(1,-1,1);
        sWHolderBottom.transform.localScale = new Vector3(1,-1,1);
        sEHolderBottom.transform.localScale = new Vector3(1,-1,1);
        IndicatorsHolder.transform.SetParent(indicatedObject.transform, false);
        IndicatorsHolder.SetActive(false);
    }

    public void FlipNormals(Vector3 direction)
    {
        for (int i = 0; i < 6; i++)
        {
            nWFilter.mesh.normals[i] = nEFilter.mesh.normals[i] = 
            sWFilter.mesh.normals[i] = sEFilter.mesh.normals[i] = direction;
        }
    }

    private void initializeIndicator(
        ref GameObject indicatorHolder, 
        ref MeshFilter meshFilter,
        ref MeshRenderer meshRenderer,
        int[] trisSequence, float xMultiplier, float yMultiplier)
    {

        meshFilter = indicatorHolder.AddComponent<MeshFilter>();
        meshRenderer = indicatorHolder.AddComponent<MeshRenderer>();
        meshRenderer.receiveShadows = false;
        Vector3[] vertices = new Vector3[6];

        vertices[0] = new Vector3(
            xMultiplier * (x + distance), 
            0, 
            yMultiplier * y
        );
        vertices[1] = new Vector3(
            xMultiplier * x, 
            0, 
            yMultiplier * (y + distance)
        );
        vertices[2] = new Vector3(
            xMultiplier * (x + distance + width), 
            0, 
            yMultiplier * (y + width)
        );
        vertices[3] = new Vector3(
            xMultiplier * (x + width), 
            0, 
            yMultiplier * (y + distance + width)
        );
        vertices[4] = new Vector3(
            xMultiplier * (x + distance), 
            0, 
            yMultiplier * (y + distance)
        );
        vertices[5] = new Vector3(
            xMultiplier * (x + distance + width), 
            0, 
            yMultiplier * (y + distance + width)
        );

        meshFilter.mesh.vertices = vertices;
        meshFilter.mesh.triangles = trisSequence;

        meshFilter.mesh.normals = new Vector3[6];
        for (int i = 0; i < 6; i++) meshFilter.mesh.normals[i] = Vector3.up;

        meshRenderer.material = material;
    }

    private void initializeIndicatorGameObjects()
    {  
        this.nWHolder = new GameObject("nWHolder");
        this.nEHolder = new GameObject("nEHolder");
        this.sWHolder = new GameObject("sWHolder");
        this.sEHolder = new GameObject("sEHolder");
    }
}