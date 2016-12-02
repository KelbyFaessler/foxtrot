using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LevelTwoTransition : MonoBehaviour {

  // Use this for initialization
  void Start()
  {
    HUDCanvas.instance.gameObject.SetActive(false);
    Invoke("LoadLevelTwo", 3.0f);
  }

  // Update is called once per frame
  void Update()
  {

  }

  public void LoadLevelTwo()
  {
    var result = SceneManager.LoadSceneAsync("LevelTwo");
    result.allowSceneActivation = true;
    Player.instance.SetVisible(true);
    HUDCanvas.instance.gameObject.SetActive(true);
  }
}
