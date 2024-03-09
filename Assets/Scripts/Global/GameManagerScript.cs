using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static MainMenu;
using static CameraManager;
using UnityEngine.UI;
using JetBrains.Annotations;

public class GameManagerScript : MonoBehaviour
{
    public List<string> possibleGameStates;
    public List<string> possibleLevelStates;


    void Start()
    {
        StatesManager.Initiate();
        StatesManager.SetState("MainMenu");

        //Initiate camera locations dictionary
        CameraManager.InitiateDictionary();
        MainMenu.LoadMainMenu();
    }

    public void StartNewGame()
    {
        //Initiating new game from the main menu
        StatesManager.PositiveGameProgression();
        MainMenu.UnloadMainMenu();
    }

    public void TerminateCurrentGame()
    {
        //Reseting the game progress and clean up memory
        MainMenu.LoadMainMenu();
        
        if (StatesManager.gameStates[StatesManager.currentGameState].isLevel)
        {
            LevelManager.UnloadLevel(StatesManager.currentGameState);
        }

        if (StatesManager.gameStates[StatesManager.currentGameState].isCutScene)
        {
            LevelManager.UnloadCutScene();
        }

        StatesManager.SetState("MainMenu");
    }
}