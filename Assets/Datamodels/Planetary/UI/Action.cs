using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Action 
{
    public string PathToImageResource => pathToImageResource;
    private string pathToImageResource;
    public string Title => title;
    private string title;
    public string Description => description;
    private string description;

    public Action(string pathToImageResource, string title, string description)
    {
        this.pathToImageResource = pathToImageResource;
        this.title = title;
        this.description = description;
    }

}