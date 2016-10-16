using UnityEngine;
using System.Collections;

public class ShipSelectMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
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
  }
}
