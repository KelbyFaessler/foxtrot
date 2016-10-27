using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ShipSelectMenu : MonoBehaviour {
  void Start()
  {
    Player.instance.SetVisible(false);
  }
  public void LoadScene(int ship)
  {
    Player.instance.SetShip((Player.EShip)ship);
    var result = SceneManager.LoadSceneAsync(1);
    result.allowSceneActivation = true;
    while (result.progress < 0.9f)
    {
      // Play loading animation
    }
    Player.instance.SetVisible(true);
  }
	// Use this for initialization
	/*void Start () {
    GetComponent<Renderer>().material.shader = Shader.Find("Self-Illumin/Outlined Diffuse");
  }
	
	// Update is called once per frame
	void Update () {
	
	}

  void OnMouseEnter()
  {
    GetComponent<Renderer>().material.shader = Shader.Find("Self-Illumin/Outlined Diffuse");
  }

  void OnMouseExit()
  {
    GetComponent<Renderer>().material.shader = Shader.Find("Diffuse");
  }*/
}
