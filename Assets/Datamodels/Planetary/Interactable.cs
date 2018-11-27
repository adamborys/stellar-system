using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Alignment {same, friendly, neutral, enemy}

[System.Serializable]
public abstract class Interactable 
{
    public string UnitClass { get => unitClass; set => unitClass = value; }
    private string unitClass;
    public string IconPath { get => iconPath; set => iconPath = value; }
    private string iconPath;
    public string MiniaturePath { get => miniaturePath; set => miniaturePath = value; }
    private string miniaturePath;
    public Alignment Alignment { get => alignment; set => alignment = value; }

    private Alignment alignment;


    protected Interactable(string iconPath, string miniaturePath, Alignment alignment)
    {
        this.UnitClass = unitClass;
        this.iconPath = iconPath;
        this.miniaturePath = miniaturePath;
        this.alignment = alignment;
    }
    
}