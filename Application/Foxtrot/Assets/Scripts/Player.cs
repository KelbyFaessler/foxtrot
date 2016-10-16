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
    float camDistance = Vector3.Distance(transform.position, Camera.main.transform.position);
    Vector2 bottomCorner = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, camDistance));
    Vector2 topCorner = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, camDistance));

    m_MinX = bottomCorner.x;
    m_MaxX = topCorner.x;
    m_MinY = bottomCorner.y;
    m_MaxY = topCorner.y;
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
      Instantiate(Resources.Load("Prefabs/SmallStar"), position, Quaternion.identity);
    }
    if (rand2 == 3)
    {
      Vector3 position = new Vector3(m_MaxX, Random.Range(m_MinY, m_MaxY));
      Instantiate(Resources.Load("Prefabs/BigStar"), position, Quaternion.identity);
    }
  }
}
