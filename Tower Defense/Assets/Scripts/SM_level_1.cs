using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SM_level_1 : SM_staging {

    void Start()
    {
        spawnPoint = enemySpawnPoint.GetComponent<WytriamSTD.Spawn>();
        spawnEnemy();
    }

    // Update is called once per frame
    void Update () {
		
	}
}
