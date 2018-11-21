using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Planet : Interactable 
{
    public int TectonicIntegrity { get => tectonicIntegrity; set => tectonicIntegrity = value; }
    private int tectonicIntegrity;

}