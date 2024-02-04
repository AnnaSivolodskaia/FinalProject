using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ScoreSystem;

public class CutSceneTextCatalog : MonoBehaviour
{
    public static string FindDialogMainText(string currentGameState)
	{
        string finalText;
        switch (currentGameState)
        {
            case "Intro_1":
                finalText = "Some text for first screen.";
                break;
            case "Intro_2":
                finalText = "Some other text for second screen.";
                break;
            case "1lvl_3":
                finalText = "Level failed... :( ";
                break;
            case "1lvl_2":
                finalText = "Level completed! :) ";
                break;
            default:
                finalText = "Not defined in catalog!";
                break;
        }
        return finalText;
    }

    public static string FindDialogBottomText(string currentGameState)
    {
        string finalText;
        switch (currentGameState)
        {
            case "Intro_1":
                finalText = "Press C to continue...";
                break;
            case "Intro_2":
                finalText = "Press C to start!";
                break;
            case "1lvl_3":
                finalText = "Press C to start again!";
                break;
            case "1lvl_2":
                int currentScore = ScoreSystem.GetScore();
                finalText = string.Format("Current score is: {0}", currentScore);
                break;
            default:
                finalText = "Not defined in catalog!";
                break;
        }
        return finalText;
    }
}
