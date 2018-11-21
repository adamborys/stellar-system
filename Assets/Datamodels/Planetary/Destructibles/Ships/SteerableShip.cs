using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SteerableShip : Ship
{
    public SteerableShip(string unitClass, string iconPath, string miniaturePath, Alignment alignment, 
    int hitpoints, int mass) : base(unitClass, iconPath, miniaturePath, alignment, hitpoints, mass)
    {
    }
}