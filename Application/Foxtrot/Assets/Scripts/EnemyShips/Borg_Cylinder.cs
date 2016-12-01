/*****************************************************************************************************************************************************************************************************************************
 * File: Borg_Cylinder.cs
 * Author: Austin Welborn
 * Date: 11/19/2016
 * Date Modified:
 * Description:  Child Class of EnemyBase for the Borg Cylinder enemy ship
 *
 */

using UnityEngine;
using System.Collections;

public class Borg_Cylinder : EnemyBase {
  // Use this for initialization
  void Awake () {
		m_BaseMaxHealth = 15f;
		m_MoveSpeed = 1.5f;
		m_Name = "Borg Cylinder";
		m_points = 20f;

		if (m_SpriteRenderer == null) {
			m_SpriteRenderer = GetComponent<SpriteRenderer> ();
			var temp = Resources.Load ("SpriteRenderers/borg_cylinder_renderer", typeof(SpriteRenderer)) as SpriteRenderer;
			m_SpriteRenderer.sprite = temp.sprite;
			m_SpriteRenderer.enabled = true;
		}
  }

  // Update is called once per frame
  void Update()
  {
		UpdateHealth ();
		// Get the enemy current position
		Vector2 position = transform.position;

		//Compute the enemy new position
		position = new Vector2(position.x - m_MoveSpeed * Time.deltaTime, position.y);

		//Update the ship with new position
		transform.position = position;

		// Find the left-most edge of the screen. If the ship is to the left of it, destroy it.
		var bounds = Globals.GetCameraBounds(gameObject);
		float minX = bounds.m_MinX;
		var pos = transform.position;
		if (pos.x < minX)
		{
			Destroy(gameObject);
			return;
		}

	    // Determine whether to fire laser
	    int rand = Random.Range(0, 60);
	    if (rand == 2)
	    {
	      Vector3 laserPosition = transform.position;
	      laserPosition.x = laserPosition.x - (m_SpriteWidthFromCenter * 2);
	      Instantiate(Resources.Load("Prefabs\\Weapons\\LaserEnemy"), laserPosition, Quaternion.identity);
	    }
  }
}
