/******************************************************************
 * File:            ShipBase.cs
 * Author:          David Hite
 * Date Created:    10/15/2016
 * Date Modified:   10/15/2016
 * Description:
 * Contains the ShipBase class, which is to be used as an abstract
 * class. Children of this class will represent different types of
 * player and enemy ships.
 ******************************************************************/
using UnityEngine;
using System.Collections;

public class ShipBase : MonoBehaviour {
  // Member variables
  public float m_SpriteWidthFromCenter;
  public float m_SpriteHeightFromCenter;

  // Each ship can have a different starting max health and move speed
  public float m_BaseMaxHealth;
  public float m_MoveSpeed = 0.08f;

  public string m_Name;
  public SpriteRenderer m_SpriteRenderer;

  // Use this for initialization
  void Start ()
  {
    // Get the size of the sprite (half), for checking boundaries
    var sprite = GetComponent<SpriteRenderer>();
    m_SpriteWidthFromCenter = (float)(sprite.bounds.size.x / 2);
    m_SpriteHeightFromCenter = (float)(sprite.bounds.size.y / 2);
  }
	
	// Update is called once per frame
	void Update () {
	
	}
}
