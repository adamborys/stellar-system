using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractableController : MonoBehaviour
{
    public SelectionIndicator Indicator;
    
    protected virtual void Start()
    {
        Indicator = new SelectionIndicator(gameObject);
    }

    public void Select()
    {
        Indicator.IndicatorsHolder.SetActive(true);
    }
    public void Deselect()
    {
        Indicator.IndicatorsHolder.SetActive(false);
    }

}
