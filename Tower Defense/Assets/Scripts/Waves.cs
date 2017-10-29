using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waves : MonoBehaviour
{
    public GameObject[] waves;

    public GameObject firstWaypoint;
    public GameObject enemySpawnPoint;
    public float enemiesPerSecond = 0.5f;
    public float timeBetweenWaves = 5f;
    public int waveCapacity; // how many monsters are in a wave

    [HideInInspector]
    public bool isSpawning = false;

    private WytriamSTD.Spawn spawnPoint;
    private GameObject currentPrefab;
    private int index;
    private int spawnCount;

    void Awake()
    {
        spawnPoint = enemySpawnPoint.GetComponent<WytriamSTD.Spawn>();
        index = 0;
    }

    void spawnEnemy()
    {
        spawnPoint.setSpawnPrefab(currentPrefab);
        GameObject enemy = spawnPoint.spawn();
        enemy.GetComponent<Enemy_Manager>().waypoint = firstWaypoint;
        spawnCount++;
    }

    // Co-routine to handle spawning waves
    IEnumerator spawnWaves()
    {
        isSpawning = true;
        while (index < waves.Length)
        {
            if (index % waveCapacity == 0 && index != 0)
                GetComponent<WytriamSTD.Scene_Manager>().announce("Next Wave!");
            currentPrefab = waves[index];
            spawnEnemy();
            if (spawnCount % waveCapacity == 0)
            {
                index++;
                spawnCount = 0;
            }
            if (index % waveCapacity == 0 && index != 0)
                yield return new WaitForSeconds(timeBetweenWaves);
            else
                yield return new WaitForSeconds(1 / enemiesPerSecond);
        }
        isSpawning = false;
        // Stop spawning waves if we're out of enemies
        if (index > waves.Length)
            StopCoroutine("spawnWaves");
    }
}
