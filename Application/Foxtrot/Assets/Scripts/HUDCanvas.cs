using UnityEngine;
using System.Collections;

public class HUDCanvas : MonoBehaviour {

  public static HUDCanvas instance;

  // Singleton instance of HUDCanvas
  void Awake()
  {
    if (instance == null)
    {
      instance = this;
    }
    else if (instance != this)
    {
      Destroy(gameObject);
    }

    DontDestroyOnLoad(gameObject);
  }
}
