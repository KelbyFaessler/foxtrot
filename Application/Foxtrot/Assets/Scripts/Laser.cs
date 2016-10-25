using UnityEngine;
using System.Collections;

public class Laser : MonoBehaviour {
  private const float m_Speed = 0.50f;

  // Use this for initialization
  void Start()
  {

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
    pos.x += m_Speed;
    transform.position = pos;
  }

  void OnCollisionEnter(Collision col)
  {
    if (col.gameObject.tag == "DestructibleObject")
    {
      // Destroy object collided with
      Destroy(col.gameObject);
      // Destroy ourself (laser)
      Destroy(gameObject);
    }
  }
}
