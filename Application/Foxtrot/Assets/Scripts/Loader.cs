using UnityEngine;
using System.Collections;

// Instantiates the static instance of Player
public class Loader : MonoBehaviour {
  public GameObject m_Player;

	// Use this for initialization
	void Awake () {
	  if (Player.instance == null)
    {
      Instantiate(m_Player);
    }
	}
}
