using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SM_level_1 : WytriamSTD.Scene_Manager
{
    public Text manaDisplay;

    private Mana mana;
    private Waves waves;
    private EnemyCounter enemies;

    void Start()
    {
        mana = GetComponent<Mana>();
        waves = GetComponent<Waves>();
        enemies = GetComponent<EnemyCounter>();

        waves.StartCoroutine("spawnWaves");
    }

    void FixedUpdate()
    {
        if (!mana.noMana)
        {
            manaDisplay.text = "Mana: " + mana.currentMana.ToString("F2");
            if (!waves.isSpawning && enemies.allEnemiesKilled)
            {
                announce("You win!");
            }

            if (mana.noMana)
            {
                announce("You Lose.");
            }
        }
	}

}
