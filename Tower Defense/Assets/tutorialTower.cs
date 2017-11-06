using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutorialTower : MonoBehaviour
{
    public GameObject tutorial;
    private SM_tutorial tut;

	// Use this for initialization
	void Start () {
        tut = tutorial.GetComponent<SM_tutorial>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnMouseOver()
    {
        tut.towerLookedAt = true;
    }
}
