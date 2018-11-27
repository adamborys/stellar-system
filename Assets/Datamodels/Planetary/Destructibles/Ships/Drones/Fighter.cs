using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : Drone
{
    public Fighter(string iconPath, string miniaturePath, Alignment alignment, 
    int hitpoints, int mass) : base(iconPath, miniaturePath, alignment, hitpoints, mass)
    {
    }
}
