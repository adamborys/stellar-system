using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ActionsToggleController : MonoBehaviour
{
    private Vector3 start, end;
    private RectTransform actionsTransform;
    private Toggle actionsToggle;
    
    private float progress;
    

    void Start()
    {
        actionsTransform = GameObject.Find("ActionsPanel").GetComponent<RectTransform>();
        actionsToggle = GetComponent<Toggle>();
        actionsToggle.onValueChanged.AddListener(delegate { toggleChange(); });

        start = new Vector3(0,120,0);
        end = new Vector3(0,170,0);
        progress = 1f;
    }   

    // Update is called once per frame 
    void Update()
    {
    }

    void toggleChange()
    {
        if (actionsToggle.isOn)
        {
            StopAllCoroutines();
            StartCoroutine(calculateLerpUp());
        }
        else
        {
            StopAllCoroutines();
            StartCoroutine(calculateLerpDown());
        }
    }

    private IEnumerator calculateLerpUp()
    {
        while(progress < 1)
		{
            progress += 0.1f;
            actionsTransform.anchoredPosition = Vector3.Lerp(start, end, progress);
            yield return new WaitForFixedUpdate();
        }
    }
    private IEnumerator calculateLerpDown()
    {
        while(progress > 0)
		{
            progress -= 0.1f;
            actionsTransform.anchoredPosition = Vector3.Lerp(start, end, progress);
            yield return new WaitForFixedUpdate();
        }
    }
}
