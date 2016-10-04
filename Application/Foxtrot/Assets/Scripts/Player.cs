using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
  private const float m_MoveSpeed = 0.1f;
  private const float m_CameraSpeed = 0.05f;
  private float m_SpriteWidthFromCenter;
  private float m_SpriteHeightFromCenter;

  private float m_MinX;
  private float m_MaxX;
  private float m_MinY;
  private float m_MaxY;

  public SpriteRenderer SmallStar;

	// Use this for initialization
	void Start ()
  {
    var sprite = GetComponent<SpriteRenderer>();
    m_SpriteWidthFromCenter = (float)(sprite.bounds.size.x / 2);
    m_SpriteHeightFromCenter = (float)(sprite.bounds.size.y / 2);
  }
	
	// Update is called once per frame
	void Update () {
    Vector3 newPosition = transform.position;
    GetCameraBounds();
    CreateRandomStars();

    if (Input.GetKey(KeyCode.LeftArrow))
    {
      newPosition.x -= m_MoveSpeed;
      if (newPosition.x - m_SpriteWidthFromCenter < m_MinX)
        newPosition.x = m_MinX + m_SpriteWidthFromCenter;
    }
    if (Input.GetKey(KeyCode.RightArrow))
    {
      newPosition.x += m_MoveSpeed;
      if (newPosition.x + m_SpriteWidthFromCenter > m_MaxX)
        newPosition.x = m_MaxX - m_SpriteWidthFromCenter;
    }
    if (Input.GetKey(KeyCode.UpArrow))
    {
      newPosition.y += m_MoveSpeed;
      if (newPosition.y + m_SpriteHeightFromCenter > m_MaxY)
      {
        newPosition.y = m_MaxY - m_SpriteHeightFromCenter;
      }
    }
    if (Input.GetKey(KeyCode.DownArrow))
    {
      newPosition.y -= m_MoveSpeed;
      if (newPosition.y - m_SpriteHeightFromCenter < m_MinY)
      {
        newPosition.y = m_MinY + m_SpriteHeightFromCenter;
      }
    }

    transform.position = newPosition;
	}

  private void GetCameraBounds()
  {
    float camDistance = Vector3.Distance(transform.position, Camera.main.transform.position);
    Vector2 bottomCorner = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, camDistance));
    Vector2 topCorner = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, camDistance));

    m_MinX = bottomCorner.x;
    m_MaxX = topCorner.x;
    m_MinY = bottomCorner.y;
    m_MaxY = topCorner.y;
  }

  private void CreateRandomStars()
  {
    int rand = (int)Random.Range(0, 20);

    if (rand == 2)
    {
      //SmallStar starInstance;
      Vector3 position = new Vector3(m_MaxX + 4, Random.Range(m_MinY, m_MaxY));
      Instantiate(Resources.Load("SmallStar"), position, Quaternion.identity);
    }
  }
}
