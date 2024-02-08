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
    public GameObject banana;
    public GameObject plasticBottle;
    //List<List<float>> litterlist;
    List<Litter> litterlist;
    public static int score = 0;
    public TextMeshProUGUI scoreText;
    private bool? allLitterSpawned = null;


    public void OnEnable()
    {
        // Starting the level
        allLitterSpawned = false;
        score = 0;
        scoreText.text = "";
        /*        litterlist = new List<List<float>>() {
                                                               new List<float>() { 1f, 1.0f, 110f, 24f, 65f },
                                                               new List<float>() { 1f, 1.5f, 110f, 24f, 65f },
                                                               new List<float>() { 1f, 1.5f, 110f, 24f, 65f },
                                                               new List<float>() { 1f, 1.5f, 110f, 24f, 65f },
                                                               new List<float>() { 1f, 1.5f, 110f, 24f, 65f },
                                                               new List<float>() { 1f, 1.5f, 110f, 24f, 65f },
                                                               new List<float>() { 1f, 1.5f, 110f, 24f, 65f },
                                                               new List<float>() { 1f, 1.5f, 110f, 24f, 65f }
                                                                };*/
        litterlist = new List<Litter>()
        {
            new Litter(_time: 1f, _locX: 116f, _locY: 24f, _locZ: 59f, _litterName: can),
            new Litter(_time: 2.5f, _locX: 123f, _locY: 22f, _locZ: 65f, _litterName: banana),
            new Litter(_time: 3f, _locX: 117f, _locY: 22f, _locZ: 60f, _litterName: can),
            new Litter(_time: 1f, _locX: 119f, _locY: 24f, _locZ: 62f, _litterName: plasticBottle),
            new Litter(_time: 1.5f, _locX: 126f, _locY: 24f, _locZ: 70f, _litterName: banana),
            new Litter(_time: 2.5f, _locX: 114f, _locY: 24f, _locZ: 56f, _litterName: plasticBottle),
            new Litter(_time: 1f, _locX: 121f, _locY: 24f, _locZ: 65f, _litterName: can),
            new Litter(_time: 1.5f, _locX: 116f, _locY: 24f, _locZ: 59f, _litterName: can)
          

        };

        LitterSpawner();
        
    }


    private void OnDisable()
    {
        Terminate();
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

    public async void LitterSpawner()
    {
        for (var i = 0; i < litterlist.Count; i++)
        {
            await Wait((int)(litterlist[i].time));
            if(litterlist != null)
            {
                Quaternion spawnAngle = Quaternion.Euler(0f, 0f, 0f);
                Vector3 spawnPos = new Vector3(litterlist[i].locX, litterlist[i].locY, litterlist[i].locZ);
                Instantiate(litterlist[i].littertName, spawnPos, spawnAngle);
            } else
            {
                break;
            }
            if(litterlist.Count - 1 == i)
            {
                allLitterSpawned = true;
            }
        }
        
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
        StatesManager.NegativeGameProgression();
    }

    public void LevelSuccessed()
    {
        ScoreSystem.UpdateScore(score);
        StatesManager.PositiveGameProgression();
    }
    public void Terminate()
    {
        allLitterSpawned = null;
        scoreText.text = null;
        litterlist = null;   
    }
}
