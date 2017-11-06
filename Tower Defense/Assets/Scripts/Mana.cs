using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mana : MonoBehaviour
{
    public float currentMana;
    public float startingMana;
    public float manaPerSecond;

    [HideInInspector]
    public bool noMana = false;

	// Use this for initialization
	void Start ()
    {
        currentMana = startingMana;
    }

    // Update is called once per frame
    void Update ()
    {
        if (currentMana < 0)
            currentMana = 0;

        if (noMana) return;

        if (currentMana <= 0)
        {
            noMana = true;
            return;
        }
        else if (Time.timeScale != 0)
            currentMana += (manaPerSecond) * Time.fixedDeltaTime;
    }
}
