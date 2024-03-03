using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State// : MonoBehaviour
{
    public string stateName;
    public bool isLevel;
    public bool isCutScene;
    public string nextPositiveState;
    public string nextNegativeState;

    public State(string name, bool isLvl, bool isCutScn, string positiveState, string negativeState)
    {
        stateName = name;
        isLevel = isLvl;
        isCutScene = isCutScn;
        nextPositiveState = positiveState;
        nextNegativeState = negativeState;
    }
}
