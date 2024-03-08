using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
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
        menuCanvas.GetComponent<Animator>().SetTrigger("Enable");
        menuCanvas.transform.GetChild(0).gameObject.SetActive(true);
        ScoreSystem.ResetScore();
    }

    public static void UnloadMainMenu()
    {
        // ! Deactivate background environment and animations 
        menuCanvas.GetComponent<Animator>().SetTrigger("Disable");
        menuCanvas.transform.GetChild(0).gameObject.SetActive(false);
    }
}
