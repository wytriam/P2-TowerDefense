using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SM_staging : WytriamSTD.Scene_Manager
{
    public GameObject[] enemyPrefabs;
    public GameObject firstWaypoint;
    public GameObject enemySpawnPoint;

    private WytriamSTD.Spawn spawnPoint;

	// Use this for initialization
	void Start ()
    {
        spawnPoint = enemySpawnPoint.GetComponent<WytriamSTD.Spawn>();
        spawnPoint.setSpawnPrefab(enemyPrefabs[0]);
        GameObject enemy = spawnPoint.spawn();
        enemy.GetComponent<Enemy_Manager>().waypoint = firstWaypoint;
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
