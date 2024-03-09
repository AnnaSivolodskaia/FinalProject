using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State
{
    public string stateName;
    public bool isLevel;
    public bool isCutScene;
    public bool isOutro;
    public bool isCredits;
    public string nextPositiveState;
    public string nextNegativeState;

    public State(string name, bool isLvl, bool isCutScn, string positiveState, string negativeState, bool isOutroScn, bool isCreditsScn)
    {
        stateName = name;
        isLevel = isLvl;
        isCutScene = isCutScn;
        isOutro = isOutroScn;
        isCredits = isCreditsScn;
        nextPositiveState = positiveState;
        nextNegativeState = negativeState;
    }
}
