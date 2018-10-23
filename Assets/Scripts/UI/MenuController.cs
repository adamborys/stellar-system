using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{

  private Text planetQuantityText;
  private Slider planetSlider;
  private Dropdown gameDuration;
  private Toggle isTight;
  private Toggle isStrange;
  private Button create;
  private SystemCreator systemCreator;

  void Start()
  {
    systemCreator = GameObject.Find("SystemOrigin").GetComponent<SystemCreator>();

    planetQuantityText = GameObject.Find("PlanetQuantityText").GetComponent<Text>();
    planetSlider = GameObject.Find("PlanetSlider").GetComponent<Slider>();
    planetSlider.onValueChanged.AddListener(delegate { planetSliderChange(); });
    gameDuration = GameObject.Find("GameDuration").GetComponent<Dropdown>();
    gameDuration.onValueChanged.AddListener(delegate { gameDurationChange(); });

    isTight = GameObject.Find("IsTight").GetComponent<Toggle>();
    isTight.onValueChanged.AddListener(delegate { isTightToggleChange(); });
    isStrange = GameObject.Find("IsStrange").GetComponent<Toggle>();
    isStrange.onValueChanged.AddListener(delegate { isStrangeToggleChange(); });
    create = GameObject.Find("Create").GetComponent<Button>();
    create.onClick.AddListener(delegate { createSystem(); });
  }

  private void gameDurationChange()
  {
    PlanetController planetController = GameObject.Find("SystemOrigin").GetComponent<PlanetController>();
    if (gameDuration.value == 0)
    {
      StellarSystem.GameDuration = 900f;
    }
    else
    {
      StellarSystem.GameDuration = 1800f;
    }
  }

  private void planetSliderChange()
  {
    systemCreator.PlanetQuantity = (int)planetSlider.value;
    planetQuantityText.text = systemCreator.PlanetQuantity.ToString();
    systemCreator.CalculateOrbits();
  }

  private void isTightToggleChange()
  {
    if (isTight.isOn)
      systemCreator.SizeDispersion = 0.1f;
    else
      systemCreator.SizeDispersion = 0.2f;
    systemCreator.CalculateOrbits();
  }

  private void isStrangeToggleChange()
  {
    if (isStrange.isOn)
      systemCreator.AngleDispersion = 0.1f;
    else
      systemCreator.AngleDispersion = 0.05f;
    systemCreator.CalculateOrbits();
  }

  private void createSystem()
  {
    GameObject system = GameObject.Find("SystemOrigin");
    Destroy(system.GetComponent<SystemCreator>());
    StartCoroutine(loadStellarSceneWithGameObjectTransferAsync(system));
  }

  private IEnumerator loadStellarSceneWithGameObjectTransferAsync(GameObject transferredGameObject)
  {
    Scene currentScene = SceneManager.GetActiveScene();
    AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Stellar", LoadSceneMode.Additive);

    while (!asyncLoad.isDone)
    {
      yield return null;
    }

    SceneManager.MoveGameObjectToScene(transferredGameObject, SceneManager.GetSceneByName("Stellar"));
    SceneManager.UnloadSceneAsync(currentScene);
  }
}
