using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower_Manager : MonoBehaviour
{
    public float rateOfFire = 2.0f;

    private WytriamSTD.Spawn spawnScript;
    private List<GameObject> enemiesInRange;
    public bool isFiring = false;

    // Use this for initialization
    void Start ()
    {
        spawnScript = GetComponent<WytriamSTD.Spawn>();
        enemiesInRange = new List<GameObject>();
	}
	
    IEnumerator firing()
    {
        Debug.Log("Beginning Coroutine firing");
        isFiring = true;        // this line is messing up the coroutine for some reason. 
        Debug.Log("isFiring is now true");
        while(isFiring)
        {
            Debug.Log("About to shoot projectile");
            shoot();
            yield return new WaitForSeconds(1 / rateOfFire);
        }
        Debug.Log("Ending Coroutine firing");
        StopCoroutine("firing");
    }

    public void register(GameObject enemy)
    {
        Debug.Log("Registering Enemy");
        enemiesInRange.Add(enemy);
        Debug.Log("Enemy registered");
        if (!isFiring)
        {
            Debug.Log("Starting Coroutine firing");
            StartCoroutine("firing");
        }
        else
            Debug.Log("Huh??????");
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
    }

    public void shoot()
    {
        Debug.Log("Projectile shot");
        spawnScript.spawn();
    }
}
