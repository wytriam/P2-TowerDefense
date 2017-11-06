using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower_Manager : MonoBehaviour
{
    public float rateOfFire = 2.0f;

    private WytriamSTD.Spawn spawnScript;
    private List<GameObject> enemiesInRange;
    public bool isFiring = false;

    private AudioSource sound;

    // Use this for initialization
    void Start ()
    {
        spawnScript = GetComponent<WytriamSTD.Spawn>();
        enemiesInRange = new List<GameObject>();
        sound = GetComponent<AudioSource>();
	}
	
    IEnumerator firing()
    {
        isFiring = true;        
        while(isFiring)
        {
            shoot();
            yield return new WaitForSeconds(1 / rateOfFire);
        }
        StopCoroutine("firing");
    }

    public void register(GameObject enemy)
    {
        enemiesInRange.Add(enemy);
        if (!isFiring)
        {
            StartCoroutine("firing");
        }
    }

    public void deregister(GameObject enemy)
    {
        enemiesInRange.Remove(enemy);
        if (enemiesInRange.Count == 0)
        {
            isFiring = false;
        }
    }

    public void deregisterAll()
    {
        //foreach (GameObject enemy in enemiesInRange)
        int iteratations = enemiesInRange.Count;
        for (int i=0;i<iteratations;i++)
        {
            GameObject enemy = enemiesInRange[0];
            deregister(enemy);
            enemy.GetComponent<Enemy_Manager>().deregisterTower(gameObject);
        }
        isFiring = false;
    }

    public void shoot()
    {
        sound.mute = false;
        sound.Play();
        spawnScript.spawn();
    }
}
