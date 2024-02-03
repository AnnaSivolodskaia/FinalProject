using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static LevelManager;
using UnityEngine.UI;
using static MainMenu;


public class StartNewGame : MonoBehaviour
{
    public Button playButton;
    public GameManagerScript _GameManagerScript;

    void Start()
    {
        playButton.onClick.AddListener(PlayButtonPressed);
        _GameManagerScript = FindObjectOfType<GameManagerScript>(); // !!! Change to FindByTag()
    }

    void PlayButtonPressed()
    {
        _GameManagerScript.StartNewGame();
    }
}
