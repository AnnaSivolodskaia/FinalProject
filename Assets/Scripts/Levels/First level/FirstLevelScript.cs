using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using TMPro;
using System;
using Unity.UI;
using static LevelManager;
using static ScoreSystem;

public class FirstLevelScript : MonoBehaviour
{
    public GameObject can;
    List<List<float>> litterlist;
    public static int score = 0;
    public TextMeshProUGUI scoreText;
    private bool? allLitterSpawned = null;


    public void OnEnable()
    {
        // Starting the level
        allLitterSpawned = false;
        score = 0;
        scoreText.text = "";
        litterlist = new List<List<float>>() {
                                               new List<float>() { 1f, 1.0f, 110f, 24f, 65f },
                                               new List<float>() { 1f, 1.5f, 110f, 24f, 65f },
                                               new List<float>() { 1f, 1.5f, 110f, 24f, 65f },
                                               new List<float>() { 1f, 1.5f, 110f, 24f, 65f },
                                               new List<float>() { 1f, 1.5f, 110f, 24f, 65f },
                                               new List<float>() { 1f, 1.5f, 110f, 24f, 65f },
                                               new List<float>() { 1f, 1.5f, 110f, 24f, 65f },
                                               new List<float>() { 1f, 1.5f, 110f, 24f, 65f }
                                                };
        
        Spawner();
        
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
            } else
            {
                break;
            }
            
        }
        allLitterSpawned = true;
    }
    private void Update()
    {
        // Updating score
        if(scoreText.text != null)
        {
            scoreText.text = String.Format("Score:  {0}", score);
        }

        // Checking winning condition
        if (allLitterSpawned == true && FindObjectOfType<LitterScript>() == null && litterlist != null)
        {
            allLitterSpawned = null;
            LevelSuccessed();
        }
    }

    public void LevelFailed()
    {
        FindObjectOfType<GameManagerScript>().SetState("1level_3");
        
        Terminate();
    }

    public void LevelSuccessed()
    {
        ScoreSystem.UpdateScore(score);
        FindObjectOfType<GameManagerScript>().SetState("1level_2");
     
        Terminate();
    }
    public void Terminate()
    {
        allLitterSpawned = null;
        scoreText.text = null;
        litterlist = null;
        
        LevelManager.LevelExit();

        gameObject.SetActive(false);
    }




}
