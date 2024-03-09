using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using static ScoreSystem;

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
        //In case new level is addded, it should go before outro scene
        gameStates.Add("MainMenu", new State(name: "Main Menu", isLvl: false, isCutScn: false, positiveState: "Cut_Scene_1", negativeState: null, isOutroScn: false, isCreditsScn: false));

        gameStates.Add("Cut_Scene_1", new State(name: "Introduction First Screen", isLvl: false, isCutScn: true, positiveState: "Cut_Scene_2", negativeState: null, isOutroScn: false, isCreditsScn: false));
        gameStates.Add("Cut_Scene_2", new State(name: "Introduction Second Screen", isLvl: false, isCutScn: true, positiveState: "1lvl_1", negativeState: null, isOutroScn: false, isCreditsScn: false));

        gameStates.Add("1lvl_1", new State(name: "First Level Gameplay", isLvl: true, isCutScn: false, positiveState: "1lvl_2", negativeState: "1lvl_3", isOutroScn: false, isCreditsScn: false));
        gameStates.Add("1lvl_2", new State(name: "First Level Successed", isLvl: false, isCutScn: true, positiveState: "Cut_Scene_3", negativeState: null, isOutroScn: false, isCreditsScn: false));
        gameStates.Add("1lvl_3", new State(name: "First Level Failed", isLvl: false, isCutScn: true, positiveState: "1lvl_1", negativeState: null, isOutroScn: false, isCreditsScn: false));

        gameStates.Add("Cut_Scene_3", new State(name: "Introduction Second Level", isLvl: false, isCutScn: true, positiveState: "2lvl_1", negativeState: null, isOutroScn: false, isCreditsScn: false));

        gameStates.Add("2lvl_1", new State(name: "Second Level Gameplay", isLvl: true, isCutScn: false, positiveState: "2lvl_2", negativeState: "2lvl_3", isOutroScn: false, isCreditsScn: false));
        gameStates.Add("2lvl_2", new State(name: "Second Level Successed", isLvl: false, isCutScn: true, positiveState: "Outro_Scene", negativeState: null, isOutroScn: false, isCreditsScn: false));
        gameStates.Add("2lvl_3", new State(name: "Second Level Failed", isLvl: false, isCutScn: true, positiveState: "2lvl_1", negativeState: null, isOutroScn: false, isCreditsScn: false));

        gameStates.Add("Outro_Scene", new State(name: "Outro Scene", isLvl: false, isCutScn: false, positiveState: "Credits_Scene", negativeState: null, isOutroScn: true, isCreditsScn: false));

        gameStates.Add("Credits_Scene", new State(name: "Credits Scene", isLvl: false, isCutScn: false, positiveState: null, negativeState: null, isOutroScn: false, isCreditsScn: true));

    }
    public static void SetState(string newState)
    {
        if (gameStates.ContainsKey(newState))
        {
            currentGameState = newState;
        }
        else
        {
            Debug.LogWarning("UNDEFINED STATE: " + newState);
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

        if (gameStates[currentGameState].isOutro)
        {
            LevelManager.LoadOutro();
        }
        if (gameStates[currentGameState].isCredits)
        {
            LevelManager.UnloadOutro();
            if(ScoreSystem.GetScore() >= 500)
            {
                LevelManager.SecretCredits();
            }
            else
            {
                LevelManager.Credits();
            }
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
