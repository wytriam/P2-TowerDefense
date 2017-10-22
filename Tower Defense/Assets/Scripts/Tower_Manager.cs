using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower_Manager : MonoBehaviour
{
    public GameObject projectile;
    public float rateOfFire = 2.0f;

    private WytriamSTD.Spawn spawnScript;
    private float timer = 0.0f;
    private List<GameObject> enemiesInRange;
    private bool fire = false;

    // Use this for initialization
    void Start ()
    {
        spawnScript = GetComponent<WytriamSTD.Spawn>();
        spawnScript.setSpawnPrefab(projectile);
        enemiesInRange = new List<GameObject>();
        timer = rateOfFire;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (fire)
        {
            timer += Time.fixedDeltaTime;
            if (timer > rateOfFire)
            {
                shoot();
                timer = 0f;
            }
        }
        else
            timer = 0;

	}

    public void register(GameObject enemy)
    {
        enemiesInRange.Add(enemy);
        fire = true;
    }

    public void deregister(GameObject enemy)
    {
        enemiesInRange.Remove(enemy);
        if (enemiesInRange.Count == 0)
        {
            fire = false;
            timer = rateOfFire;
        }
    }

    public void shoot()
    {
        spawnScript.spawn();
    }
}
