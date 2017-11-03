using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverMenu : MonoBehaviour {

    private GameObject menu;
    private SM_tower_defense sm;

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

    void closeMenu()
    {
        menu.SetActive(false);
        sm.manaDisplay.enabled = true;
        sm.scoreDisplay.enabled = true;
    }

}
