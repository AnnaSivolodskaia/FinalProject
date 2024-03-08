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
        Debug.Log("Start works");
        playButton.onClick.AddListener(PlayButtonPressed);
        Debug.Log("Listnere is added");
        _GameManagerScript = FindObjectOfType<GameManagerScript>(); // !!! Change to FindByTag()
        Debug.Log(_GameManagerScript.name);

    }

    void PlayButtonPressed()
    {
        Debug.Log("Click is registered");
        _GameManagerScript.StartNewGame();
    }
}
