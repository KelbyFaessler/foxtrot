/***************************************************************************************************************
* File:						EnemyBase.cs
* Author:					Austin Welborn
* Date Created:				10/16/2016
* Date Modified:			11/19/2016
* Description:
* Contains the EnemyBase class, which is to be used as an abstract class.  Children of this class will represent 
* different types of enemy ships for the Borg, Romulan, and Klingon ships and also the bosses from each race.
***************************************************************************************************************/
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnemyBase : MonoBehaviour {
  // Member variables
  public float m_SpriteWidthFromCenter;
  public float m_SpriteHeightFromCenter;
  public int m_DropNumber;
  public Slider m_HealthSlider;

  // Each ship can have a different starting max health and move speed
  public float m_BaseMaxHealth;
  public float m_MoveSpeed = 0.08f;
  private float m_HealthScale;

  public float m_CurrentHealth;

  // Each ship can have a name and rendering model
  public string m_Name;
  public SpriteRenderer m_SpriteRenderer;

  // Each enemy ship can have its own points for scoreboard
  public float m_points;

	// Use this for initialization
	void Start () {
    var sprite = GetComponent<SpriteRenderer>();
    m_SpriteWidthFromCenter = (float)(sprite.bounds.size.x / 2);
    m_SpriteHeightFromCenter = (float)(sprite.bounds.size.y / 2);
    m_HealthSlider = GetComponentInChildren<Slider>();
    m_CurrentHealth = m_BaseMaxHealth;
    m_HealthScale = m_BaseMaxHealth / 10f;
    // the drop number will determine which, if any, item an enemy drops
    m_DropNumber = (int)Random.Range(0, 10);
	}
	
	// Update is called once per frame
	void Update () {
    // If Update() is overwritten, always make sure to call UpdateHealth()!
    UpdateHealth();
	}

  protected void UpdateHealth()
  {
    m_HealthSlider.value = m_CurrentHealth / m_HealthScale;
    if (m_CurrentHealth < 1)
    {
      DestroySelf();
    }
  }

  public void ApplyDamage(int damage)
  {
    m_CurrentHealth -= damage;
  }

  protected void DestroySelf()
  {
    switch(m_DropNumber)
    {
      case 1:
        Instantiate(Resources.Load("Prefabs\\Drops\\HealthDrop"), transform.position, Quaternion.identity);
        break;
      case 2:
        Instantiate(Resources.Load("Prefabs\\Drops\\MultiShotDrop"), transform.position, Quaternion.identity);
        break;
      case 3:
        Instantiate(Resources.Load("Prefabs\\Drops\\BombDrop"), transform.position, Quaternion.identity);
        break;
      default:
        //Instantiate(Resources.Load("Prefabs\\Drops\\MultiShotDrop"), transform.position, Quaternion.identity);
        break;
    }
    Player.instance.AddScore((int)m_points);
    Destroy(gameObject);
  }
}
