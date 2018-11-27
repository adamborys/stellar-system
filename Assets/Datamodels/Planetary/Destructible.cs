using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Destructible : Interactable 
{
    public int Hitpoints { get => hitpoints; }
    private int hitpoints;
    public int Mass { get => mass; }
    private int mass;

    protected Destructible(string iconPath, string miniaturePath, Alignment alignment,
    int hitpoints, int mass) : base(iconPath, miniaturePath, alignment)
    {
    }
}