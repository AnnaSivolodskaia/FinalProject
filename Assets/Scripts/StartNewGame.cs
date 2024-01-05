using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static LevelManager;
using UnityEngine.UI;
using static MenuManager;


public class StartNewGame : MonoBehaviour
{
    public Button playButton;
    public GameManagerScript _GameManagerScript;

    void Start()
    {
        playButton.onClick.AddListener(PlayButtonPressed);
        _GameManagerScript = FindObjectOfType<GameManagerScript>(); // !!! Change to FindByTag()
        Debug.Log("Debug Start");
    }

    void PlayButtonPressed()
    {
        _GameManagerScript.SetState("Intro_1");
        MenuManager.UnloadMainMenu();
        LevelManager.LoadIntro();
    }
}
