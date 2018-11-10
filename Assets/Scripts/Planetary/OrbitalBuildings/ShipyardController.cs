using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipyardController : MonoBehaviour
{
    private Transform clockwise, cclockwise;
    private bool isConstructing;
    // Start is called before the first frame update
    void Start()
    {
        clockwise = GameObject.Find("ShipyardClockwise").transform;
        cclockwise = GameObject.Find("ShipyardCClockwise").transform;
        isConstructing = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(isConstructing)
        {
            clockwise.Rotate(Vector3.forward, 0.5f);
            cclockwise.Rotate(Vector3.forward, -0.5f);
        }
    }
}
