using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Action 
{
    public string PathToImageResource { get => pathToImageResource; set => pathToImageResource = value; }
    private string pathToImageResource;
    public string Title { get => title; set => title = value; }
    private string title;
    public string Description { get => description; set => description = value; }
    private string description;

    public Action(string pathToImageResource, string title, string description)
    {
        this.PathToImageResource = pathToImageResource;
        this.Title = title;
        this.Description = description;
    }

}