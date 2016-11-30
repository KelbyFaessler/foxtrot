using UnityEngine;
using System.Collections;

public class LaserEnemy : MonoBehaviour
{
  private float m_speed;
  private bool m_travelRight;
  private float m_LeftEdge;

  // Use this for initialization
  void Start()
  {
    m_speed = 0.25f;
    m_travelRight = false;
    m_LeftEdge = Globals.GetCameraBounds(gameObject).m_MinX;
  }

  // Update is called once per frame
  void Update()
  {
    var pos = transform.position;
    if (pos.x < m_LeftEdge)
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
    if (col.gameObject.tag == "Player")
    {
      // Destroy ourself (laser)
      Destroy(gameObject);
    }
  }

  void OnTriggerEnter2D(Collider2D other)
  {
    if (other.gameObject.tag == "Player")
    {
      // Destroy ourself (laser)
      Destroy(gameObject);
    }
  }
}
