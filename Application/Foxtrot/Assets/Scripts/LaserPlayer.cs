using UnityEngine;
using System.Collections;

public class LaserPlayer : MonoBehaviour {
  private float m_speed;
  private bool m_travelRight;
  private int m_AttackPoints = 1;

  // Use this for initialization
  void Start()
  {
    m_speed = 0.50f;
    m_travelRight = true;
  }

  // Update is called once per frame
  void Update()
  {
    // Find the right-most edge of the screen. If laser is to right of it, destroy it.
    float camDistance = Vector3.Distance(transform.position, Camera.main.transform.position);
    Vector2 bottomCorner = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, camDistance));
    float maxX = bottomCorner.x;

    var pos = transform.position;
    if (pos.x > maxX)
    {
      Destroy(gameObject);
      return;
    }

    // Otherwise, move the laser to the right by m_Speed
    if (m_travelRight)
    {
      pos.x += m_speed;
    }
    else
    {
      pos.x -= m_speed;
    }
    transform.position = pos;
  }

  /***********************************************************
  /** SetDirection
   Determines the direction the laser will travel (right or
   left)

   @param in : right The laser travels to the right (positive
   x direction)
  /***********************************************************/
  public void SetDirection(bool right)
  {
    m_travelRight = right;
  }

  /***********************************************************
  /** SetSpeed
   Determines the speed of the laser

   @param in : right The laser travels to the right (positive
   x direction)
  /***********************************************************/
  public void SetSpeed(float speed)
  {
    m_speed = speed;
  }

  void OnCollisionEnter2D(Collision2D col)
  {
    if (col.gameObject.tag == "DestructibleObject")
    {
      // Destroy object collided with
      Destroy(col.gameObject);
      // Destroy ourself (laser)
      Destroy(gameObject);
    }
  }

  void OnTriggerEnter2D(Collider2D other)
  {
    if (other.gameObject.tag == "DestructibleObject")
    {
      // Destroy object collided with
      EnemyBase enemy = other.gameObject.GetComponent<EnemyBase>();
      enemy.ApplyDamage(m_AttackPoints);
      // Destroy ourself (laser)
      Destroy(gameObject);
    }
    if (other.gameObject.tag == "Asteroid")
    {
      Asteroid asteroid = other.gameObject.GetComponent<Asteroid>();
      asteroid.ApplyDamage(m_AttackPoints);
      Destroy(gameObject);
    }
  }
}
