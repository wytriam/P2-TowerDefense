using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waves : MonoBehaviour
{
    public GameObject[] waves;

    public GameObject firstWaypoint;
    public GameObject enemySpawnPoint;
    public float enemiesPerSecond = 0.5f;
    public bool spawnAllowed = true;

    private WytriamSTD.Spawn spawnPoint;

    void Start()
    {
        spawnPoint = enemySpawnPoint.GetComponent<WytriamSTD.Spawn>();
    }

    public void spawnWave(GameObject waveObject)
    {
        GameObject[] wave = waveObject.GetComponent<Wave>().enemyPrefabs;
    }

    void spawnEnemy(GameObject enemyPrefab)
    {
        if (!spawnAllowed) return;
        spawnPoint.setSpawnPrefab(enemyPrefab);
        GameObject enemy = spawnPoint.spawn();
        enemy.GetComponent<Enemy_Manager>().waypoint = firstWaypoint;
    }

}
