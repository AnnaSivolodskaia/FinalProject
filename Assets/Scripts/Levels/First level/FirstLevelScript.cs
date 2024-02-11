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
    // Litter prefabs
    public List<Litter> litterlist;
    private bool? allLitterSpawned = null;
    public GameObject can;
    public GameObject banana;
    public GameObject plasticBottle;

    // Dwellers prefabs
    public List<Dwellers> dwellerList;
    public GameObject fox;
    public GameObject racoon;
    public GameObject tom;
    public GameObject cat;
    public GameObject frog;
    public GameObject fenneko;
    public GameObject retsuko;

    // General
    public static int score = 0;
    public TextMeshProUGUI scoreText;


    public void OnEnable()
    {
        // Starting the level
        allLitterSpawned = false;
        score = 0;
        scoreText.text = "";

        // Defining litter to be spawned on the level
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

        //LitterSpawner();

        // Defining dwellers to be spawned on the level
        dwellerList = new List<Dwellers>()
        {
            new Dwellers(_time: 1.5f, _travelRoute: 0, _currentTravelSpot: 0, _locX: 111f, _locY: 23f, _locZ: 47f, _dwellerModel: fenneko, _dwellerSpeed: 1f),
            new Dwellers(_time: 1.5f, _travelRoute: 1, _currentTravelSpot: 0, _locX: 146f, _locY: 23f, _locZ: 91f, _dwellerModel: fenneko, _dwellerSpeed: 1f),

            new Dwellers(_time: 1.5f, _travelRoute: 3, _currentTravelSpot: 0, _locX: 148f, _locY: 24f, _locZ: 55f, _dwellerModel: fenneko, _dwellerSpeed: 1f),

            new Dwellers(_time: 1.5f, _travelRoute: 2, _currentTravelSpot: 0, _locX: 111f, _locY: 23f, _locZ: 47f, _dwellerModel: fenneko, _dwellerSpeed: 1f),
            new Dwellers(_time: 1.5f, _travelRoute: 1, _currentTravelSpot: 0, _locX: 146f, _locY: 23f, _locZ: 91f, _dwellerModel: fenneko, _dwellerSpeed: 1f),

            new Dwellers(_time: 1.5f, _travelRoute: 3, _currentTravelSpot: 0, _locX: 148f, _locY: 24f, _locZ: 55f, _dwellerModel: fenneko, _dwellerSpeed: 1f),

            new Dwellers(_time: 1.5f, _travelRoute: 0, _currentTravelSpot: 0, _locX: 111f, _locY: 23f, _locZ: 47f, _dwellerModel: fenneko, _dwellerSpeed: 1f),
            new Dwellers(_time: 1.5f, _travelRoute: 1, _currentTravelSpot: 0, _locX: 146f, _locY: 23f, _locZ: 91f, _dwellerModel: fenneko, _dwellerSpeed: 1f),
            new Dwellers(_time: 1.5f, _travelRoute: 2, _currentTravelSpot: 0, _locX: 111f, _locY: 23f, _locZ: 47f, _dwellerModel: fenneko, _dwellerSpeed: 1f),
            new Dwellers(_time: 1.5f, _travelRoute: 1, _currentTravelSpot: 0, _locX: 146f, _locY: 23f, _locZ: 91f, _dwellerModel: fenneko, _dwellerSpeed: 1f),
            new Dwellers(_time: 1.5f, _travelRoute: 0, _currentTravelSpot: 0, _locX: 111f, _locY: 23f, _locZ: 47f, _dwellerModel: fenneko, _dwellerSpeed: 1f),
            new Dwellers(_time: 1.5f, _travelRoute: 1, _currentTravelSpot: 0, _locX: 146f, _locY: 23f, _locZ: 91f, _dwellerModel: fenneko, _dwellerSpeed: 1f),
            new Dwellers(_time: 1.5f, _travelRoute: 2, _currentTravelSpot: 0, _locX: 111f, _locY: 23f, _locZ: 47f, _dwellerModel: fenneko, _dwellerSpeed: 1f),
            new Dwellers(_time: 1.5f, _travelRoute: 1, _currentTravelSpot: 0, _locX: 146f, _locY: 23f, _locZ: 91f, _dwellerModel: fenneko, _dwellerSpeed: 1f)
        };

        DwellerSpawner();

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

    public async void DwellerSpawner()
    {
        for (var i = 0; i < dwellerList.Count; i++)
        {
            await Wait((int)(dwellerList[i].time));
            if (dwellerList != null)
            {
                Quaternion spawnAngle = Quaternion.Euler(0f, 0f, 0f);
                Vector3 spawnPos = new Vector3(dwellerList[i].locX, dwellerList[i].locY, dwellerList[i].locZ);
                Instantiate(dwellerList[i].dwellerModel, spawnPos, spawnAngle).GetComponent<DwellerScript>().setTravelRoute(dwellerList[i].travelRoute, dwellerList[i].currentTravelSpot);
            }
            else
            {
                break;
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
        litterlist = null;
        dwellerList = null;
        scoreText.text = null;
    }
}
