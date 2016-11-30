using UnityEngine;
using System.Collections;

// Instantiates the static instance of Player
public class Loader : MonoBehaviour {
  public GameObject m_Player;
  public GameObject m_HUDCanvas;

	// Use this for initialization
	void Awake ()
  {
    if (HUDCanvas.instance == null)
    {
      Instantiate(m_HUDCanvas);
    }

    if (Player.instance == null)
    {
      Instantiate(m_Player);
    }
    Player.instance.GetCameraBounds();
	}
}
