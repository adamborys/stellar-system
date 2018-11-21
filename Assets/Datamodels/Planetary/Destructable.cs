using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Destructable : Interactable 
{
    public int Hitpoints { get => hitpoints; }
    private int hitpoints;
    public int Mass { get => mass; }
    private int mass;

}