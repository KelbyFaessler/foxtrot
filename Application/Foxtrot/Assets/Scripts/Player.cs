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
  // deceleration factor
  const float DECEL_FACTOR = 0.6f;
  public enum EShip
  {
    Constitution = 0,
    Galaxy,
    Enterprise
  }

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
  public bool m_Visible;

  // Connection to health slider
  public Slider m_HealthSlider;

  public float m_HorizontalSpeed;
  public float m_VerticalSpeed;

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

    m_Ship = gameObject.AddComponent<Galaxy>() as ShipBase;
  }

	// Use this for initialization
	void Start ()
  {
    // If we decide to have the camera move, then this should be in Update() instead
    GetCameraBounds();
    var sounds = GetComponents<AudioSource>();
    m_ExplosionAudio = sounds[1];
    m_FireAudio = sounds[0];
    
    m_HealthSlider = GetComponentInChildren<Slider>();
    m_MaxHealth = 10f;
    m_CurrentHealth = m_HealthSlider.value;
    m_HorizontalSpeed = 0f;
    m_VerticalSpeed = 0f;
    m_Visible = true;
  }
	
	// Update is called once per frame
	void Update () {
    // this is true in the menu, when we hide the player object
    if (m_Ship == null || m_Visible == false)
      return;

    if (Input.GetKey("escape"))
    {
      Application.Quit();
    }

    // Get movement input from player
    transform.position = GetPlayerControls();

    if (Input.GetKeyDown(KeyCode.Space))
    {
      m_FireAudio.Play();
      Vector3 laserPosition = transform.position;
      laserPosition.x = laserPosition.x + m_Ship.m_SpriteWidthFromCenter;
      Instantiate(Resources.Load("Prefabs\\BlueLaser"), laserPosition, Quaternion.identity);
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
  /** SetShip
   Sets m_Ship to the selected ship type

   @param in : eShipType  The type of ship selected
  /***********************************************************/
  public void SetShip(EShip eShipType)
  {
    switch (eShipType)
    {
      case EShip.Constitution:
        {
          m_Ship = gameObject.AddComponent<Constitution>() as ShipBase;
          break;
        }
      case EShip.Galaxy:
        {
          m_Ship = gameObject.AddComponent<Galaxy>() as ShipBase;
          break;
        }
      case EShip.Enterprise:
        {
          m_Ship = gameObject.AddComponent<Enterprise>() as ShipBase;
          break;
        }
      default:
        {
          m_Ship = gameObject.AddComponent<Enterprise>() as ShipBase;
          break;
        }
    }
  }

  /***********************************************************
  /** SetVisible
   Moves the player object off the screen if isVisible is false,
   otherwise moves it to the left edge of the screen

   @param in : isVisible  Hides player when false
  /***********************************************************/
  public void SetVisible(bool isVisible)
  {
    GetCameraBounds();
    var newPosition = transform.position;
    if (isVisible)
      newPosition.x = m_MinX + m_Ship.m_SpriteWidthFromCenter;
    else
      newPosition.x = m_MinX - 500;
    transform.position = newPosition;
    m_Visible = isVisible;
  }

  /***********************************************************
  /** GetCameraBounds
   Gets the minimum and maximum x and y values for the camera
   view.
  /***********************************************************/
  public void GetCameraBounds()
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
    bool horizontalAcceleration = false;
    bool verticalAcceleration = false;
    if (Input.GetKey(KeyCode.LeftArrow))
    {
      horizontalAcceleration = true;
      m_HorizontalSpeed -= m_Ship.m_Acceleration;
      if (m_HorizontalSpeed < -(m_Ship.m_MoveSpeed))
        m_HorizontalSpeed = -(m_Ship.m_MoveSpeed);
    }
    if (Input.GetKey(KeyCode.RightArrow))
    {
      horizontalAcceleration = true;
      m_HorizontalSpeed += m_Ship.m_Acceleration;
      if (m_HorizontalSpeed > m_Ship.m_MoveSpeed)
        m_HorizontalSpeed = m_Ship.m_MoveSpeed;
    }
    if (Input.GetKey(KeyCode.UpArrow))
    {
      verticalAcceleration = true;
      m_VerticalSpeed += m_Ship.m_Acceleration;
      if (m_VerticalSpeed > m_Ship.m_MoveSpeed)
        m_VerticalSpeed = m_Ship.m_MoveSpeed;
    }
    if (Input.GetKey(KeyCode.DownArrow))
    {
      verticalAcceleration = true;
      m_VerticalSpeed -= m_Ship.m_Acceleration;
      if (m_VerticalSpeed < -(m_Ship.m_MoveSpeed))
        m_VerticalSpeed = -(m_Ship.m_MoveSpeed);
    }

    if (!horizontalAcceleration)
    {
      if (m_HorizontalSpeed < 0)
        m_HorizontalSpeed += (m_Ship.m_Acceleration * DECEL_FACTOR);
      else if (m_HorizontalSpeed > 0)
        m_HorizontalSpeed -= (m_Ship.m_Acceleration * DECEL_FACTOR);
      if (Mathf.Abs(m_HorizontalSpeed) < 0.05)
        m_HorizontalSpeed = 0f;
    }
    if (!verticalAcceleration)
    {
      if (m_VerticalSpeed < 0)
        m_VerticalSpeed += (m_Ship.m_Acceleration * DECEL_FACTOR);
      else if (m_VerticalSpeed > 0)
        m_VerticalSpeed -= (m_Ship.m_Acceleration * DECEL_FACTOR);
      if (Mathf.Abs(m_VerticalSpeed) < 0.05f)
        m_VerticalSpeed = 0f;
    }

    newPosition.x += m_HorizontalSpeed;
    newPosition.y += m_VerticalSpeed;

    // Check for camera boundaries
    if (newPosition.x - m_Ship.m_SpriteWidthFromCenter < m_MinX)
    {
      newPosition.x = m_MinX + m_Ship.m_SpriteWidthFromCenter;
      m_HorizontalSpeed = 0f;
    }
    if (newPosition.x + m_Ship.m_SpriteWidthFromCenter > m_MaxX)
    {
      newPosition.x = m_MaxX - m_Ship.m_SpriteWidthFromCenter;
      m_HorizontalSpeed = 0f;
    }
    if (newPosition.y + m_Ship.m_SpriteHeightFromCenter > m_MaxY)
    {
      newPosition.y = m_MaxY - m_Ship.m_SpriteHeightFromCenter;
      m_VerticalSpeed = 0f;
    }
    if (newPosition.y - m_Ship.m_SpriteHeightFromCenter < m_MinY)
    {
      newPosition.y = m_MinY + m_Ship.m_SpriteHeightFromCenter;
      m_VerticalSpeed = 0f;
    }

    return newPosition;
  }
}
