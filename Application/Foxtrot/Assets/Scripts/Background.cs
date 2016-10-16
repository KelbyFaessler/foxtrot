using UnityEngine;
using System.Collections;

public class Background : MonoBehaviour {
  // Boundaries for player, set by camera view
  private float m_MinX;
  private float m_MaxX;
  private float m_MinY;
  private float m_MaxY;

  // Keep track of whether a planet has been created
  private bool m_planetCreated = false;

  // Use this for initialization
  void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
    CreateRandomStars();
    // KELBY
    //if (m_planetCreated == false) {
    //  CreatePlanet();
    //}
    // END KELBY
  }

  /***********************************************************
  /** CreateRandomStars
   Instantiates a SmallStar object just to the right of the
   camera view at a random y position.
  /***********************************************************/
  private void CreateRandomStars()
  {
    // Do not generate a star at every update; reduce probability of generation
    int rand = Random.Range(0, 20);
    int rand2 = Random.Range(0, 70);

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

  /***********************************************************
  /** CreatePlanet
   Determines whether to add a planet to background based on 
   current level. If so, instantiates and adds one.
  /***********************************************************/
  private void CreatePlanet()
  {
    // Determine whether to create planet based on current level

    // Set planet spawn point

    // Create new planet
    Vector3 position = new Vector3(m_MaxX, Random.Range(m_MinY, m_MaxY));
    Instantiate(Resources.Load("Planet"), position, Quaternion.identity);
    m_planetCreated = true;
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
}
