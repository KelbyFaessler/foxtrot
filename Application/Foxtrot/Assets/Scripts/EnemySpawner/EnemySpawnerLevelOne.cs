using UnityEngine;
using System.Collections;

public class EnemySpawnerLevelOne : MonoBehaviour 
{
	public GameObject[] LevelOneEnemies;
	private GameObject[] VelesCount, NorexanCount;
	float howManyVeles;
	float howManyNorexan;
	float maxVelesSpawnRateInSeconds = 5f;
	float maxNorexanSpawnRateInSeconds = 10f;
	//float StopVelesInvoke = 0;

	float maxVelesSpawns = 35f;
	float maxNorexanSpawns = 25f;
	float maxDderidexSpawns = 1f;

	float StartTime = Time.time;
	float Timer = 0.0f;

	// Use this for initialization
	void Start () 
	{

		if (Timer < StartTime + .012f) {
			Invoke ("SpawnEnemyVeles", maxVelesSpawnRateInSeconds);
			InvokeRepeating ("IncreaseVelesSpawnRate", 0f, 30f);

			Invoke ("SpawnEnemyNorexan", maxNorexanSpawnRateInSeconds);
			InvokeRepeating ("IncreaseNorexanSpawnRate", 0f, 30f);
		}
		else
		{
			Invoke ("SpawnEnemyDderidex", 1f); 
		}
	}
	
	// Update is called once per frame
	void Update()
	{
		
	}

	void SpawnEnemyVeles()
	{
		var bounds = Globals.GetCameraBounds (gameObject);
		float minY = bounds.m_MinY;
		float maxY = bounds.m_MaxY;
		float maxX = bounds.m_MaxX;

		GameObject anVeles = (GameObject)Instantiate (LevelOneEnemies [0]);
		anVeles.transform.position = new Vector2 (maxX, Random.Range(minY, maxY));

		ScheduleNextVelesSpawn ();
	}

	void ScheduleNextVelesSpawn()
	{
		float spawnInNSeconds;
		if (maxVelesSpawnRateInSeconds > 1f) {
			// pick a number between 1 and maxVelesSpawnRatesInSeconds
			spawnInNSeconds = Random.Range (1f, maxVelesSpawnRateInSeconds);
		} else {
			spawnInNSeconds = 1f;
		}

		Invoke ("SpawnEnemyVeles", spawnInNSeconds);
	}

	//function to increase number of Veles Spawns
	void IncreaseVelesSpawnRate()
	{
		if (maxVelesSpawnRateInSeconds > 1f)
			maxVelesSpawnRateInSeconds-= 0.5f;

		if (maxVelesSpawnRateInSeconds == 1f)
			CancelInvoke("IncreaseVelesSpawnRate");
	}


	// Spawns Enemy Norexan Ships
	void SpawnEnemyNorexan()
	{
		var bounds = Globals.GetCameraBounds (gameObject);
		float minY = bounds.m_MinY;
		float maxY = bounds.m_MaxY;
		float maxX = bounds.m_MaxX;

		GameObject anNorexan = (GameObject)Instantiate (LevelOneEnemies [1]);
		anNorexan.transform.position = new Vector2 (maxX, Random.Range(minY, maxY));

		ScheduleNextNorexanSpawn ();
	}

	void ScheduleNextNorexanSpawn()
	{
		float spawnInNSeconds;
		if (maxNorexanSpawnRateInSeconds > 1f) {
			// pick a number between 1 and maxVelesSpawnRatesInSeconds
			spawnInNSeconds = Random.Range (1f, maxNorexanSpawnRateInSeconds);
		} else {
			spawnInNSeconds = 1f;
		}

		Invoke ("SpawnEnemyNorexan", spawnInNSeconds);
	}

	//function to increase number of Veles Spawns
	void IncreaseNorexanSpawnRate()
	{
		if (maxNorexanSpawnRateInSeconds > 1f)
			maxNorexanSpawnRateInSeconds-= 0.5f;

		if (maxNorexanSpawnRateInSeconds == 1f)
			CancelInvoke("IncreaseNorexanSpawnRate");
	}

	void SpawnEnemyDderidex()
	{
		var bounds = Globals.GetCameraBounds (gameObject);
		float maxY = bounds.m_MaxY;
		float minY = bounds.m_MinY;
		float maxX = bounds.m_MaxX;

		GameObject anDderidex = (GameObject)Instantiate (LevelOneEnemies [2]);
		anDderidex.transform.position = new Vector2 (maxX, maxY/2.0f);
	}
}