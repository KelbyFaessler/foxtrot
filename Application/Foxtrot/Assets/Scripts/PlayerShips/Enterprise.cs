/******************************************************************
 * File:            Enterprise.cs
 * Author:          David Hite
 * Date Created:    10/19/2016
 * Date Modified:   10/19/2016
 * Description:
 * Contains the Enterprise class. This class represents the 
 * USS Enterprise starship.
 ******************************************************************/
using UnityEngine;
using System.Collections;

public class Enterprise : ShipBase
{
  // Use this for initialization
  void Awake()
  {
    m_BaseMaxHealth = 10f;
    m_MoveSpeed = 0.3f;
    m_Name = "USS Enterprise";
    m_Acceleration = 0.2f;

    if (m_SpriteRenderer == null)
    {
      m_SpriteRenderer = GetComponent<SpriteRenderer>();
      var temp = Resources.Load("SpriteRenderers/uss_enterprise_renderer", typeof(SpriteRenderer)) as SpriteRenderer;
      m_SpriteRenderer.sprite = temp.sprite;
      m_SpriteRenderer.enabled = true;
    }
  }
}
