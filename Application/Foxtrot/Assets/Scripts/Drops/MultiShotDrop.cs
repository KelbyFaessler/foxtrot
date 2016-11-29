using UnityEngine;
using System.Collections;

public class MultiShotDrop : DropBase {

	// Use this for initialization
	void Start () {
	
	}

  // Update is called once per frame
  //void Update () {
  //  MoveAndRotate();
  //}

  // Apply MultiShots to player then self-destruct
  void OnTriggerEnter2D(Collider2D other)
  {
    if (other.gameObject.tag == "Player")
    {
      Player.instance.ApplyMultiShots();
      Destroy(gameObject);
    }
  }
}
