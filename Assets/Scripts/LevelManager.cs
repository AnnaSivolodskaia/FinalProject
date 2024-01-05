using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CameraManager;

public class LevelManager : MonoBehaviour
{
    public static void LoadIntro()
    {
        //Switch camera
        CameraManager.SwitchActiveCamera("IntroLocation");
        //Activate Intro Pop up
        DialogWindow dialogWindow = FindObjectOfType<DialogWindow>(); // !!! Change to FindByTag()
        if (dialogWindow != null)
        {
            dialogWindow.EnableDialogWindow();
        }   
    }

    public static void LoadNextLevel(int score, string level)
    {
        CameraManager.SwitchActiveCamera(level);
        FirstLevelScript firstLevelScript = FindObjectOfType<FirstLevelScript>();
        firstLevelScript.Initiate();
    }
    public static void FirstLevelCompleted()
    {
        CameraManager.SwitchActiveCamera("IntroLocation");
        DialogWindow dialogWindow = FindObjectOfType<DialogWindow>(); // !!! Change to FindByTag()
        if (dialogWindow != null)
        {
            dialogWindow.EnableDialogWindow();
        }
    }

    public static void LoadOutro()
    {

    }

    public static void LoadCredits()
    {

    }

    public static void LoadSecretCredits()
    {

    }
}
