using UnityEngine;
using System.Collections;

public class SmallStar : MonoBehaviour {
  
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
    float camDistance = Vector3.Distance(transform.position, Camera.main.transform.position);
    Vector2 bottomCorner = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, camDistance));
    float minX = bottomCorner.x;

    var pos = transform.position;
    if (pos.x < minX)
    {
      Destroy(gameObject);
      return;
    }

    pos.x -= 0.05f;
    transform.position = pos;
	}
}
