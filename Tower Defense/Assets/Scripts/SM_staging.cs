using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SM_staging : WytriamSTD.Scene_Manager
{
    public GameObject[] enemyPrefabs;
    public GameObject firstWaypoint;
    public GameObject enemySpawnPoint;
    public float enemiesPerSecond = 0.5f;

    private WytriamSTD.Spawn spawnPoint;

	// Use this for initialization
	void Start ()
    {
        spawnPoint = enemySpawnPoint.GetComponent<WytriamSTD.Spawn>();
        spawnEnemy();
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void spawnEnemy()
    {
        int enemyIndex = Random.Range(0, enemyPrefabs.Length - 1);
        spawnPoint.setSpawnPrefab(enemyPrefabs[enemyIndex]);
        GameObject enemy = spawnPoint.spawn();
        enemy.GetComponent<Enemy_Manager>().waypoint = firstWaypoint;
        Invoke("spawnEnemy", 1 / enemiesPerSecond);

    }
}
