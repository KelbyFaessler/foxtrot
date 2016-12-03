using UnityEngine;
using System.Collections;

public class EnemySpawnerLevelThree : MonoBehaviour 
{
	public GameObject[] LevelThreeEnemies;
	float maxBoPSpawnRateInSeconds = 5f;
	float maxKlothosSpawnRateInSeconds = 10f;
	float BoPCounter = 0f;
	float KlothosCounter = 0f;

	float maxBoPSpawns = 20f;
	float maxKlothosSpawns = 20f;
	float maxNeghvarSpawns = 0f;

	// Use this for initialization
	void Start () 
	{
		Invoke ("SpawnEnemyBoP", maxBoPSpawnRateInSeconds);
		InvokeRepeating ("IncreaseBoPSpawnRate", 0f, 30f);

		Invoke ("SpawnEnemyKlothos", maxKlothosSpawnRateInSeconds);
		InvokeRepeating ("IncreaseKlothosSpawnRate", 0f, 30f);
	}
	
	// Update is called once per frame
	void Update()
	{
		if (BoPCounter > maxBoPSpawns && KlothosCounter > maxKlothosSpawns) 
		{
			Invoke ("SpawnEnemyNeghvar", 1f);
		}
	}

	void SpawnEnemyBoP()
	{
		if (BoPCounter <= maxBoPSpawns) {
			var bounds = Globals.GetCameraBounds (gameObject);
			float minY = bounds.m_MinY;
			float maxY = bounds.m_MaxY;
			float maxX = bounds.m_MaxX;

			GameObject aBoP = (GameObject)Instantiate (LevelThreeEnemies [0]);
			aBoP.transform.position = new Vector2 (maxX, minY + Random.Range(0, 6));


			ScheduleNextBoPSpawn ();
			BoPCounter++;
		} 
		else {
			return;
		}
	}

	void ScheduleNextBoPSpawn()
	{
		float spawnInNSeconds;
		if (maxBoPSpawnRateInSeconds > 1f) {
			// pick a number between 1 and maxVelesSpawnRatesInSeconds
			spawnInNSeconds = Random.Range (1f, maxBoPSpawnRateInSeconds);
		} else {
			spawnInNSeconds = 1f;
		}

		Invoke ("SpawnEnemyBoP", spawnInNSeconds);
	}

	//function to increase number of Bird of Prey Spawns
	void IncreaseBoPSpawnRate()
	{
		if (maxBoPSpawnRateInSeconds > 1f)
			maxBoPSpawnRateInSeconds-= 0.5f;

		if (maxBoPSpawnRateInSeconds == 1f)
			CancelInvoke("IncreaseBoPSpawnRate");
	}


	// Spawns Enemy Klothos Ships
	void SpawnEnemyKlothos()
	{
		if (KlothosCounter <= maxKlothosSpawns) {
			var bounds = Globals.GetCameraBounds (gameObject);
			float minY = bounds.m_MinY;
			float maxY = bounds.m_MaxY;
			float maxX = bounds.m_MaxX;

			GameObject aKlothos = (GameObject)Instantiate (LevelThreeEnemies [1]);
			aKlothos.transform.position = new Vector2 (maxX, Random.Range(minY, maxY));

			ScheduleNextKlothosSpawn ();
			KlothosCounter++;
		} 
		else {
			return;
		}
	}

	void ScheduleNextKlothosSpawn()
	{
		float spawnInNSeconds;
		if (maxKlothosSpawnRateInSeconds > 1f) {
			// pick a number between 1 and maxKlothosSpawnRatesInSeconds
			spawnInNSeconds = Random.Range (1f, maxKlothosSpawnRateInSeconds);
		} else {
			spawnInNSeconds = 1f;
		}

		Invoke ("SpawnEnemyKlothos", spawnInNSeconds);
	}

	//function to increase number of Klothos Spawns
	void IncreaseKlothosSpawnRate()
	{
		if (maxKlothosSpawnRateInSeconds > 1f)
			maxKlothosSpawnRateInSeconds-= 0.5f;

		if (maxKlothosSpawnRateInSeconds == 1f)
			CancelInvoke("IncreaseKlothosSpawnRate");
	}

	void SpawnEnemyNeghvar()
	{
		if (maxNeghvarSpawns < 1f)
		{
			var bounds = Globals.GetCameraBounds (gameObject);
			float maxY = bounds.m_MaxY;
			float minY = bounds.m_MinY;
			float maxX = bounds.m_MaxX;

			GameObject aNeghvar = (GameObject)Instantiate (LevelThreeEnemies [2]);
			aNeghvar.transform.position = new Vector2 (maxX, (1*(maxY - minY)/2f)+minY);

			maxNeghvarSpawns++;
		}
		else
		{
			return;
		}
	}
}