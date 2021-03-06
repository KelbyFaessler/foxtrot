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
	// Use this for initialization
	void Awake () {
		m_BaseMaxHealth = 8f;
		m_MoveSpeed = 3f;
		m_Name = "Klothos";
		m_points = 75f;
	}

	void Update()
	{
		UpdateHealth();
		// Get the enemy current position
		Vector2 position = transform.position;

		// Compute the enemy new position
		position = new Vector2(position.x - m_MoveSpeed * Time.deltaTime, Mathf.Sin(Time.time)*Time.deltaTime*m_MoveSpeed + position.y);

		// Update the ship with new position
		transform.position = position;

		// Determine whether to fire laser
		int rand = Random.Range(0, 75);
		if (rand == 2)
		{
			Vector3 laserPosition = transform.position;
			laserPosition.x = laserPosition.x - m_SpriteWidthFromCenter;
			Instantiate(Resources.Load("Prefabs\\Weapons\\LaserEnemy"), laserPosition, Quaternion.identity);
		}

		// Find the left-most edge of the screen. If the ship is to the left of it, destroy it.
		var bounds = Globals.GetCameraBounds(gameObject);
		float minX = bounds.m_MinX;
		var pos = transform.position;
		if (pos.x < minX)
		{
			Destroy(gameObject);
			return;
		}
	}
}
