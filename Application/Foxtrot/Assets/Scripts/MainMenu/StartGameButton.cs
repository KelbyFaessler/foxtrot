/******************************************************************
 * File:            StartGameButton.cs
 * Author:          David Hite
 * Date Created:    10/29/2016
 * Date Modified:   10/29/2016
 * Description:
 * Contains the StartGameButton class, which is connected to the
 * Start Game button.
 ******************************************************************/
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class StartGameButton : MonoBehaviour {
  // When the Start Game button is clicked, go to the ship selection menu
  void OnMouseUp()
  {
    var result  = SceneManager.LoadSceneAsync("ShipSelectionMenu");
  }
}
