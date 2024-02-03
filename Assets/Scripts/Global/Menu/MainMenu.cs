using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CameraManager;
using static ScoreSystem;

public class MainMenu : MonoBehaviour
{
    public static GameObject menuCanvas = GameObject.Find("MenuCanvas");
    
    public static void LoadMainMenu()
    {
        CameraManager.SwitchActiveCamera("MainMenuLocation");
        menuCanvas.SetActive(true);
        ScoreSystem.ResetScore();
    }

    public static void UnloadMainMenu()
    {
        menuCanvas.SetActive(false);
    }
}
