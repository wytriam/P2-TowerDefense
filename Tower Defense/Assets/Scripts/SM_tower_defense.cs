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
    public float levelBonus = 100;

    [HideInInspector]
    public float score;

    private Mana mana;
    private Waves waves;
    private EnemyCounter enemies;

    private int timeScale = 1;

    [HideInInspector]
    public bool notStarted = true;

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
        hearAnnouncements();
        announce("Press Enter to begin!");

        mana = GetComponent<Mana>();
        waves = GetComponent<Waves>();
        enemies = GetComponent<EnemyCounter>();

        displayMana();
        displayScore();

        Time.timeScale = 1;
    }

    void Update()
    {
        if (notStarted)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                waves.StartCoroutine("spawnWaves");
                notStarted = false;
                mana.generatingMana = true;
            }
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space))
            toggleTime();

        if (Input.GetKeyDown(KeyCode.Tab))
            changeTime();

        if(!mana.noMana)
            score += (mana.manaPerSecond) * Time.fixedDeltaTime * Time.timeScale;

    }

    void FixedUpdate()
    {
        displayMana();
        displayScore();
        if (!waves.isSpawning && enemies.allEnemiesKilled)
        {
            announce("You win!");
            if (menus.GetComponent<UpgradeMenu>().menuOpen)
                menus.GetComponent<UpgradeMenu>().closeMenu();
            score += levelBonus;
            saveScore();
            openNextLevel();
        }

        if (mana.noMana)
        {
            announce("You Lose.");
            if (menus.GetComponent<UpgradeMenu>().menuOpen)
                menus.GetComponent<UpgradeMenu>().closeMenu();
            openGameOver();
        }
    }

    public void displayScore()
    {
        scoreDisplay.text = "Score: " + score.ToString("F2");
    }

    public void displayMana()
    {
        manaDisplay.text = "Mana: " + mana.currentMana.ToString("F2");
    }

    void toggleTime()
    {
        if (Time.timeScale == 0)
            Time.timeScale = timeScale;
        else if (Time.timeScale == timeScale)
            Time.timeScale = 0;
    }

    void changeTime()
    {
        if (timeScale == 1)
            timeScale = 2;
        else if (timeScale == 2)
            timeScale = 4;
        else if (timeScale == 4)
            timeScale = 1;
        Time.timeScale = timeScale;
    }

    void openNextLevel()
    {
        menus.GetComponent<NextLevelMenu>().openMenu();
        muteAnnoucements();
    }

    void openGameOver()
    {
        menus.GetComponent<GameOverMenu>().openMenu();
        muteAnnoucements();
    }

    void saveScore()
    {
        PlayerPrefs.SetFloat("score", score);
    }

    void saveHighScore()
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

    public void resetScore()
    {
        saveHighScore();
        score = 0;
        PlayerPrefs.SetFloat("score", 0);
    }
}