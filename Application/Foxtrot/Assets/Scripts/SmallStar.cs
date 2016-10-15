/******************************************************************
 * File:            SmallStar.cs
 * Author:          David Hite
 * Date Created:    10/2/2016
 * Date Modified:   10/3/2016
 * Description:
 * Contains the SmallStar class, which controls the behavior of 
 * SmallStar objects
 ******************************************************************/
using UnityEngine;
using System.Collections;

public class SmallStar : MonoBehaviour {
  private const float m_StarSpeed = 0.05f;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
    // Find the left-most edge of the screen. If a star is to the left of it, destroy it.
    var bounds = Globals.GetCameraBounds(gameObject);
    float minX = bounds.m_MinX;

    var pos = transform.position;
    if (pos.x < minX)
    {
      Destroy(gameObject);
      return;
    }

    // Otherwise, move the star to the left by m_StarSpeed
    pos.x -= m_StarSpeed;
    transform.position = pos;
	}
}
