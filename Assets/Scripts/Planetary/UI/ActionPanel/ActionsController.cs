using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionsController : MonoBehaviour
{
    List<string> ShipyardActionButtonsPath;
    
    private Button button;
    private GameObject slider;
    private Transform sliderTransform;

    private Slider progress;
    void Awake()
    {
        ShipyardActionButtonsPath = new List<string>();
        ShipyardActionButtonsPath.Add(@"Planetary\Prefabs\Shipyard\ActionButtons\BuildCarrierBtn");
        foreach(string path in ShipyardActionButtonsPath)
        {
            GameObject newButton = Instantiate(Resources.Load(path)) as GameObject;
            newButton.transform.SetParent(transform, false);
            sliderTransform = newButton.transform.GetChild(1);
            progress = (slider = sliderTransform.gameObject).GetComponent<Slider>();
            (button = newButton.GetComponent<Button>())
                .onClick.AddListener(delegate {buildCarrierClick();});
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void buildCarrierClick()
    {
        button.enabled = false;
        ActionProgress actionProgress = progress.GetComponent<ActionProgress>();
        StartCoroutine(actionProgress.DisplayProgress(Time.realtimeSinceStartup, 5f, button));
    }
}
