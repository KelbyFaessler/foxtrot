/******************************************************************
 * File:            Constitution.cs
 * Author:          David Hite
 * Date Created:    10/15/2016
 * Date Modified:   10/15/2016
 * Description:
 * Contains the Constitution class. This class represents the 
 * Constitution starship.
 ******************************************************************/
using UnityEngine;
using System.Collections;

public class Constitution : ShipBase {

	// Use this for initialization
	void Awake () {
    m_BaseMaxHealth = 15f;
    m_MoveSpeed     = 0.05f;
    m_Name          = "Constitution";
    
    if (m_SpriteRenderer == null)
    {
      m_SpriteRenderer = GetComponent<SpriteRenderer>();
      var temp = Resources.Load("SpriteRenderers/constitution_renderer", typeof(SpriteRenderer)) as SpriteRenderer;
      m_SpriteRenderer.sprite = temp.sprite;
      m_SpriteRenderer.enabled = true;
    }
  }
}
