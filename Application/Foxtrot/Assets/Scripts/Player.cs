/******************************************************************
 * File:            Player.cs
 * Author:          David Hite
 * Date Created:    10/2/2016
 * Date Modified:   10/14/2016
 * Description:
 * Contains the Player class, which controls the behavior of the 
 * Player object
 ******************************************************************/
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Player : MonoBehaviour {
  // Member variables
  private const float m_MoveSpeed = 0.1f;
  private const float m_CameraSpeed = 0.05f;
  private float m_SpriteWidthFromCenter;
  private float m_SpriteHeightFromCenter;

  // Health values
  private float m_MaxHealth;
  public float m_CurrentHealth;

  // Boundaries for player, set by camera view
  private float m_MinX;
  private float m_MaxX;
  private float m_MinY;
  private float m_MaxY;
  
  // AudioSources associated with player
  public AudioSource    m_FireAudio;
  public AudioSource    m_ExplosionAudio;

  // Connection to health slider
  public Slider m_HealthSlider;

	// Use this for initialization
	void Start ()
  {
    // Get the size of the sprite (half), for checking boundaries
    var sprite = GetComponent<SpriteRenderer>();
    m_SpriteWidthFromCenter = (float)(sprite.bounds.size.x / 2);
    m_SpriteHeightFromCenter = (float)(sprite.bounds.size.y / 2);

    // If we decide to have the camera move, then this should be in Update() instead
    GetCameraBounds();

    var sounds = GetComponents<AudioSource>();
    m_ExplosionAudio = sounds[1];
    m_FireAudio = sounds[0];
    
    m_HealthSlider = GetComponentInChildren<Slider>();//GetComponent<Slider>();
    m_MaxHealth = 10f;
    m_CurrentHealth = m_HealthSlider.value;
  }
	
	// Update is called once per frame
	void Update () {
    // TODO: this should probably go in a different script,
    // maybe one associated with the scene or camera
    CreateRandomStars();

    // Get movement input from player
    transform.position = GetPlayerControls();

    if (Input.GetKeyDown(KeyCode.Space))
    {
      m_FireAudio.Play();
    }
    if (Input.GetKeyDown(KeyCode.F))
    {
      m_ExplosionAudio.Play();
    }

    if (Input.GetKeyDown(KeyCode.V))
    {
      if (m_CurrentHealth < m_MaxHealth)
      {
        m_CurrentHealth += 1f;
      }
    }
    if (Input.GetKeyDown(KeyCode.C))
    {
      if (m_CurrentHealth > 0)
      {
        m_CurrentHealth -= 1f;
      }
    }

    m_HealthSlider.value = m_CurrentHealth;
	}

  /***********************************************************
  /** GetCameraBounds
   Gets the minimum and maximum x and y values for the camera
   view.
  /***********************************************************/
  private void GetCameraBounds()
  {
    var bounds = Globals.GetCameraBounds(gameObject);
    m_MinX = bounds.m_MinX;
    m_MaxX = bounds.m_MaxX;
    m_MinY = bounds.m_MinY;
    m_MaxY = bounds.m_MaxY;
  }

  /***********************************************************
  /** GetPlayerControls
   Checks for directional movement from the keyboard and moves
   the player object accordingly
  /***********************************************************/
  Vector3 GetPlayerControls()
  {
    Vector3 newPosition = transform.position;
    if (Input.GetKey(KeyCode.LeftArrow))
    {
      newPosition.x -= m_MoveSpeed;
      // Check for camera boundaries
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

    return newPosition;
  }

  /***********************************************************
  /** CreateRandomStars
   Instantiates a SmallStar object just to the right of the
   camera view at a random y position.
  /***********************************************************/
  private void CreateRandomStars()
  {
    // Do not generate a star at every update; reduce probability of generation
    int rand = (int)Random.Range(0, 20);
    int rand2 = (int)Random.Range(0, 70);

    if (rand == 2)
    {
      Vector3 position = new Vector3(m_MaxX, Random.Range(m_MinY, m_MaxY));
      Instantiate(Resources.Load("SmallStar"), position, Quaternion.identity);
    }
    if (rand2 == 3)
    {
      Vector3 position = new Vector3(m_MaxX, Random.Range(m_MinY, m_MaxY));
      Instantiate(Resources.Load("BigStar"), position, Quaternion.identity);
    }
  }
}
