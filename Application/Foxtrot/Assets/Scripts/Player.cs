/******************************************************************
 * File:            Player.cs
 * Author:          David Hite
 * Date Created:    10/2/2016
 * Date Modified:   10/15/2016
 * Description:
 * Contains the Player class, which controls the behavior of the 
 * Player object
 ******************************************************************/
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Player : MonoBehaviour {
  // Boundaries for player, set by camera view
  private float m_MinX;
  private float m_MaxX;
  private float m_MinY;
  private float m_MaxY;
  
  // AudioSources associated with player
  public AudioSource    m_FireAudio;
  public AudioSource    m_ExplosionAudio;

  // Health values
  private float m_MaxHealth;
  public float m_CurrentHealth;

  // Connection to health slider
  public Slider m_HealthSlider;

  // Singleton instance
  public static Player instance;

  // The ship being used
  public ShipBase m_Ship;

  // Called before Start()
  void Awake()
  {
    // Use this class as a Singleton instance
    if (instance == null)
    {
      instance = this;
    }
    else if (instance != this)
    {
      Destroy(gameObject);
    }

    // Ensures that the object persists between scenes
    DontDestroyOnLoad(gameObject);
  }

	// Use this for initialization
	void Start ()
  {
    // If we decide to have the camera move, then this should be in Update() instead
    GetCameraBounds();
    m_Ship = gameObject.AddComponent<Constitution>() as ShipBase;
    var sounds = GetComponents<AudioSource>();
    m_ExplosionAudio = sounds[1];
    m_FireAudio = sounds[0];
    
    m_HealthSlider = GetComponentInChildren<Slider>();
    m_MaxHealth = 10f;
    m_CurrentHealth = m_HealthSlider.value;
  }
	
	// Update is called once per frame
	void Update () {
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
      newPosition.x -= m_Ship.m_MoveSpeed;
      // Check for camera boundaries
      if (newPosition.x - m_Ship.m_SpriteWidthFromCenter < m_MinX)
        newPosition.x = m_MinX + m_Ship.m_SpriteWidthFromCenter;
    }
    if (Input.GetKey(KeyCode.RightArrow))
    {
      newPosition.x += m_Ship.m_MoveSpeed;
      if (newPosition.x + m_Ship.m_SpriteWidthFromCenter > m_MaxX)
        newPosition.x = m_MaxX - m_Ship.m_SpriteWidthFromCenter;
    }
    if (Input.GetKey(KeyCode.UpArrow))
    {
      newPosition.y += m_Ship.m_MoveSpeed;
      if (newPosition.y + m_Ship.m_SpriteHeightFromCenter > m_MaxY)
      {
        newPosition.y = m_MaxY - m_Ship.m_SpriteHeightFromCenter;
      }
    }
    if (Input.GetKey(KeyCode.DownArrow))
    {
      newPosition.y -= m_Ship.m_MoveSpeed;
      if (newPosition.y - m_Ship.m_SpriteHeightFromCenter < m_MinY)
      {
        newPosition.y = m_MinY + m_Ship.m_SpriteHeightFromCenter;
      }
    }

    return newPosition;
  }
}
