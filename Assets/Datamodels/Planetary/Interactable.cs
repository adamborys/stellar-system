using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Interactable 
{
    //private string iconPath;
    //private string miniaturePath;
    public string Alignment { get => alignment; set => alignment = value; }
    private string alignment;

    /* 
    public Interactable(string iconPath, string miniaturePath, string alignment)
    {

    }
    */
}