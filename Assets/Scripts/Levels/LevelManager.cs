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
        FindObjectOfType<AudioManager>().Play("CutScene", 2f);
        CameraManager.SwitchActiveCamera("CutSceneLocation");
        CutScene dialogWindow = FindObjectOfType<CutScene>();
        if (dialogWindow != null)
        {
            dialogWindow.EnableDialogWindow();
        }   
    }

    public static void UnloadCutScene()
    {
        FindObjectOfType<AudioManager>().StopMusic("CutScene", 2f);
        CutScene dialogWindow = FindObjectOfType<CutScene>();
        if (dialogWindow != null)
        {
            dialogWindow.DisableDialogWindow();
        }
    }

    public static void LoadLevel(string level)
    {
        FindObjectOfType<AudioManager>().Play("LevelPlaying", 2f);
        CameraManager.SwitchActiveCamera(level);

        FindGameObject(StatesManager.gameStates[level].stateName).SetActive(true);
    }
    public static async void UnloadLevel(string level)
    {
        if( FindGameObject(StatesManager.gameStates[level].stateName).TryGetComponent<SecondLevelScript>(out SecondLevelScript script) )
        {
            script.levelIsDisabling = true;
        }
        FindObjectOfType<AudioManager>().StopMusic("LevelPlaying", 2f);
        await StandardWait(1.5f);

        FindGameObject(StatesManager.gameStates[level].stateName).SetActive(false);
    }

    public static async void LoadOutro()
    {
        FindObjectOfType<AudioManager>().Play("Outro", 2f);
        outroSceneObject.SetActive(true);
        await StandardWait(1f);
        CameraManager.SwitchActiveCamera("OutroScene");
        await StandardWait(3f);
        outroSceneObject.transform.GetChild(1).gameObject.SetActive(true);
        FindObjectOfType<AudioManager>().TriggerSound("OutroSpeech");

    }

    public static async void UnloadOutro()
    {
        FindObjectOfType<AudioManager>().StopSound("OutroSpeech");
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
        FindObjectOfType<AudioManager>().StopMusic("Outro", 2f);
        FindObjectOfType<GameManagerScript>().TerminateCurrentGame();
        await StandardWait(1f);
        creditsCanvas.SetActive(false);
    }

    public static async void SecretCredits()
    {
        //Modified function to be triggered once max score is achieved, plays a SeCrEt ViDeO ;)
        creditsCanvas = FindGameObject("SecretCreditsCanvas");
        await StandardWait(3f);
        creditsCanvas.SetActive(true);
        await StandardWait(23f);
        FindObjectOfType<AudioManager>().StopMusic("Outro", 2f);
        creditsCanvas.transform.GetChild(1).gameObject.SetActive(true);
        creditsCanvas.transform.GetChild(2).gameObject.SetActive(true);
        await StandardWait(18f);
        creditsCanvas.transform.GetChild(1).gameObject.SetActive(false);
        creditsCanvas.transform.GetChild(2).gameObject.SetActive(false);
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
