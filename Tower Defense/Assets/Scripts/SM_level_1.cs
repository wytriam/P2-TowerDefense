using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SM_level_1 : SM_staging
{
    public Text manaDisplay;

    private Mana mana;
    private Waves waves;
    private List<GameObject> enemies;

    void Start()
    {
        mana = GetComponent<Mana>();
        waves = GetComponent<Waves>();

        enemies = new List<GameObject>();

        waves.StartCoroutine("spawnWaves");
    }

    void FixedUpdate()
    {
        manaDisplay.text = "Mana: " + mana.currentMana.ToString("F2");
	}

    public void register(GameObject enemy)
    {
        enemies.Add(enemy);
    }

    public void deregister(GameObject enemy)
    {
        enemies.Remove(enemy);
        if (enemies.Count == 0 && !waves.isSpawning)
        {
            announce("You win!");
        }
    }
}
