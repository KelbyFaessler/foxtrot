/*****************************************************************************************************************************************************************************************************************************
 * File: Borg_Cube.cs
 * Author: Austin Welborn
 * Date: 11/21/2016
 * Date Modified: 
 * Description:  Child Class of EnemyBase for the Borg Cube boss enemy ship
 *
 */

using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Borg_Cube : EnemyBase {
  // Use this for initialization
  bool m_GoingUp;
  void Awake () {
		m_BaseMaxHealth = 125f;
		m_MoveSpeed = 4.0f;
		m_Name = "Borg Cube";
		m_points = 1000f;
    m_GoingUp = true;

		if (m_SpriteRenderer == null) {
			m_SpriteRenderer = GetComponent<SpriteRenderer> ();
			var temp = Resources.Load ("SpriteRenderers/borg_cube_renderer", typeof(SpriteRenderer)) as SpriteRenderer;
			m_SpriteRenderer.sprite = temp.sprite;
			m_SpriteRenderer.enabled = true;
		}
	}
    
  void Update()
  {

		UpdateHealth();
		var bounds = Globals.GetCameraBounds (gameObject);
		float maxX = bounds.m_MaxX;

    // Get the enemy current position
    Vector2 position = transform.position;
		if (position.x > maxX - m_SpriteWidthFromCenter * 2)
    {
      //Compute the enemy new position
      position = new Vector2(position.x - m_MoveSpeed * Time.deltaTime, position.y);

      //Update the ship with new position
      transform.position = position;
    }
    else
    {
      if (position.y < bounds.m_MinY + m_SpriteHeightFromCenter)
        m_GoingUp = true;
      else if (position.y > bounds.m_MaxY - m_SpriteHeightFromCenter)
        m_GoingUp = false;

      if (m_GoingUp)
        position = new Vector2(position.x, m_MoveSpeed * Time.deltaTime + position.y);
      else
        position = new Vector2(position.x, position.y - m_MoveSpeed * Time.deltaTime);

      transform.position = position;
    }

    // Determine whether to fire laser
    int rand = Random.Range(0, 50);
    if (rand == 2)
    {
      Vector3 laserPosition = transform.position;
      laserPosition.x = laserPosition.x - (m_SpriteWidthFromCenter / 2);
      Instantiate(Resources.Load("Prefabs\\Weapons\\LaserEnemy"), laserPosition, Quaternion.identity);
    }
  }

  protected override void DestroySelf()
  {
    Instantiate(Resources.Load("Prefabs\\CustomExplosion"), transform.position, Quaternion.identity);
    Instantiate(Resources.Load("Prefabs\\Drops\\HealthDrop"), transform.position, Quaternion.identity);
    Instantiate(Resources.Load("Prefabs\\Drops\\BombDrop"), transform.position, Quaternion.identity);
    Instantiate(Resources.Load("Prefabs\\Drops\\BombDrop"), transform.position, Quaternion.identity);
    Instantiate(Resources.Load("Prefabs\\Drops\\MultiShotDrop"), transform.position, Quaternion.identity);
    gameObject.SetActive(false);
    Player.instance.AddScore((int)m_points);
    Invoke("LoadLevelThree", 7f);
  }

  void LoadLevelThree()
  {
    SceneManager.LoadSceneAsync("LevelThreeTransition");
    Destroy(gameObject);
  }
}
