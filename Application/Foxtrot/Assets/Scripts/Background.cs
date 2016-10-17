using UnityEngine;
using System.Collections;

public class Background : MonoBehaviour {
  // Keep track of whether a planet has been created
  private bool m_planetCreated = false;

  // Use this for initialization
  void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
    CreateRandomStars();
    CreateRandomAsteroids();
    // KELBY
    //if (m_planetCreated == false) {
    //  CreatePlanet();
    //}
    // END KELBY
  }

  /***********************************************************
   Instantiates a SmallStar object just to the right of the
   camera view at a random y position.
  /***********************************************************/
  private void CreateRandomStars()
  {
    // Do not generate a star at every update; reduce probability of generation
    int rand = Random.Range(0, 20);
    int rand2 = Random.Range(0, 70);

    Boundaries bounds = Globals.GetCameraBounds(gameObject);

    if (rand == 2)
    {
      Vector3 position = new Vector3(bounds.m_MaxX, Random.Range(bounds.m_MinY, bounds.m_MaxY));
      Instantiate(Resources.Load("Prefabs\\SmallStar"), position, Quaternion.identity);
    }
    if (rand2 == 3)
    {
      Vector3 position = new Vector3(bounds.m_MaxX, Random.Range(bounds.m_MinY, bounds.m_MaxY));
      Instantiate(Resources.Load("Prefabs\\BigStar"), position, Quaternion.identity);
    }
  }

  /***********************************************************
   Instantiates a Asteroid object just to the right of the
   camera view at a random y position. 
  /***********************************************************/
  private void CreateRandomAsteroids()
  {
    // Do not generate a star at every update; reduce probability of generation
    int rand = Random.Range(0, 2000);

    Boundaries bounds = Globals.GetCameraBounds(gameObject);

    if (rand == 2)
    {
      Vector3 position = new Vector3(bounds.m_MaxX, Random.Range(bounds.m_MinY, bounds.m_MaxY));
      Instantiate(Resources.Load("Prefabs\\Asteroid"), position, Quaternion.identity);
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
    //Vector3 position = new Vector3(m_MaxX, Random.Range(m_MinY, m_MaxY));
    //Instantiate(Resources.Load("Planet"), position, Quaternion.identity);
    //m_planetCreated = true;
  }
}
