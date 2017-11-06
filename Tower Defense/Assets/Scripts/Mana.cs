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

    [HideInInspector]
    public bool generatingMana = false;

	// Use this for initialization
	void Start ()
    {
        currentMana = startingMana;
    }

    // Update is called once per frame
    void Update ()
    {
        if (currentMana <= 0)
        {
            currentMana = 0;
            generatingMana = false;
            noMana = true;
        }


        if (generatingMana)
            currentMana += (manaPerSecond) * Time.fixedDeltaTime * Time.timeScale;
    }
}
