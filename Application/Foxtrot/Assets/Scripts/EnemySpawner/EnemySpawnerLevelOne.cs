using UnityEngine;
using System.Collections;

public class EnemySpawnerLevelOne : MonoBehaviour 
{
	public GameObject[] LevelOneEnemies;
	float maxVelesSpawnRateInSeconds = 5f;
	float maxNorexanSpawnRateInSeconds = 10f;
	float VelesCounter = 0f;
	float NorexanCounter = 0f;

	float maxVelesSpawns = 55f;
	float maxNorexanSpawns = 55f;
	float maxDderidexSpawns = 0f;

	// Use this for initialization
	void Start () 
	{
		Invoke ("SpawnEnemyVeles", maxVelesSpawnRateInSeconds);
		InvokeRepeating ("IncreaseVelesSpawnRate", 0f, 12f);

		Invoke ("SpawnEnemyNorexan", maxNorexanSpawnRateInSeconds);
		InvokeRepeating ("IncreaseNorexanSpawnRate", 0f, 12f);
	}
	
	// Update is called once per frame
	void Update()
  {
    if (VelesCounter > maxVelesSpawns && NorexanCounter > maxNorexanSpawns) 
		{
			Invoke ("SpawnEnemyDderidex", 1f);
		}
	}

	void SpawnEnemyVeles()
	{
		if (VelesCounter <= maxVelesSpawns) {
			var bounds = Globals.GetCameraBounds (gameObject);
			float minY = bounds.m_MinY;
			float maxY = bounds.m_MaxY;
			float maxX = bounds.m_MaxX;

			GameObject anVeles = (GameObject)Instantiate (LevelOneEnemies [0]);
			anVeles.transform.position = new Vector2 (maxX, Random.Range(minY, maxY));
      //DEBUG
      Debug.Log(Time.realtimeSinceStartup.ToString() + ": New Veles spawned");
      //END DEBUG

      ScheduleNextVelesSpawn();
			VelesCounter++;
		} 
		else {
			return;
		}
	}

	void ScheduleNextVelesSpawn()
	{
		float spawnInNSeconds;
		if (maxVelesSpawnRateInSeconds > 1f) {
			// pick a number between 1 and maxVelesSpawnRatesInSeconds
			spawnInNSeconds = Random.Range (Mathf.Max(0.1f, (maxVelesSpawnRateInSeconds - 0.75f)), maxVelesSpawnRateInSeconds);
		} else {
			spawnInNSeconds = 1f;
		}
    
    //DEBUG
    Debug.Log(Time.realtimeSinceStartup.ToString() + ": Spawn Veles in " + spawnInNSeconds.ToString());
    //END DEBUG
    Invoke ("SpawnEnemyVeles", spawnInNSeconds);
	}

	//function to increase number of Veles Spawns
	void IncreaseVelesSpawnRate()
	{
		if (maxVelesSpawnRateInSeconds > 1f)
			maxVelesSpawnRateInSeconds-= 0.5f;

    if (maxVelesSpawnRateInSeconds < 0.25f)
    {
      maxVelesSpawnRateInSeconds = 0.25f;
    }
    //DEBUG
    Debug.Log(Time.realtimeSinceStartup.ToString() + ": Veles Spawn Rate increased to " + maxVelesSpawnRateInSeconds.ToString());
    //END DEBUG
    //if (maxVelesSpawnRateInSeconds == 1f)
    //	CancelInvoke("IncreaseVelesSpawnRate");
  }


  // Spawns Enemy Norexan Ships
  void SpawnEnemyNorexan()
	{

		if (NorexanCounter <= maxNorexanSpawns) {
			var bounds = Globals.GetCameraBounds (gameObject);
			float minY = bounds.m_MinY;
			float maxY = bounds.m_MaxY;
			float maxX = bounds.m_MaxX;

			GameObject anNorexan = (GameObject)Instantiate (LevelOneEnemies [1]);
			anNorexan.transform.position = new Vector2 (maxX, Random.Range(minY, maxY));

			ScheduleNextNorexanSpawn ();
			NorexanCounter++;
		} 
		else {
			return;
		}
	}

	void ScheduleNextNorexanSpawn()
	{
		float spawnInNSeconds;
		if (maxNorexanSpawnRateInSeconds > 1f) {
			// pick a number between 1 and maxVelesSpawnRatesInSeconds
			spawnInNSeconds = Random.Range(Mathf.Max(0.25f, maxNorexanSpawnRateInSeconds - 1.5f), maxNorexanSpawnRateInSeconds);
		} else {
			spawnInNSeconds = 1f;
		}

		Invoke ("SpawnEnemyNorexan", spawnInNSeconds);
	}

	//function to increase number of Veles Spawns
	void IncreaseNorexanSpawnRate()
	{
		if (maxNorexanSpawnRateInSeconds > 1f)
			maxNorexanSpawnRateInSeconds-= 1.0f;

    if (maxNorexanSpawnRateInSeconds < 0.5f)
    {
      maxNorexanSpawnRateInSeconds = 0.5f;
    }
    
    //  if (maxNorexanSpawnRateInSeconds == 1f)
		//	CancelInvoke("IncreaseNorexanSpawnRate");
	}

	void SpawnEnemyDderidex()
	{
		if (maxDderidexSpawns < 1f)
		{
			var bounds = Globals.GetCameraBounds (gameObject);
			float maxY = bounds.m_MaxY;
			float minY = bounds.m_MinY;
			float maxX = bounds.m_MaxX;

			GameObject anDderidex = (GameObject)Instantiate (LevelOneEnemies [2]);
			anDderidex.transform.position = new Vector2 (maxX, (1*(maxY - minY)/2)+minY);

			maxDderidexSpawns++;
		}
		else
		{
			return;
		}
	}
}