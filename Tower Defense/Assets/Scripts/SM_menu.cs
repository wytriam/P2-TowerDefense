using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SM_menu : MonoBehaviour
{
    public string NextSceneName;

    private GameObject returnButton;
    private GameObject[] buttons;
    private GameObject activePage;
    private GameObject rulesPage;
    private GameObject scorePage;
    private GameObject creditsPage;

    void Awake()
    {
        // initialize high scores
        if (!PlayerPrefs.HasKey("HighScore1"))
        {
            PlayerPrefs.SetFloat("HighScore1", 0);
        }
        if (!PlayerPrefs.HasKey("HighScore2"))
        {
            PlayerPrefs.SetFloat("HighScore2", 0);
        }
        if (!PlayerPrefs.HasKey("HighScore3"))
        {
            PlayerPrefs.SetFloat("HighScore3", 0);
        }
    }

    // Use this for initialization
    void Start()
    {
        buttons = GameObject.FindGameObjectsWithTag("Button");
        foreach(GameObject button in buttons)
        {
            if (button.name.StartsWith("Return"))
                returnButton = button;
        }
        returnButton.SetActive(false);
        GameObject[] menuPages = GameObject.FindGameObjectsWithTag("MenuPage");
        foreach(GameObject page in menuPages)
        {
            if (page.name.StartsWith("Credits"))
                creditsPage = page;
            else if (page.name.StartsWith("Scores"))
                scorePage = page;
            else if (page.name.StartsWith("Rules"))
                rulesPage = page;
            page.SetActive(false);
        }

        activePage = creditsPage;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void hideButtons()
    {
        foreach (GameObject button in buttons)
            button.SetActive(false);
        returnButton.SetActive(true);
    }

    void showButtons()
    {
        foreach (GameObject button in buttons)
            button.SetActive(true);
        returnButton.SetActive(false);
    }

    // Play button is pushed
    public void play()
    {
        Debug.Log("SM_menu::play() - Button Pushed");
        SceneManager.LoadScene(NextSceneName);
    }

    // High Scores button is pushed
    public void highScores()
    {
        Debug.Log("SM_menu::highScores() - Button Pushed");
        hideButtons();
        activePage = scorePage;
        activePage.GetComponent<Text>().text = "1. " + PlayerPrefs.GetFloat("HighScore1").ToString("F2") + "\n" +
                                               "2. " + PlayerPrefs.GetFloat("HighScore2").ToString("F2") + "\n" +
                                               "3. " + PlayerPrefs.GetFloat("HighScore3").ToString("F2");
        activePage.SetActive(true);
    }

    // Rules button is pushed
    public void rules()
    {
        Debug.Log("SM_menu::rules() - Button Pushed");
        hideButtons();
        activePage = rulesPage;
        activePage.SetActive(true);
    }

    // Credits button is pushed
    public void credits()
    {
        Debug.Log("SM_menu::credits() - Button Pushed");
        hideButtons();
        activePage = creditsPage;
        activePage.SetActive(true);
    }

    // Exit button is pushed
    public void exit()
    {
        Debug.Log("SM_menu::exit() - Button Pushed");
        // Code below taken from http://answers.unity3d.com/questions/899037/applicationquit-not-working-1.html
        // save any game data here
        #if UNITY_EDITOR
            // Application.Quit() does not work in the editor so
            // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
            UnityEditor.EditorApplication.isPlaying = false;
        #else
             Application.Quit();
        #endif
    }

    // Return to menu button is pushed
    public void returnToMenu()
    {
        Debug.Log("SM_menu::returnToMenu() - Button Pushed");
        showButtons();
        activePage.SetActive(false);
    }

    public void resetScores()
    {
        PlayerPrefs.SetFloat("score", 0);
        PlayerPrefs.SetFloat("HighScore1", 0);
        PlayerPrefs.SetFloat("HighScore2", 0);
        PlayerPrefs.SetFloat("HighScore3", 0);
        scorePage.GetComponent<Text>().text =  "1. " + PlayerPrefs.GetFloat("HighScore1").ToString("F2") + "\n" +
                                               "2. " + PlayerPrefs.GetFloat("HighScore2").ToString("F2") + "\n" +
                                               "3. " + PlayerPrefs.GetFloat("HighScore3").ToString("F2");
    }
}