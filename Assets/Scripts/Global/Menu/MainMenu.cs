using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CameraManager;
using static ScoreSystem;

public class MainMenu : MonoBehaviour
{
    public static GameObject menuCanvas = GameObject.Find("MainMenuCanvas");
    
    public static void LoadMainMenu()
    {
        //Activate background environment and animations 

        CameraManager.SwitchActiveCamera("MainMenuLocation");
        menuCanvas.SetActive(true);
        ScoreSystem.ResetScore();
    }

    public static void UnloadMainMenu()
    {
        //Deactivate background environment and animations 
        menuCanvas.SetActive(false);
    }
}
