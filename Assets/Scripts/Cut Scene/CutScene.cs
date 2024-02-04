using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static CutSceneTextCatalog;
using static LevelManager;

public class CutScene : MonoBehaviour
{
    
    public TextMeshProUGUI mainText;
    public TextMeshProUGUI bottomText;

    public void EnableDialogWindow()
    {
        GetComponent<Animator>().SetTrigger("Enable");
        DefineDialogText(StatesManager.currentGameState);
        UserInputHandler.Input_C += CheckUserAction;
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
    }

    public void SwitchDialogContent()
    {
        DefineDialogText(StatesManager.currentGameState);
    }

    public void CheckUserAction()
    {
        StatesManager.PositiveGameProgression();

        if (StatesManager.gameStates[StatesManager.currentGameState].isCutScene)
        {
            SwitchDialogContent();
        }
    }
}
