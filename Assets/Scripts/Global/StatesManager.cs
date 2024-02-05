using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class StatesManager : MonoBehaviour
{
    public static Dictionary<string, State> gameStates = new();
    public static string currentGameState;

    public static bool wasLvl;
    public static bool wasCutScn;
    public static string wasState;

    public static void Initiate()
    {
        //Defining all possible states
        gameStates.Add("MainMenu", new State(name: "Main Menu", isLvl: false, isCutScn: false, positiveState: "Intro_1", negativeState: null));

        gameStates.Add("Intro_1", new State(name: "Introduction First Screen", isLvl: false, isCutScn: true, positiveState: "Intro_2", negativeState: null));
        gameStates.Add("Intro_2", new State(name: "Introduction Second Screen", isLvl: false, isCutScn: true, positiveState: "1lvl_1", negativeState: null));

        gameStates.Add("1lvl_1", new State(name: "First Level Gameplay", isLvl: true, isCutScn: false, positiveState: "1lvl_2", negativeState: "1lvl_3"));
        gameStates.Add("1lvl_2", new State(name: "First Level Successed", isLvl: false, isCutScn: true, positiveState: null, negativeState: null));
        gameStates.Add("1lvl_3", new State(name: "First Level Failed", isLvl: false, isCutScn: true, positiveState: "1lvl_1", negativeState: null));
    }
    public static void SetState(string newState)
    {
        if (gameStates.ContainsKey(newState))
        {
            Debug.Log("Switching game state from: " + currentGameState + ". Switched to: " + newState);
            currentGameState = newState;
        }
        else
        {
            Debug.Log("UNDEFINED STATE " + newState);
        }
    }

    public static void PositiveGameProgression()
    {
        wasLvl = gameStates[currentGameState].isLevel;
        wasCutScn = gameStates[currentGameState].isCutScene;
        wasState = currentGameState;

        SetState(gameStates[currentGameState].nextPositiveState);

        if (gameStates[currentGameState].isCutScene && !wasCutScn)
        {
            LevelManager.LoadCutScene();
        }

        if (!gameStates[currentGameState].isCutScene && wasCutScn)
        {
            LevelManager.UnloadCutScene();
        }

        if (gameStates[currentGameState].isLevel && !wasLvl)
        {
            LevelManager.LoadLevel(currentGameState);
        }

        if (!gameStates[currentGameState].isLevel && wasLvl)
        {
            LevelManager.UnloadLevel(wasState);
        }
    }

    public static void NegativeGameProgression()
    {
        wasLvl = gameStates[currentGameState].isLevel;
        wasCutScn = gameStates[currentGameState].isCutScene;
        wasState = currentGameState;

        SetState(gameStates[currentGameState].nextNegativeState);

        if (gameStates[currentGameState].isCutScene && !wasCutScn)
        {
            LevelManager.LoadCutScene();
        }

        if (!gameStates[currentGameState].isLevel && wasLvl)
        {
            LevelManager.UnloadLevel(wasState);
        }
    }
}
