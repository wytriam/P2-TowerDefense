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

    private WytriamSTD.Spawn spawnPoint;
    private GameObject currentPrefab;
    private int index;

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
    }

    // Co-routine to handle spawning waves
    IEnumerator spawnWaves()
    {
        Debug.Log("Waves::spawnWaves()");
        // Stop spawning waves if we're out of enemies
        if (index > waves.Length)
            StopCoroutine("spawnWaves");
        if (index % waveCapacity == 0 && index != 0)
            GetComponent<WytriamSTD.Scene_Manager>().announce("Next Wave!");
        currentPrefab = waves[index];
        spawnEnemy();
        index++;
        if (index % waveCapacity == 0 && index != 0)
        {
            Debug.Log("Waiting " + timeBetweenWaves + " before spawning next wave.");
            yield return new WaitForSeconds(timeBetweenWaves);
        }
        Debug.Log("Spawning new enemy in " + 1 / enemiesPerSecond + " seconds");
        yield return new WaitForSeconds(1/enemiesPerSecond);
    }
}
