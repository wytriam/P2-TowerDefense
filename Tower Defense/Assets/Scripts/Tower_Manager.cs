using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower_Manager : MonoBehaviour
{
    public GameObject projectile;
    public WytriamSTD.Spawn spawnScript;

	// Use this for initialization
	void Start ()
    {
        spawnScript = GetComponent<WytriamSTD.Spawn>();
        spawnScript.setSpawnPrefab(projectile);	
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void shoot()
    {
        spawnScript.spawn();
    }
}
