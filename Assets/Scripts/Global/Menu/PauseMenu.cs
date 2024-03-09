using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public static bool gameIsPaused = false;
    public GameObject pauseMenuUI;
    public GameObject restartButton;
    public GameManagerScript _GameManagerScript;

    void Update()
    {
        if( (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.JoystickButton7)) && StatesManager.currentGameState != "MainMenu")
        {
            if(gameIsPaused)
            {
                Resume();
            } else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        if (!StatesManager.gameStates[StatesManager.currentGameState].isLevel)
        {
            restartButton.GetComponent<Button>().interactable = false;
            restartButton.GetComponent<Image>().color = Color.gray;
        } else
        {
            restartButton.GetComponent<Button>().interactable = true;
            restartButton.GetComponent<Image>().color = new Color(0.9568628f, 0.9529412f, 0.8352942f, 1f);
        }
        
        Time.timeScale = 0f;
        gameIsPaused = true;
    }

    public void LoadMenu()
    {
        _GameManagerScript = FindObjectOfType<GameManagerScript>();
        _GameManagerScript.TerminateCurrentGame();
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void RestartLevel()
    {
        if (StatesManager.gameStates[StatesManager.currentGameState].isLevel)
        {
            LevelManager.UnloadLevel(StatesManager.currentGameState);

            List<string> keysList = new List<string>(StatesManager.gameStates.Keys);

            StatesManager.SetState(keysList[keysList.IndexOf(StatesManager.currentGameState) - 1]);

            LevelManager.LoadCutScene();

            Resume();
        }
    }
 }