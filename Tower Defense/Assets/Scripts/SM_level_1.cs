using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SM_level_1 : SM_staging
{
    public float startingMana;
    public float currentMana;
    public float manaPerSecond;
    public Text manaDisplay;

    void Start()
    {
        spawnPoint = enemySpawnPoint.GetComponent<WytriamSTD.Spawn>();
        spawnEnemy();
        currentMana = startingMana;
    }

    // Update is called once per frame
    void Update ()
    {
        currentMana += (manaPerSecond) * Time.fixedDeltaTime;
        manaDisplay.text = "Mana: " + currentMana.ToString("F2");

	}
}
