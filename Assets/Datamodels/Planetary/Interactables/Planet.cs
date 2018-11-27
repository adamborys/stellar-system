using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Planet : Interactable 
{
    public int TectonicIntegrity => tectonicIntegrity;
    private int tectonicIntegrity;

    public Planet(string iconPath, string miniaturePath, Alignment alignment,
    int tectonicIntegrity) : base(iconPath, miniaturePath, alignment)
    {
        this.tectonicIntegrity = tectonicIntegrity;
    }
}