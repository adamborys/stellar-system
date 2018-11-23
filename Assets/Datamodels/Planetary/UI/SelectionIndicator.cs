using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SelectionIndicator 
{
    public LineRenderer NW { get => nW;}
    public LineRenderer NE { get => nE;}
    public LineRenderer SW { get => sW;}
    public LineRenderer SE { get => sE;}    
    public GameObject IndicatedObject { get => indicatedObject; set => indicatedObject = value; }
    public Material Material { get => material; set => material = value; }

    private GameObject nWHolder, nEHolder, sWHolder, sEHolder;
    private LineRenderer nW, nE, sW, sE;
    private GameObject indicatedObject;
    private Material material;


    public SelectionIndicator(GameObject indicatedObject)
    {
        this.indicatedObject = indicatedObject;
        initRenderersGameObjects();
        setupRenderers();
        EnableIndication();
    }

    public void DisableIndication()
    {
        this.nW.positionCount = this.nE.positionCount = this.sW.positionCount = this.sE.positionCount = 0;
    }

    public void EnableIndication()
    {
        this.nW.positionCount = this.nE.positionCount = this.sW.positionCount = this.sE.positionCount = 3;
        Bounds bounds = indicatedObject.GetComponent<MeshCollider>().bounds;

        float length;
        if(bounds.size.x > bounds.size.z)
            length = bounds.size.x / (10*indicatedObject.transform.localScale.x);
        else
            length = bounds.size.z / (10*indicatedObject.transform.localScale.x);

        this.nW.widthMultiplier = this.nE.widthMultiplier = this.sW.widthMultiplier = this.sE.widthMultiplier = 1;

        float x = (bounds.size.x + 2) / (2*indicatedObject.transform.localScale.x), 
              y = (bounds.size.z + 2) / (2*indicatedObject.transform.localScale.x);
        this.nW.SetPositions(
            new Vector3 [] {
                new Vector3(-x , 0, y - length),
                new Vector3(-x , 0, y ),
                new Vector3(-x + length, 0, y )
            }/*
            new Vector3 [] {
                new Vector3(-100 , 0, 75),
                new Vector3(-100 , 0, 100 ),
                new Vector3(-75, 0, 100 )
            }*/
        );
        this.nE.SetPositions(
            new Vector3 [] {
                new Vector3(x - length, 0, y ),
                new Vector3(x , 0, y ),
                new Vector3(x , 0, y - length)
            }/*
            new Vector3 [] {
                new Vector3(75 , 0, 100),
                new Vector3(100 , 0, 100),
                new Vector3(100 , 0, 75 )
            }*/
        );
        this.sW.SetPositions(
            new Vector3 [] {
                new Vector3(-x , 0, -y + length),
                new Vector3(-x , 0, - y ),
                new Vector3(-x + length, 0, -y )
            }/*
            new Vector3 [] {
                new Vector3(-100 , 0, -75),
                new Vector3(-100 , 0, -100),
                new Vector3(-75, 0, -100 )
            }*/
        );
        this.sE.SetPositions(
            new Vector3 [] {
                new Vector3(x - length, 0, -y ),
                new Vector3(x , 0, -y ),
                new Vector3(x , 0, -y + length)
            }/*
            new Vector3 [] {
                new Vector3(75 , 0, -100),
                new Vector3(100 , 0, -100),
                new Vector3(100, 0, -75 )
            }*/
        );
    }

    private void initRenderersGameObjects()
    {  
        this.nWHolder = new GameObject();
        this.nEHolder = new GameObject();
        this.sWHolder = new GameObject();
        this.sEHolder = new GameObject();
        this.nWHolder.name = "nWHolder";
        this.nEHolder.name = "nEHolder";
        this.sWHolder.name = "sWHolder";
        this.sEHolder.name = "sEHolder";
    }
    private void setupRenderers()
    {
        (this.nW = nWHolder.AddComponent<LineRenderer>()).useWorldSpace = false;
        (this.nE = nEHolder.AddComponent<LineRenderer>()).useWorldSpace = false;
        (this.sW = sWHolder.AddComponent<LineRenderer>()).useWorldSpace = false;
        (this.sE = sEHolder.AddComponent<LineRenderer>()).useWorldSpace = false;
        this.nWHolder.transform.SetParent(indicatedObject.transform, false);
        this.nEHolder.transform.SetParent(indicatedObject.transform, false);
        this.sWHolder.transform.SetParent(indicatedObject.transform, false);
        this.sEHolder.transform.SetParent(indicatedObject.transform, false);
        this.nW.material = this.nE.material = this.sW.material = this.sE.material = 
        Resources.Load(@"Planetary\Materials\Selection") as Material;
    }
}