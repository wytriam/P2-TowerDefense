using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SM_tower_defense : WytriamSTD.Scene_Manager
{
    public string nextSceneName;
    public Text manaDisplay;
    public Text scoreDisplay;
    public GameObject menus;

    private Mana mana;
    private Waves waves;
    private EnemyCounter enemies;

    void Start()
    {
        Time.timeScale = 1;

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
            Invoke("openNextLevel", 3);
        }

        if (mana.noMana)
        {
            announce("You Lose.");
            Time.timeScale = 0;
            openGameOver();
        }
    }

    void openNextLevel()
    {
        menus.GetComponent<NextLevelMenu>().openMenu();
    }

    void openGameOver()
    {
        menus.GetComponent<GameOverMenu>().openMenu();
    }
}