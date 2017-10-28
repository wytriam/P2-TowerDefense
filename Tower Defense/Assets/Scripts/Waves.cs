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
    private GameObject currentPrefab;

    void Start()
    {
        spawnPoint = enemySpawnPoint.GetComponent<WytriamSTD.Spawn>();
    }

    public void spawnWave(GameObject waveObject)
    {
        GameObject[] wave = waveObject.GetComponent<Wave>().enemyPrefabs;

        for (int i=0;i<wave.Length;i++)
        {
            currentPrefab = currentPrefab = wave[i];
            Invoke("spawnEnemy", (1 / enemiesPerSecond) * i);
        }
    }

    void spawnEnemy()
    {
        if (!spawnAllowed) return;
        spawnPoint.setSpawnPrefab(currentPrefab);
        GameObject enemy = spawnPoint.spawn();
        enemy.GetComponent<Enemy_Manager>().waypoint = firstWaypoint;
    }

}
