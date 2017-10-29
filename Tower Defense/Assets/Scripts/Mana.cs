using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mana : MonoBehaviour
{
    public float currentMana;
    public float startingMana;
    public float manaPerSecond;

    //[HideInInspector]
    public bool noMana = false;

	// Use this for initialization
	void Start ()
    {
        currentMana = startingMana;
    }

    // Update is called once per frame
    void Update ()
    {
        if (!noMana && currentMana <= 0)
        {
            noMana = true;
            return;
        }
        else
            currentMana += (manaPerSecond) * Time.fixedDeltaTime;
    }
}
