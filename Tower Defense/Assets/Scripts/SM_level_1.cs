using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SM_level_1 : SM_staging
{
    public Text manaDisplay;

    private Mana mana;
    private Waves waves;

    void Start()
    {
        mana = GetComponent<Mana>();
        waves = GetComponent<Waves>();

        waves.StartCoroutine("spawnWaves");
    }

    // Update is called once per frame
    void Update ()
    {
        manaDisplay.text = "Mana: " + mana.currentMana.ToString("F2");
	}
}
