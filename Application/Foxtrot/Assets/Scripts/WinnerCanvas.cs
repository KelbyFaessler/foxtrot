﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class WinnerCanvas : MonoBehaviour {

  // Use this for initialization
  void Start()
  {
    var scoreText = GetComponentsInChildren<Text>()[2];
    if (Player.instance != null)
    {
      scoreText.text = string.Format("{0}", Player.instance.m_Score);
      Player.instance.SetVisible(false);
      Player.instance.ResetPlayer();
    }
    if (HUDCanvas.instance != null)
      HUDCanvas.instance.gameObject.SetActive(false);

    Invoke("LoadMainMenu", 9f);
  }

  void LoadMainMenu()
  {
    SceneManager.LoadSceneAsync("MainMenu");
  }
}
