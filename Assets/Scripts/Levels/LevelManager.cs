using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static CameraManager;

public class LevelManager : MonoBehaviour
{

    public static void LoadIntro()
    {
        //Switch camera
        CameraManager.SwitchActiveCamera("IntroLocation");
        //Activate Intro Pop up
        CutScene dialogWindow = FindObjectOfType<CutScene>(); // !!! Change to FindByTag()
        if (dialogWindow != null)
        {
            dialogWindow.EnableDialogWindow();
        }   
    }

    public static void LoadNextLevel(int score, string level)
    {
        CameraManager.SwitchActiveCamera(level);

        FindGameObject("FirstLevel").SetActive(true);
    }
    public static void LevelExit()
    {
        CameraManager.SwitchActiveCamera("IntroLocation");
        CutScene dialogWindow = FindObjectOfType<CutScene>(); // !!! Change to FindByTag()
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

    public static GameObject FindGameObject(string objectTag)
    {
        GameObject returnObject = null;

        foreach (GameObject objectOnCanvas in Resources.FindObjectsOfTypeAll(typeof(GameObject)) as GameObject[])
        {
            if (objectOnCanvas.tag == objectTag)
            {
                returnObject = objectOnCanvas;
            }
        }

        return returnObject;
        
    }
}
