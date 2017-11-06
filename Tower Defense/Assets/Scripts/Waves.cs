using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waves : MonoBehaviour
{
    [Header("Monsters go here")]
    public GameObject[] waves;

    public GameObject firstWaypoint;
    public GameObject[] alternateWaypoints;
    public GameObject enemySpawnPoint;
    public GameObject[] alternateSpawns;
    public float enemiesPerSecond = 0.5f;
    public float timeBetweenWaves = 5f;
    public int[] enemiesPerWave; // how many monsters are in a wave

    [HideInInspector]
    public bool isSpawning = false;

    private WytriamSTD.Spawn spawnPoint;
    private GameObject currentPrefab;
    private int index;
    private int spawnCount;
    private int enemiesInWave;

    void Awake()
    {
        spawnPoint = enemySpawnPoint.GetComponent<WytriamSTD.Spawn>();
        index = 0;
        enemiesInWave = enemiesPerWave[index];
        if (enemiesPerWave.Length != waves.Length)
            Debug.Log("Waves not properly set up - enemiesPerWave and waves length not equal");
    }

    void spawnEnemy()
    {
        spawnPoint.setSpawnPrefab(currentPrefab);
        GameObject enemy = spawnPoint.spawn();
        enemy.GetComponent<Enemy_Manager>().waypoint = firstWaypoint;
        enemy.GetComponent<Enemy_Manager>().firstWaypoint = firstWaypoint;
        enemy.GetComponent<Enemy_Manager>().spawnPos = spawnPoint.getSpawnPos();
        spawnCount++;
    }

    // Co-routine to handle spawning waves
    IEnumerator spawnWaves()
    {
        isSpawning = true;
        while (index < waves.Length)
        {
            if (spawnCount % enemiesInWave == 0 && index != 0)
                GetComponent<WytriamSTD.Scene_Manager>().announce("Next Wave!");
            currentPrefab = waves[index];
            spawnEnemy();
            if (spawnCount % enemiesInWave == 0)
            {
                index++;
                if (alternateWaypoints.Length != 0)
                    firstWaypoint = alternateWaypoints[index % alternateWaypoints.Length];
                if (alternateSpawns.Length != 0)
                    spawnPoint = alternateSpawns[index % alternateSpawns.Length].GetComponent<WytriamSTD.Spawn>();
                spawnCount = 0;
                enemiesInWave = enemiesPerWave[index % enemiesPerWave.Length];
            }
            if (spawnCount % enemiesInWave == 0 && index != 0)
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
