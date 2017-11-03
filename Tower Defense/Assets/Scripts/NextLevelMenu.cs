using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NextLevelMenu : MonoBehaviour
{
    private GameObject menu;
    protected SM_tower_defense sm;

    // Use this for initialization
    void Start()
    {
        menu = transform.GetChild(2).gameObject;
        sm = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<SM_tower_defense>();
        menu.SetActive(false);
    }

    public void openMenu()
    {
        Time.timeScale = 0;
        menu.SetActive(true);
        sm.manaDisplay.enabled = false;
        sm.scoreDisplay.enabled = false;
        menu.GetComponentInChildren<Text>().text = "Score: " + sm.score.ToString("F2");
    }

    public void closeMenu()
    {
        Time.timeScale = 1;
        menu.SetActive(false);
        sm.manaDisplay.enabled = true;
        sm.scoreDisplay.enabled = true;
    }

    public void returnMainMenu()
    {
        SceneManager.LoadScene("menu");
    }


    public void nextLevel()
    {
        SceneManager.LoadScene(sm.nextSceneName);
    }
}
