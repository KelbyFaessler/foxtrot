/*****************************************************************************************************************************************************************************************************************************
 * File: Borg_Cylinder.cs
 * Author: Austin Welborn
 * Date: 10/16/2016
 * Date Modified:
 * Description:  Child Class of EnemyBase for the Borg Cylinder enemy ship
 *
 */

using UnityEngine;
using System.Collections;

public class Borg_Cylinder : EnemyBase {
  // Use this for initialization
  void Awake () {
		m_BaseMaxHealth = 8f;
		m_MoveSpeed = 3.0f;
		m_Name = "Borg Cylinder";
		m_points = 25f;

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
		UpdateHealth();
		// Get the enemy current position
		Vector2 position = transform.position;

		//Compute the enemy new position
		position = new Vector2(position.x - m_MoveSpeed * Time.deltaTime, Mathf.PingPong(Time.time, 5));
		transform.position = position;

    // Determine whether to fire laser
    int rand = Random.Range(0, 75);
    if (rand == 2)
    {
      Vector3 laserPosition = transform.position;
      laserPosition.x = laserPosition.x - (m_SpriteWidthFromCenter * 2);
      Instantiate(Resources.Load("Prefabs\\Weapons\\LaserEnemy"), laserPosition, Quaternion.identity);
    }
  }
}
