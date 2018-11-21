using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Ship : Destructible
{
    protected Ship(string unitClass, string iconPath, string miniaturePath, Alignment alignment, 
    int hitpoints, int mass) : base(unitClass, iconPath, miniaturePath, alignment, hitpoints, mass)
    {
    }
}