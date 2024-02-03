using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static MainMenu;
using static CameraManager;
using UnityEngine.UI;

public class GameManagerScript : MonoBehaviour
{
    public string currentGameState;
    public List<string> possibleGameStates;
    public List<string> possibleLevelStates;

    void Start()
    {
        possibleGameStates = new List<string>() { "MainMenu", "PauseMenu", "Intro_1", "Intro_2", "1level_1", "1level_3", "1level_1", "1level_2" };
        possibleLevelStates = new List<string>() { "1level_1", "2level_1" };
        
        SetState("MainMenu");
        //Initialise camera locations dictionary
        CameraManager.InitialiseDictionary();
        MainMenu.LoadMainMenu();
    }

    public void SetState(string newState)
    {
        if(possibleGameStates.Contains(newState))
        {
            Debug.Log("Switching game state from: " + currentGameState + ". Switched to: " + newState);
            currentGameState = newState;
        } else
        {
            Debug.Log("UNDEFINED STATE " + newState);
        }
    }

    public void ProgressState()
    {
        Debug.Log("Game state progressing...");

        int nextStateIndex = possibleGameStates.IndexOf(currentGameState) + 1;
        SetState(possibleGameStates[nextStateIndex]);
    }

    public string CheckNextState()
    {
        int nextStateIndex = possibleGameStates.IndexOf(currentGameState) + 1;
        return possibleGameStates[nextStateIndex];
    }

}