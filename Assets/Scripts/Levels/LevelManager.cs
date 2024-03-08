using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.Threading.Tasks;
using static CameraManager;
using Newtonsoft.Json.Utilities;
using UnityEngine.InputSystem.LowLevel;

public class LevelManager : MonoBehaviour
{
    static GameObject outroSceneObject = FindGameObject("OutroScene");
    static GameObject creditsCanvas;
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

    public static async void LoadOutro()
    {
        outroSceneObject.SetActive(true);
        await StandardWait(1f);
        CameraManager.SwitchActiveCamera("OutroScene");
        await StandardWait(3f);
        outroSceneObject.transform.GetChild(1).gameObject.SetActive(true);
    }

    public static async void UnloadOutro()
    {
        outroSceneObject.transform.GetChild(0).GetComponent<Animator>().SetTrigger("Fading");
        outroSceneObject.transform.GetChild(1).gameObject.SetActive(false);
        await StandardWait(4f);
        outroSceneObject.SetActive(false);
    }

    public static async void Credits()
    {
        creditsCanvas = FindGameObject("CreditsCanvas"); 
        await StandardWait(3f);
        creditsCanvas.SetActive(true);
        await StandardWait(23f);
        FindObjectOfType<GameManagerScript>().TerminateCurrentGame();
        await StandardWait(1f);
        creditsCanvas.SetActive(false);
    }

    public static async void SecretCredits()
    {
        creditsCanvas = FindGameObject("CreditsCanvas");  // Change to secret canvas
        await StandardWait(3f);
        creditsCanvas.SetActive(true);
        await StandardWait(23f);
        FindObjectOfType<GameManagerScript>().TerminateCurrentGame();
        await StandardWait(1f);
        creditsCanvas.SetActive(false);
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
