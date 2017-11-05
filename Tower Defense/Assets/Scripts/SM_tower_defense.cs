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

    [HideInInspector]
    public float score;

    private Mana mana;
    private Waves waves;
    private EnemyCounter enemies;

    void Awake()
    {
        // If the PlayerPrefs score already exists, read it
        if (PlayerPrefs.HasKey("score"))
        {
            score = PlayerPrefs.GetFloat("score");
        }
        // Assign the score to score
        PlayerPrefs.SetFloat("score", score);
    }

    void Start()
    {
        Time.timeScale = 0;
        announce("Press Space to begin!");

        mana = GetComponent<Mana>();
        waves = GetComponent<Waves>();
        enemies = GetComponent<EnemyCounter>();

        waves.StartCoroutine("spawnWaves");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            toggleTime();

        if(!mana.noMana)
            score += (mana.manaPerSecond) * Time.fixedDeltaTime;

    }

    void FixedUpdate()
    {
        manaDisplay.text = "Mana: " + mana.currentMana.ToString("F2");
        displayScore();
        if (!waves.isSpawning && enemies.allEnemiesKilled)
        {
            announce("You win!");
            saveScore();
            openNextLevel();
        }

        if (mana.noMana)
        {
            announce("You Lose.");
            saveScore();
            openGameOver();
            resetScore();
        }
    }

    public void displayScore()
    {
        scoreDisplay.text = "Score: " + score.ToString("F2");
    }

    void toggleTime()
    {
        if (Time.timeScale == 0)
            Time.timeScale = 1;
        else if (Time.timeScale == 1)
            Time.timeScale = 0;
    }

    void openNextLevel()
    {
        menus.GetComponent<NextLevelMenu>().openMenu();
    }

    void openGameOver()
    {
        menus.GetComponent<GameOverMenu>().openMenu();
    }

    void saveScore()
    {
        PlayerPrefs.SetFloat("score", score);
        float temp = score;
        if (PlayerPrefs.HasKey("HighScore1"))
        {
            if (temp > PlayerPrefs.GetFloat("HighScore1"))
            {
                float temp1 = PlayerPrefs.GetFloat("HighScore1");
                PlayerPrefs.SetFloat("HighScore1", temp);
                temp = temp1;
            }
        }
        if (PlayerPrefs.HasKey("HighScore2"))
        {
            if (temp > PlayerPrefs.GetFloat("HighScore2"))
            {
                float temp1 = PlayerPrefs.GetFloat("HighScore2");
                PlayerPrefs.SetFloat("HighScore2", temp);
                temp = temp1;
            }
        }
        if (PlayerPrefs.HasKey("HighScore3"))
        {
            if (temp > PlayerPrefs.GetFloat("HighScore3"))
            {
                float temp1 = PlayerPrefs.GetFloat("HighScore3");
                PlayerPrefs.SetFloat("HighScore3", temp);
                temp = temp1;
            }
        }
    }

    void resetScore()
    {
        score = 0;
        PlayerPrefs.SetFloat("score", 0);
    }
}