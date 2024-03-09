using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;
using static CutSceneTextCatalog;
using static LevelManager;

public class CutScene : MonoBehaviour
{
    
    public TextMeshProUGUI mainText;
    public TextMeshProUGUI bottomText;

    public void EnableDialogWindow()
    {
        DefineDialogText(StatesManager.currentGameState);
        GetComponent<Animator>().SetTrigger("Enable");
        EnableUserInput();
    }

    public void DisableDialogWindow()
    {
        GetComponent<Animator>().SetTrigger("Disable");
        UserInputHandler.Input_C -= CheckUserAction;
    }

    public void DefineDialogText(string currentState) 
    {
        mainText.text = CutSceneTextCatalog.FindDialogMainText(currentState);
        bottomText.text = CutSceneTextCatalog.FindDialogBottomText(currentState);
        FindObjectOfType<AudioManager>().TriggerSound(CutSceneTextCatalog.FindCutSceneVoiceOver(currentState));
    }   

    public void SwitchDialogContent()
    {
        DefineDialogText(StatesManager.currentGameState);
    }

    public void CheckUserAction()
    {
        FindObjectOfType<AudioManager>().StopSound(CutSceneTextCatalog.FindCutSceneVoiceOver(StatesManager.currentGameState));
        StatesManager.PositiveGameProgression();

        if (StatesManager.gameStates[StatesManager.currentGameState].isCutScene)
        {
            SwitchDialogContent();
        }
    }

    private async void EnableUserInput()
    {
        await Wait(2f);
        UserInputHandler.Input_C += CheckUserAction;
    }
    private async Task Wait(float time)
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
