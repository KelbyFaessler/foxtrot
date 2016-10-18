﻿using UnityEngine;
using System.Collections;

public class Asteroid : MonoBehaviour {
  private const float m_Speed = 0.01f;
  // Use this for initialization
  void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
    // Find the left-most edge of the screen. If a planet is to the left of it, destroy it.
    float camDistance = Vector3.Distance(transform.position, Camera.main.transform.position);
    Vector2 bottomCorner = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, camDistance));
    float minX = bottomCorner.x;

    var pos = transform.position;
    if (pos.x < minX)
    {
      Destroy(gameObject);
      return;
    }

    // Otherwise, move the star to the left by m_StarSpeed
    pos.x -= m_Speed;
    transform.position = pos;
  }
}