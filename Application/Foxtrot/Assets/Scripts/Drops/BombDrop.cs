using UnityEngine;
using System.Collections;

public class BombDrop : DropBase {

	// Use this for initialization
	void Start () {
	
	}

  // Update is called once per frame
  //void Update () {
  //  MoveAndRotate();
  //}

  // Apply bombs to player then self-destruct
  void OnTriggerEnter2D(Collider2D other)
  {
    if (other.gameObject.tag == "Player")
    {
      Player.instance.ApplyBombs();
      Destroy(gameObject);
    }
  }
}
