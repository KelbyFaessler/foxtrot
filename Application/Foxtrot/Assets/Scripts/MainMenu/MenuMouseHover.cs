using UnityEngine;
using System.Collections;

public class MenuMouseHover : MonoBehaviour {
  public Color originalTextColor = Color.white;
  public Color hoveredTextColor  = Color.cyan;

  // Use this for initialization
  void Start () {
    GetComponent<Renderer>().material.color = originalTextColor;
  }
	
  void OnMouseEnter() {
    GetComponent<Renderer>().material.color = hoveredTextColor;
  }

  void OnMouseExit() {
    GetComponent<Renderer>().material.color = originalTextColor;
  }
}
