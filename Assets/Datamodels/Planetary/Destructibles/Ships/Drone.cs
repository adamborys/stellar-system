using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Drone : Ship
{
    public Drone(string unitClass, string iconPath, string miniaturePath, Alignment alignment, 
    int hitpoints, int mass) : base(unitClass, iconPath, miniaturePath, alignment, hitpoints, mass)
    {
    }
}