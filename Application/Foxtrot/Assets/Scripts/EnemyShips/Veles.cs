/*****************************************************************************************************************************************************************************************************************************
 * File: Veles.cs
 * Author: Austin Welborn
 * Date: 10/12/2016
 * Date Modified: 11/19/2016
 * Description:  Child Class of EnemyBase for the Veles enemy ship
 *
 */

using UnityEngine;
using System.Collections;

public class Veles : EnemyBase {

	// Use this for initialization
	void Awake () {
		m_BaseMaxHealth = 3f;
		m_MoveSpeed = 2.3f;
		m_Name = "Veles";
		m_points = 15f;

		if (m_SpriteRenderer == null) {
			m_SpriteRenderer = GetComponent<SpriteRenderer> ();
			var temp = Resources.Load ("SpriteRenderers/veles_renderer", typeof(SpriteRenderer)) as SpriteRenderer;
			m_SpriteRenderer.sprite = temp.sprite;
			m_SpriteRenderer.enabled = true;
		}
	}

  void Update()
  {
    UpdateHealth();
    // Get the enemy current position
    Vector2 position = transform.position;

    //Compute the enemy new position
    position = new Vector2(position.x - m_MoveSpeed * Time.deltaTime, position.y);

    //Update the ship with new position
    transform.position = position;

    // Determine whether to fire laser
    int rand = Random.Range(0, 75);
    if (rand == 2)
    {
      Vector3 laserPosition = transform.position;
      laserPosition.x = laserPosition.x - (m_SpriteWidthFromCenter * 2);
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
