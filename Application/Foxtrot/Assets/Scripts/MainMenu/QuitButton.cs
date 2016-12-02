using UnityEngine;
using System.Collections;

public class QuitButton : MonoBehaviour {
  
	void OnMouseUp()
  {
    Application.Quit();
  }
}
