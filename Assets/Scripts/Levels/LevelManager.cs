using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.Threading.Tasks;
using static CameraManager;
using Newtonsoft.Json.Utilities;

public class LevelManager : MonoBehaviour
{
    public static void LoadCutScene()
    {
        //Switch camera
        CameraManager.SwitchActiveCamera("CutSceneLocation");
        //Activate dialog window
        CutScene dialogWindow = FindObjectOfType<CutScene>(); // !!! Change to FindByTag()
        if (dialogWindow != null)
        {
            dialogWindow.EnableDialogWindow();
        }   
    }

    public static void UnloadCutScene()
    {
        //Deactivate dialog window
        CutScene dialogWindow = FindObjectOfType<CutScene>(); // !!! Change to FindByTag()
        if (dialogWindow != null)
        {
            dialogWindow.DisableDialogWindow();
        }
    }

    public static void LoadLevel(string level)
    {
        CameraManager.SwitchActiveCamera(level);

        FindGameObject(StatesManager.gameStates[level].stateName).SetActive(true);
    }
    public static async void UnloadLevel(string level)
    {
        await StandardWait(1.5f);

        FindGameObject(StatesManager.gameStates[level].stateName).SetActive(false);
    }

    public static void LoadOutro()
    {

    }

    public static void UnloadOutro()
    {

    }

    public static void LoadCredits()
    {

    }

    public static void UnloadCredits()
    {

    }

    public static void LoadSecretCredits()
    {

    }

    public static void UnloadSecretCredits()
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

    private static async Task StandardWait(float time)
    {
        float startTime = Time.time;
        float currentTime = startTime;
        while (currentTime - startTime < time)
        {
            currentTime += Time.deltaTime;
            await Task.Yield();
        }
    }
}
