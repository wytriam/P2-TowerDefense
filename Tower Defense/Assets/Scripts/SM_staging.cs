using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SM_staging : WytriamSTD.Scene_Manager
{
    private Mana mana;
    private Waves waves;
    private EnemyCounter enemies;

    private bool notStarted = true;
    private int timeScale = 1;


    // Use this for initialization
    void Start ()
    {
        announce("Press enter to begin");

        mana = GetComponent<Mana>();
        waves = GetComponent<Waves>();
        enemies = GetComponent<EnemyCounter>();
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
