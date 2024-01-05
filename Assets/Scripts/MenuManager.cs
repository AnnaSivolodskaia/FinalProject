using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CameraManager;

public class MenuManager : MonoBehaviour
{
    public static GameObject menuCanvas = GameObject.Find("MenuCanvas");
    
    public static void LoadMainMenu()
    {
        CameraManager.SwitchActiveCamera("MainMenuLocation");
        menuCanvas.SetActive(true);
    }

    public static void UnloadMainMenu()
    {
        menuCanvas.SetActive(false);
    }

    public static void LoadPauseMenu()
    {

    }

    public static void UnloadPauseMenu()
    {

    }
}
