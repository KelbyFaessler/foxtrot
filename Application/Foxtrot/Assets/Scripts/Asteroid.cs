using UnityEngine;
using System.Collections;

public class Asteroid : MonoBehaviour {
  private const float m_Speed = 0.01f;
  private bool m_IsDestroyed;
  private int m_DestructionFrames;
  private int m_Durability;
  private int m_DropNumber;

  // Use this for initialization
  void Start () {
    m_IsDestroyed = false;
    m_DestructionFrames = 0;
    m_Durability = Random.Range(3, 7);
    m_DropNumber = Random.Range(0, 10);
	}
	
	// Update is called once per frame
	void Update () {
    // Find the left-most edge of the screen. If a planet is to the left of it, destroy it.
    float camDistance = Vector3.Distance(transform.position, Camera.main.transform.position);
    Vector2 bottomCorner = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, camDistance));
    float minX = bottomCorner.x;

    var pos = transform.position;
    if (pos.x < minX)
    {
      Destroy(gameObject);
      return;
    }

    // Otherwise, move the star to the left by m_StarSpeed
    pos.x -= m_Speed;
    
    transform.Rotate(new Vector3(0, 0, 0.4f));
    transform.position = pos;

    if (m_IsDestroyed)
    {
      transform.Rotate(new Vector3(9, 0, 0));
      m_DestructionFrames++;
      if (m_DestructionFrames == 64)
      {
        Destroy(gameObject);
        switch (m_DropNumber)
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
            break;
        }
      }
    }
  }

  public void ApplyDamage(int damage)
  {
    m_Durability -= damage;
    if (m_Durability < 1)
    {
      DestroyAsteroid();
    }
  }

  private void DestroyAsteroid()
  {
    m_IsDestroyed = true;
  }
}
