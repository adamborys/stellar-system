using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionProgress : MonoBehaviour
{
    private Slider progress;
    private Text text;
    // Start is called before the first frame update
    void Start()
    {
        progress = GetComponent<Slider>();
        text = transform.GetChild(3).gameObject.GetComponent<Text>();
        text.text = "";
        gameObject.SetActive(false);
    }

    public IEnumerator DisplayProgress(float timeStart, float duration, Button button)
    {
        gameObject.SetActive(true);
        progress.value = 0;
        float timeStop = timeStart + duration;
        float secondsPassed = 0;
        while((progress.value = (secondsPassed = Time.realtimeSinceStartup - timeStart) / duration) < 1)
		{
            TimeSpan time = TimeSpan.FromSeconds(duration - secondsPassed);
            text.text = time.ToString("m':'ss");
            yield return new WaitForFixedUpdate();
        }
        text.text = "";
        button.enabled = true;
        gameObject.SetActive(false);
    }
}
