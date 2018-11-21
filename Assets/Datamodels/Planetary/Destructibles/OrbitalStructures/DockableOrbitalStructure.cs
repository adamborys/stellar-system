using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DockableOrbitalStructure : OrbitalStructure
{
    public DockableOrbitalStructure(string unitClass, string iconPath, string miniaturePath, Alignment alignment, int hitpoints, int mass) : base(unitClass, iconPath, miniaturePath, alignment, hitpoints, mass)
    {
    }
}
