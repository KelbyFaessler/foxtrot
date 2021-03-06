/*****************************************************************************************************************************************************************************************************************************
 * File: Neghvar.cs
 * Author: Austin Welborn
 * Date: 11/27/2016
 * Date Modified: 12/1/2016 
 * Description:  Child Class of EnemyBase for the Negh'var boss enemy ship
 *
 */

using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Neghvar : EnemyBase {
  // Use this for initialization
	float Ship_position;
  void Awake () {
		m_BaseMaxHealth = 140f;
		m_MoveSpeed = 4.5f;
		m_Name = "Negh'var";
		m_points = 2000f;
		Ship_position = 1f;
	}
    
  void Update()
  {

		UpdateHealth();

		var bounds = Globals.GetCameraBounds (gameObject);
		float maxX = bounds.m_MaxX;;
		float maxY = bounds.m_MaxY;
    float minY = bounds.m_MinY;

    // Get the enemy current position
    var position = transform.position;
		if (position.x > (maxX - m_SpriteWidthFromCenter))
    {
      //Compute the en+emy new position
      position = new Vector2(position.x - m_MoveSpeed * Time.deltaTime, position.y);

      //Update the ship with new position
      transform.position = position;
    }
    else
    {
			if ((position.y + m_SpriteHeightFromCenter) > maxY) {
				Ship_position = 2;
			}

			if ((position.x - m_SpriteHeightFromCenter) > maxX && Ship_position == 2) {
				Ship_position = 3; 
			}
			if ((position.y - m_SpriteHeightFromCenter) < minY) {
				Ship_position = 4;
			}

			if ((position.x - m_SpriteWidthFromCenter) > maxX && Ship_position == 4) {
				Ship_position = 1;
			}
			switch ((int)Ship_position)
			{
			case 1:
				position = new Vector2 (position.x - m_MoveSpeed * Time.deltaTime, position.y + m_MoveSpeed * Time.deltaTime);
				break;
			case 2:
				position = new Vector2 (position.x + m_MoveSpeed * Time.deltaTime, position.y - m_MoveSpeed * Time.deltaTime);
				break;
        
			case 3:
				position = new Vector2 (position.x - m_MoveSpeed * Time.deltaTime, position.y - m_MoveSpeed * Time.deltaTime);
				break;
			case 4:
				position = new Vector2 (position.x + m_MoveSpeed * Time.deltaTime, position.y + m_MoveSpeed * Time.deltaTime);
				break;
			default:
				position = new Vector2 (position.x - m_MoveSpeed * Time.deltaTime, position.y + m_MoveSpeed * Time.deltaTime);
				break;
			}
      transform.position = position;
    }

    // Determine whether to fire laser
    int rand = Random.Range(0, 50);
    if (rand == 2)
    {
      Vector3 laserPosition = transform.position;
      laserPosition.x = laserPosition.x - m_SpriteWidthFromCenter;
      Instantiate(Resources.Load("Prefabs\\Weapons\\LaserEnemy"), laserPosition, Quaternion.identity);
    }
  }
  
  protected override void DestroySelf()
  {
    gameObject.SetActive(false);
    Player.instance.AddScore((int)m_points);
    Invoke("LoadWinnerScene", 3f);
  }

  void LoadWinnerScene()
  {
    SceneManager.LoadSceneAsync("WinnerScene");
    Destroy(gameObject);
  }
}
