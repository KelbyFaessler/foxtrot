/******************************************************************
 * File:            Player.cs
 * Author:          David Hite
 * Date Created:    10/2/2016
 * Date Modified:   11/10/2016
 * Description:
 * Contains the Player class, which controls the behavior of the 
 * Player object
 ******************************************************************/
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
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

  private float m_TimeOfDeath;
  private bool m_Destroyed;

  // AudioSources associated with player
  public AudioSource    m_FireAudio;
  public AudioSource    m_ExplosionAudio;
  public AudioSource    m_ItemPickupAudio;

  // Health values
  private float m_MaxHealth;
  public float m_CurrentHealth;
  private float m_HealthScale;

  public bool m_Visible;

  public int m_Score;
  private Text m_ScoreText;

  // Connection to health slider
  public Slider m_HealthSlider;

  public float m_HorizontalSpeed;
  public float m_VerticalSpeed;

  // Singleton instance
  public static Player instance;

  // The ship being used
  public ShipBase m_Ship;

  // Weapon references
  private int m_numBombs;
  private Text m_BombText;
  private int m_numMultiShots;
  private Text m_MultiShotText;

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
    m_ItemPickupAudio = sounds[2];
    
    m_HealthSlider = GetComponentInChildren<Slider>();
    m_MaxHealth = 10f;
    m_HealthScale = m_MaxHealth / 100f;
    m_CurrentHealth = m_HealthSlider.value * m_HealthScale;
    m_HorizontalSpeed = 0f;
    m_VerticalSpeed = 0f;
    m_Visible = true;

    m_Score = 0;
    Text[] textArray = HUDCanvas.instance.GetComponentsInChildren<Text>();
    m_ScoreText = textArray[1];
    m_numBombs  = 0;
    m_BombText  = textArray[2];
    m_numMultiShots = 0;
    m_MultiShotText = textArray[3];

    m_TimeOfDeath = -1f;
    m_Destroyed = false;
  }
	
	// Update is called once per frame
	void Update () {
    // this is true in the menu, when we hide the player object
    if (m_Ship == null || m_Visible == false)
      return;

    if (Input.GetKey("escape"))
    {
      ResetPlayer();
      HUDCanvas.instance.gameObject.SetActive(false);
      SceneManager.LoadSceneAsync("MainMenu");
    }

    if (m_Destroyed)
    {
      float deltaTimeOfDeath = Time.time - m_TimeOfDeath;
      if (deltaTimeOfDeath >= 3f)
      {
        SceneManager.LoadSceneAsync("MainMenu");
        Destroy(HUDCanvas.instance.gameObject);
        Destroy(gameObject);
      }
    }

    // Get movement input from player
    transform.position = GetPlayerControls();

    if (Input.GetKeyDown(KeyCode.Space))
    {
      if (m_numMultiShots <= 0) {
        m_FireAudio.Play();
        Vector3 laserPosition = transform.position;
        laserPosition.x = laserPosition.x + m_Ship.m_SpriteWidthFromCenter;
        Instantiate(Resources.Load("Prefabs\\Weapons\\LaserPlayer"), laserPosition, Quaternion.identity);
      } else { //fire multishot
        //straight shots
        m_FireAudio.Play();
        Vector3 midLaserPosition = transform.position;
        midLaserPosition.x = midLaserPosition.x + m_Ship.m_SpriteWidthFromCenter;
        Instantiate(Resources.Load("Prefabs\\Weapons\\LaserPlayerDiagonal"), midLaserPosition, Quaternion.identity);
        Vector3 topLaserPosition = transform.position;
        topLaserPosition.x = topLaserPosition.x + 0.5f * m_Ship.m_SpriteWidthFromCenter;
        topLaserPosition.y = topLaserPosition.y + 0.25f * m_Ship.m_SpriteWidthFromCenter;
        Instantiate(Resources.Load("Prefabs\\Weapons\\LaserPlayerDiagonal"), topLaserPosition, Quaternion.identity);
        Vector3 botLaserPosition = transform.position;
        botLaserPosition.x = botLaserPosition.x + 0.5f * m_Ship.m_SpriteWidthFromCenter;
        botLaserPosition.y = botLaserPosition.y - 0.25f * m_Ship.m_SpriteWidthFromCenter;
        Instantiate(Resources.Load("Prefabs\\Weapons\\LaserPlayerDiagonal"), botLaserPosition, Quaternion.identity);
        m_numMultiShots -= 1;
        m_MultiShotText.text = string.Format("{0}", m_numMultiShots);
        //diagonal shots
        Vector3 topDiagLaserPosition = transform.position;
        topDiagLaserPosition.x = topDiagLaserPosition.x + 0.5f * m_Ship.m_SpriteWidthFromCenter;
        topDiagLaserPosition.y = topDiagLaserPosition.y + 0.75f * m_Ship.m_SpriteWidthFromCenter;
        Quaternion topDiagLaserRotation = transform.rotation;
        topDiagLaserRotation.z = topDiagLaserRotation.z + 10 * Mathf.Deg2Rad;
        Instantiate(Resources.Load("Prefabs\\Weapons\\LaserPlayerDiagonal"), topDiagLaserPosition, topDiagLaserRotation);
        Vector3 botDiagLaserPosition = transform.position;
        botDiagLaserPosition.x = botDiagLaserPosition.x + 0.5f * m_Ship.m_SpriteWidthFromCenter;
        botDiagLaserPosition.y = botDiagLaserPosition.y - 0.75f * m_Ship.m_SpriteWidthFromCenter;
        Quaternion botDiagLaserRotation = transform.rotation;
        botDiagLaserRotation.z = botDiagLaserRotation.z - 10 * Mathf.Deg2Rad;
        Instantiate(Resources.Load("Prefabs\\Weapons\\LaserPlayerDiagonal"), botDiagLaserPosition, botDiagLaserRotation);
      }
    }

    bool noCurrentBomb = (GameObject.Find("bomb") == null);
    if (Input.GetKeyDown(KeyCode.B) && noCurrentBomb && m_numBombs > 0)
    {
      // Need bomb audio here
      Vector3 bombPosition = transform.position;
      bombPosition.x = bombPosition.x + m_Ship.m_SpriteWidthFromCenter * 1.2f;
      GameObject bombGameObject = (GameObject)Instantiate(Resources.Load("Prefabs\\Weapons\\BombPlayer"), bombPosition, Quaternion.identity);
      bombGameObject.name = "bomb";

      m_numBombs -= 1;
      m_BombText.text = string.Format("{0}", m_numBombs);
    }
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

  // Check for collisions
  void OnTriggerEnter2D(Collider2D other)
  {
    if (other.gameObject.tag == "Asteroid")
      DamagePlayer(1f);
    else if (other.gameObject.tag == "DestructibleObject" || other.gameObject.tag == "EnemyLaser")
      DamagePlayer(0.5f);
    if (other.gameObject.tag != "PlayerWeapon")
      Destroy(other.gameObject);
  }

  // Apply damage to player
  public void DamagePlayer(float damage)
  {
    if (m_CurrentHealth > 0)
      m_CurrentHealth -= damage;
    else
      m_CurrentHealth = 0;

    if (m_CurrentHealth == 0)
      PlayerDied();

    m_HealthSlider.value = m_CurrentHealth / m_HealthScale;
    m_ExplosionAudio.Play();
  }


  private void PlayerDied()
  {
    // Reset everything, since player persists
    ResetPlayer();
    GameObject gameOver = (GameObject) Instantiate(Resources.Load("Prefabs\\GameOverCanvas"), Camera.main.transform.position, Quaternion.identity);
    m_Destroyed = true;
    m_TimeOfDeath = Time.time;
  }

  public void ResetPlayer()
  {
    m_CurrentHealth = m_MaxHealth;
    m_numBombs = 0;
    m_BombText.text = "0";
    m_numMultiShots = 0;
    m_MultiShotText.text = "0";
    m_Score = 0;
    m_ScoreText.text = "0";
  }

  // Add to player health
  public void ApplyHealth(float health)
  {
    m_ItemPickupAudio.Play();
    if (m_CurrentHealth + health <= m_MaxHealth)
    {
      m_CurrentHealth += health;
    }
    else
    {
      m_CurrentHealth = m_MaxHealth;
    }
    m_HealthSlider.value = m_CurrentHealth / m_HealthScale;
  }

  public void AddScore(int score)
  {
    m_Score += score;
    m_ScoreText.text = string.Format("{0}", m_Score);
  }

  public void ApplyBombs()
  {
    m_ItemPickupAudio.Play();
    m_numBombs += 3;
    m_BombText.text = string.Format("{0}", m_numBombs);
  }

  public void ApplyMultiShots()
  {
    m_ItemPickupAudio.Play();
    m_numMultiShots += 40;
    m_MultiShotText.text = string.Format("{0}", m_numMultiShots);
  }
}
