using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SM_tutorial : WytriamSTD.Scene_Manager
{
    public Text manaDisplay;

    private bool usedW = false;
    private bool usedA = false;
    private bool usedS = false;
    private bool usedD = false;
    private bool usedQ = false;
    private bool usedE = false;
    private bool canMove = false;

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
                usedW = true;
            if (Input.GetKeyDown(KeyCode.E))
                usedW = true;
            canMove = usedW && usedA && usedS && usedD && usedQ && usedE;
        }
    }

    IEnumerator tutorial()
    {
        announce("Move around with W, A, S, D, Q, and E", 2);
        yield return new WaitForSeconds(2);
        while (!canMove)
        {
            yield return new WaitForFixedUpdate();
        }
        waves.StartCoroutine("spawnWaves");
        announce("Enemies will attack your tower.\nIf they reach your tower, they will steal your mana and start over.");
        while (!mana.noMana)
        {
            yield return new WaitForFixedUpdate();
        }
        announce("When you run out of mana, it\'s game over.", 2);
        yield return new WaitForSeconds(2);
        announce("Here's some more mana. Use it to upgrade that tower over there to defend yourself", 3);
        mana.currentMana = 150;
        mana.noMana = false;
        while(!enemyCounter.allEnemiesKilled)
        {
            yield return new WaitForFixedUpdate();
        }
        announce("Congratulations! You've killed all the enemies and finished the tutorial.\nEnjoy the game!");
        StopCoroutine("tutorial");
    }
}
