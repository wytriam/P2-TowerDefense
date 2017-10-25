using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SM_level_1 : SM_staging
{
    public Text manaDisplay;

    private Mana mana;

    void Start()
    {
        spawnPoint = enemySpawnPoint.GetComponent<WytriamSTD.Spawn>();
        spawnEnemy();
        mana = GetComponent<Mana>();
    }

    // Update is called once per frame
    void Update ()
    {
        manaDisplay.text = "Mana: " + mana.currentMana.ToString("F2");

	}
}
