using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCounter : MonoBehaviour
{
    [HideInInspector]
    public bool allEnemiesKilled = false;

    private List<GameObject> enemies;

	// Use this for initialization
	void Start ()
    {
        enemies = new List<GameObject>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void register(GameObject enemy)
    {
        enemies.Add(enemy);
        allEnemiesKilled = false;
    }

    public void deregister(GameObject enemy)
    {
        enemies.Remove(enemy);
        if (enemies.Count == 0)
            allEnemiesKilled = true;
    }

}
