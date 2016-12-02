using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LevelOneTransition : MonoBehaviour {

	// Use this for initialization
	void Start () {
    Invoke("LoadLevelOne", 3.0f);
  }
	
	// Update is called once per frame
	void Update () {
	
	}

  public void LoadLevelOne()
  {
    var result = SceneManager.LoadSceneAsync("LevelOne");
    result.allowSceneActivation = true;
    Player.instance.SetVisible(true);
    HUDCanvas.instance.gameObject.SetActive(true);
  }
}
