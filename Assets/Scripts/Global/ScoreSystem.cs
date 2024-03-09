using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreSystem : MonoBehaviour
{
    public static int gameScore = 0;

    public static void ResetScore()
    {
        gameScore = 0;
    }

    public static void UpdateScore(int score)
    {
        gameScore += score;
    }

    public static int GetScore()
    {
        return gameScore;
    }
}
