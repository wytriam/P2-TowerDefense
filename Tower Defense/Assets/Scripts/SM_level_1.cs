using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SM_level_1 : SM_staging
{
    public Text manaDisplay;

    private Mana mana;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        spawnPoint = enemySpawnPoint.GetComponent<WytriamSTD.Spawn>();
        spawnEnemy();
        mana = GetComponent<Mana>();
    }

    // Update is called once per frame
    void Update ()
    {
        if (Input.GetKeyDown("escape"))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        if (Input.GetMouseButtonDown(0))
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }
        manaDisplay.text = "Mana: " + mana.currentMana.ToString("F2");

	}
}
