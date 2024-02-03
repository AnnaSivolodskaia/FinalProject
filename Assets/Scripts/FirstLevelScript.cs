using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using TMPro;
using System;
using Unity.UI;
using static LevelManager;

public class FirstLevelScript : MonoBehaviour
{
    public GameObject can;
    List<List<float>> litterlist;
    public static int score = 0;
    public GameObject ScoreCanvas;
    public TextMeshProUGUI scoreText;
    private bool? allLitterSpawned = false;


    public void Initiate()
    {
        allLitterSpawned = false;
        scoreText.text = "";
        litterlist = new List<List<float>>() {
                                               new List<float>() { 1f, 6.0f, 110f, 24f, 65f },
                                               new List<float>() { 1f, 7.5f, 110f, 24f, 65f },
                                               new List<float>() { 1f, 5.5f, 110f, 24f, 65f }
                                                };
        StartLevel();
        ScoreCanvas.SetActive(true);
        
    }

    private void Update()
    {
        if(scoreText.text != null)
        {
            scoreText.text = String.Format("Score:  {0}", score);
        }

        if(allLitterSpawned == true && FindObjectOfType<LitterScript>() == null && litterlist != null)
        {
            allLitterSpawned = null;
            LevelSuccessed();
        }
    }

    public void StartLevel()
    {
        Spawner();
    }

    public void Terminate()
    {
        allLitterSpawned = null;
        ScoreCanvas.SetActive(false);
        score = 0;
        scoreText.text = null;
        litterlist = null;
        FindObjectOfType<GameManagerScript>().SetState("1level_3");
        LevelManager.FirstLevelCompleted();
    }

    public void LevelSuccessed()
    {
        ScoreCanvas.SetActive(false);
        scoreText.text = null;
        FindObjectOfType<GameManagerScript>().SetState("1level_2");
        LevelManager.FirstLevelCompleted();
    }

    public async void Spawner()
    {
        for (var i = 0; i < litterlist.Count; i++)
        {
            await Task.Delay((int)(litterlist[i][1] * 1000));
            if(litterlist != null)
            {
                Quaternion spawnAngle = Quaternion.Euler(0f, 0f, 0f);
                Vector3 spawnPos = new Vector3(litterlist[i][2], litterlist[i][3], litterlist[i][4]);
                Instantiate(can, spawnPos, spawnAngle);
                Debug.Log(i);
            } else
            {
                break;
            }
            
        }
        allLitterSpawned = true;
    }

}
