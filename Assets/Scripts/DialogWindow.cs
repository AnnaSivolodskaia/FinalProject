using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static DialogWindowTextCatalog;
using static LevelManager;

public class DialogWindow : MonoBehaviour
{
    
    public TextMeshProUGUI mainText;
    public TextMeshProUGUI bottomText;
    public GameManagerScript _GameManagerScript;

    public void EnableDialogWindow()
    {
        _GameManagerScript = FindObjectOfType<GameManagerScript>(); // !!! Change to FindByTag()
        GetComponent<Animator>().SetTrigger("Enable");
        DefineDialogText(_GameManagerScript.currentGameState);
        UserInputHandler.Input_C += CheckUserAction;
    }

    public void DisableDialogWindow()
    {
        GetComponent<Animator>().SetTrigger("Disable");
        UserInputHandler.Input_C -= CheckUserAction;
    }

    public void DefineDialogText(string currentState) 
    {
        mainText.text = DialogWindowTextCatalog.FindDialogMainText(currentState);
        bottomText.text = DialogWindowTextCatalog.FindDialogBottomText(currentState);
    }


    public void SwitchDialogContent()
    {
        _GameManagerScript.ProgressState();
        DefineDialogText(_GameManagerScript.currentGameState);


    }

    public void CheckUserAction()
    {
        if (_GameManagerScript.possibleLevelStates.Contains(_GameManagerScript.CheckNextState()))
        {
            DisableDialogWindow();
            _GameManagerScript.ProgressState();
            LevelManager.LoadNextLevel(0, _GameManagerScript.currentGameState);
        } else
        {
            SwitchDialogContent();
        }
    }
}
