/*****************************************************************************************************************************************************************************************************************************
 * File: Klothos.cs
 * Author: Austin Welborn
 * Date: 11/27/2016
 * Date Modified: 11/30/2016
 * Description:  Child Class of EnemyBase for the Klothos enemy ship
 *
 */

using UnityEngine;
using System.Collections;

public class Klothos : EnemyBase {
	bool m_GoingUp;
	// Use this for initialization
	void Awake () {
		m_BaseMaxHealth = 55f;
		m_MoveSpeed = 3f;
		m_Name = "Klothos";
		m_points = 75f;
		m_GoingUp = true;

		if (m_SpriteRenderer == null) {
			m_SpriteRenderer = GetComponent<SpriteRenderer> ();
			var temp = Resources.Load ("SpriteRenderers/klothos_renderer", typeof(SpriteRenderer)) as SpriteRenderer;
			m_SpriteRenderer.sprite = temp.sprite;
			m_SpriteRenderer.enabled = true;
		}
	}

  void Update()
  {
    UpdateHealth();

		var bounds = Globals.GetCameraBounds(gameObject);
		float minY = bounds.m_MinY;
		float maxY = bounds.m_MaxY;

    // Get the enemy current position
    Vector2 position = transform.position;

	if (position.y < bounds.m_MinY + m_SpriteHeightFromCenter)
	{
		m_GoingUp = true;
	}
	else if (position.y > bounds.m_MaxY - m_SpriteHeightFromCenter)
	{
			m_GoingUp = false;
	}
	if (m_GoingUp)
		{
			position = new Vector2(position.x - m_MoveSpeed * Time.deltaTime, m_MoveSpeed * Time.deltaTime + position.y);
		}
		else
		{
		position = new Vector2(position.x, position.y - m_MoveSpeed * Time.deltaTime);
		}
    //Update the ship with new position
    transform.position = position;

    // Determine whether to fire laser
    int rand = Random.Range(0, 60);
    if (rand == 2)
    {
      Vector3 laserPosition = transform.position;
      laserPosition.x = laserPosition.x - (m_SpriteWidthFromCenter * 2);
      Instantiate(Resources.Load("Prefabs\\Weapons\\LaserEnemy"), laserPosition, Quaternion.identity);
    }
    // Find the left-most edge of the screen. If the ship is to the left of it, destroy it.
    float minX = bounds.m_MinX;
    var pos = transform.position;
    if (pos.x < minX)
    {
      Destroy(gameObject);
      return;
    }
  }
}
