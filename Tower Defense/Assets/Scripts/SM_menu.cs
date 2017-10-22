using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SM_menu : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    // Play button is pushed
    public void play()
    {
        Debug.Log("SM_menu::play() - Button Pushed");
    }

    // High Scores button is pushed
    public void highScores()
    {
        Debug.Log("SM_menu::highScores() - Button Pushed");
    }

    // Rules button is pushed
    public void rules()
    {
        Debug.Log("SM_menu::rules() - Button Pushed");
    }

    // Credits button is pushed
    public void credits()
    {
        Debug.Log("SM_menu::credits() - Button Pushed");
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
}