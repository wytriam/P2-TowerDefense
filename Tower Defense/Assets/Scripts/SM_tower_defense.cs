using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SM_tower_defense : WytriamSTD.Scene_Manager
{
    public string nextSceneName;
    public Text manaDisplay;
    public Text scoreDisplay;

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
        manaDisplay.text = "Mana: " + mana.currentMana.ToString("F2");
        if (!waves.isSpawning && enemies.allEnemiesKilled)
        {
            announce("You win!");
        }

        if (mana.noMana)
        {
            announce("You Lose.");
            Time.timeScale = 0;
        }
    }
}