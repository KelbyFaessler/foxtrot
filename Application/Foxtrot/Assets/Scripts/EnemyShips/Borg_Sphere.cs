/*****************************************************************************************************************************************************************************************************************************
 * File: Borg_Sphere.cs
 * Author: Austin Welborn
 * Date: 10/16/2016
 * Date Modified:
 * Description:  Child Class of EnemyBase for the Borg Sphere enemy ship
 *
 */

using UnityEngine;
using System.Collections;

public class Borg_Sphere : EnemyBase {
  // Use this for initialization
  void Awake () {
		m_BaseMaxHealth = 4f;
		m_MoveSpeed = 1.5f;
		m_Name = "Borg Sphere";
		m_points = 20f;

		if (m_SpriteRenderer == null) {
			m_SpriteRenderer = GetComponent<SpriteRenderer> ();
			var temp = Resources.Load ("SpriteRenderers/borg_sphere_renderer", typeof(SpriteRenderer)) as SpriteRenderer;
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
		position = new Vector2(position.x - m_MoveSpeed * Time.deltaTime, Random.Range(-2,2)*Time.deltaTime*m_MoveSpeed + position.y);
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
  }
}
