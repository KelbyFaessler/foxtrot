/******************************************************************
 * File:            ShipSelectMenu.cs
 * Author:          David Hite
 * Date Created:    10/19/2016
 * Date Modified:   10/26/2016
 * Description:
 * Contains the ShipSelectMenu class, which controls the ship 
 * selection menu
 ******************************************************************/
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class ShipSelectMenu : MonoBehaviour {
  public Slider m_ProgressSlider;
  private Canvas m_ProgressCanvas;

  void Start()
  {
    Player.instance.SetVisible(false);
    m_ProgressSlider = GetComponentInChildren<Slider>();
    m_ProgressCanvas = GetComponentsInChildren<Canvas>()[1];
    HideSlider(true);
    HUDCanvas.instance.gameObject.SetActive(false);
  }

  /*************************************************/
  /** LoadScene
  Assigns the selected ship to the player and loads
  the first level
  @param  : ship  int used as enum of the selected
                  ship
  **************************************************/
  public void LoadScene(int ship)
  {
    Player.instance.SetShip((Player.EShip)ship);
    var result = SceneManager.LoadSceneAsync("TestScene");
    result.allowSceneActivation = true;
    HideSlider(false);
    while (result.progress < 0.9f)
    {
      // Play loading animation
      m_ProgressSlider.value = result.progress;
    }
    Player.instance.SetVisible(true);
    HUDCanvas.instance.gameObject.SetActive(true);
  }

  /*************************************************/
  /** HideSlider
  Hides or shows the progress slider bar
  @param  : isHidden  When true, moves the slider
                      bar out of view. Otherwise
                      moves slider to center of screen
  **************************************************/
  private void HideSlider(bool isHidden)
  {
    var position = m_ProgressCanvas.transform.position;
    if (isHidden)
      position.y = 1000f;
    else
      position.y = 346.5f;
    m_ProgressCanvas.transform.position = position;
  }
}
