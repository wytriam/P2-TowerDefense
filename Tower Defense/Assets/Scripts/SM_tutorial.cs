using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SM_tutorial : WytriamSTD.Scene_Manager
{
    public Text manaDisplay;

    [HideInInspector]
    public bool towerLookedAt = false;

    private bool usedW = false;
    private bool usedA = false;
    private bool usedS = false;
    private bool usedD = false;
    private bool usedQ = false;
    private bool usedE = false;
    private bool canMove = false;

    private bool tutorialComplete = false;

    private int tabCount = 0;
    private int spaceCount = 0;

    private int timeScale = 1;

    private Waves waves;
    private Mana mana;
    private EnemyCounter enemyCounter;

    // Use this for initialization
    void Start ()
    {
        waves = GetComponent<Waves>();
        mana = GetComponent<Mana>();
        enemyCounter = GetComponent<EnemyCounter>();

        StartCoroutine("tutorial");
    }

    // Update is called once per frame
    void Update ()
    {
        manaDisplay.text = "Mana: " + mana.currentMana.ToString("F2");

        if (Input.GetKeyDown(KeyCode.Space))
        {
            spaceCount++;
            toggleTime();
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            tabCount++;
            changeTime();
        }

        if (!canMove)
        {
            if (Input.GetKeyDown(KeyCode.W))
                usedW = true;
            if (Input.GetKeyDown(KeyCode.A))
                usedA = true;
            if (Input.GetKeyDown(KeyCode.S))
                usedS = true;
            if (Input.GetKeyDown(KeyCode.D))
                usedD = true;
            if (Input.GetKeyDown(KeyCode.Q))
                usedQ = true;
            if (Input.GetKeyDown(KeyCode.E))
                usedE = true;
            canMove = usedW && usedA && usedS && usedD && usedQ && usedE;
        }
        
        if (tutorialComplete)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                StopCoroutine("tutorial");
                SceneManager.LoadScene("menu");
            }
        }
    }

    IEnumerator tutorial()
    {
        announce("Welcome to the tutorial!", 2);
        yield return new WaitForSeconds(2);
        announce("In front of you is your Wizzzard Tower. You need to protect this!", 3);
        yield return new WaitForSeconds(3);
        announce("Move around with W, A, S, D, Q, and E", 2);
        yield return new WaitForSeconds(2);
        while (!canMove)
        {
            yield return new WaitForFixedUpdate();
        }
        announce("You can look around by holding the Right Mouse Button and moving the mouse.\nTry to find your turret tower! \n(and mouse over it, please)", 3);
        yield return new WaitForSeconds(3);
        while (!towerLookedAt)
            yield return new WaitForFixedUpdate();
        waves.StartCoroutine("spawnWaves");
        announce("Enemies will attack your Wizzzard Tower.\nIf they reach your Tower, they will steal your mana and start over.");
        while (!mana.noMana)
        {
            yield return new WaitForFixedUpdate();
        }
        announce("When you run out of mana, it\'s game over.", 2);
        yield return new WaitForSeconds(2);
        announce("Fortunately, you regenerate mana.", 2);
        mana.manaPerSecond = 2;
        mana.generatingMana = true;
        mana.currentMana = 1;
        mana.noMana = false;
        yield return new WaitForSeconds(2);
        announce("You can also manipulate time. Hit tab to cycle between 1x, 2x, and 4x speed.", 3);
        yield return new WaitForSeconds(3);
        while (tabCount < 3)
            yield return new WaitForFixedUpdate();
        announce("You can also pause and unpause tab with the space button.", 3);
        yield return new WaitForSeconds(3);
        while (spaceCount < 2)
            yield return new WaitForFixedUpdate();
        announce("Here's some more mana. Click on that turret tower to upgrade it and defend yourself", 3);
        mana.currentMana = 150;
        while(!enemyCounter.allEnemiesKilled)
        {
            yield return new WaitForFixedUpdate();
        }
        announce("Congratulations! You've killed all the enemies and finished the tutorial.\nEnjoy the game!");
        tutorialComplete = true;
        yield return new WaitForSeconds(6);
        announce("Click anywhere to return to the main menu");
        yield return null;
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

}
