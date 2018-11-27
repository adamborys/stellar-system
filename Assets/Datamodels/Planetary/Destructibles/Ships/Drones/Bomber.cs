using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomber : Drone
{
    public Bomber(string iconPath, string miniaturePath, Alignment alignment, 
    int hitpoints, int mass) : base(iconPath, miniaturePath, alignment, hitpoints, mass)
    {
    }
}
