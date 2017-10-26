using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mana : MonoBehaviour
{
    public float currentMana;
    public float startingMana;
    public float manaPerSecond;

	// Use this for initialization
	void Start ()
    {
        currentMana = startingMana;
    }

    // Update is called once per frame
    void Update ()
    {
        currentMana += (manaPerSecond) * Time.fixedDeltaTime;
    }
}
