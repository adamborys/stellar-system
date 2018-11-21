using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipyardController : OrbitalStructureController
{
    private Transform clockwise, cclockwise;
    private bool isConstructing;
    
    void Start()
    {
        clockwise = GameObject.Find("Clockwise").transform;
        cclockwise = GameObject.Find("CClockwise").transform;
        isConstructing = true;
    }


    void FixedUpdate()
    {
        if(isConstructing)
        {
            clockwise.Rotate(Vector3.forward, 0.5f);
            cclockwise.Rotate(Vector3.forward, -0.5f);
        }
    }
}
