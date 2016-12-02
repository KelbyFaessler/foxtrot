/******************************************************************
 * File:            Galaxy.cs
 * Author:          David Hite
 * Date Created:    10/19/2016
 * Date Modified:   10/19/2016
 * Description:
 * Contains the Galaxy class. This class represents the 
 * Galaxy Class starship.
 ******************************************************************/
using UnityEngine;
using System.Collections;

public class Galaxy : ShipBase {
  // Use this for initialization
  void Awake()
  {
    m_BaseMaxHealth = 13f;
    m_MoveSpeed = 0.25f;
    m_Name = "Galaxy Class";
    m_Acceleration = 0.2f;

    if (m_SpriteRenderer == null)
    {
      m_SpriteRenderer = GetComponent<SpriteRenderer>();
      var temp = Resources.Load("SpriteRenderers/galaxy_class_renderer", typeof(SpriteRenderer)) as SpriteRenderer;
      m_SpriteRenderer.sprite = temp.sprite;
      m_SpriteRenderer.enabled = true;
    }
  }
}
