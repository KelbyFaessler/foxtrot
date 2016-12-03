using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LevelThreeTransition : MonoBehaviour {

  // Use this for initialization
  void Start()
  {
    HUDCanvas.instance.gameObject.SetActive(false);
    Invoke("LoadLevelThree", 3.0f);
  }

  // Update is called once per frame
  void Update()
  {

  }

  public void LoadLevelThree()
  {
    var result = SceneManager.LoadSceneAsync("LevelThree");
    result.allowSceneActivation = true;
    Player.instance.SetVisible(true);
    HUDCanvas.instance.gameObject.SetActive(true);
  }
}
