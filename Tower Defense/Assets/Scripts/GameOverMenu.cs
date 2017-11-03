﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour {

    private GameObject menu;
    protected SM_tower_defense sm;

    // Use this for initialization
    void Start()
    {
        menu = transform.GetChild(1).gameObject;
        sm = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<SM_tower_defense>();
        menu.SetActive(false);
    }

    public void openMenu()
    {
        menu.SetActive(true);
        sm.manaDisplay.enabled = false;
        sm.scoreDisplay.enabled = false;
    }

    public void closeMenu()
    {
        menu.SetActive(false);
        sm.manaDisplay.enabled = true;
        sm.scoreDisplay.enabled = true;
    }

    public void returnMainMenu()
    {
        SceneManager.LoadScene("menu");
    }

}
