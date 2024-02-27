using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using TMPro;
using System;
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

    // Scoring
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI bottlesCollectedText;
    public TextMeshProUGUI cansCollectedText;
    public TextMeshProUGUI bananasCollectedText;
    public TextMeshProUGUI missedText;
    public TextMeshProUGUI mistakesMadeText;
    public static int score;
    public static int mistakesMade;
    public static int lostLitter;
    public static int cansCollected;
    public static int bottlesCollected;
    public static int bananasCollected;


    public void OnEnable()
    {
        // Starting the level
        allLitterSpawned = false;
        score = 0;
        scoreText.text = "";
        mistakesMade = 0;
        lostLitter = 0;
        cansCollected = 0;
        bottlesCollected = 0;
        bananasCollected = 0;

    // 113 53
    // 115 56
    // 117 59
    // 118 61
    // 120 64
    // 122.43 66.71
    // 124.67 69.49
    // 127.37 72.86
    // 129.5 76

    // Defining litter to be spawned on the level
    litterlist = new List<Litter>()
        {
            new Litter(_time: 3f, _locX: 120f, _locY: 24f, _locZ: 64f, _litterName: banana),
            new Litter(_time: 2.5f, _locX: 129.5f, _locY: 24f, _locZ: 76f, _litterName: can),
            new Litter(_time: 2.5f, _locX: 122.43f, _locY: 24f, _locZ: 66.71f, _litterName: banana),
            new Litter(_time: 2f, _locX: 117f, _locY: 24f, _locZ: 59f, _litterName: can),
            new Litter(_time: 2f, _locX: 115f, _locY: 24f, _locZ: 56f, _litterName: plasticBottle),
            new Litter(_time: 1f, _locX: 118f, _locY: 24f, _locZ: 61f, _litterName: can),
            new Litter(_time: 3.5f, _locX: 122.43f, _locY: 24f, _locZ: 66.71f, _litterName: can),
            new Litter(_time: 1f, _locX: 113f, _locY: 24f, _locZ: 53f, _litterName: plasticBottle),
            new Litter(_time: 2f, _locX: 118f, _locY: 24f, _locZ: 61f, _litterName: banana),
            new Litter(_time: 2f, _locX: 122.43f, _locY: 24f, _locZ: 66.71f, _litterName: plasticBottle),
            new Litter(_time: 3f, _locX: 127.37f, _locY: 24f, _locZ: 72.86f, _litterName: can),
            new Litter(_time: 1.5f, _locX: 129.5f, _locY: 24f, _locZ: 76f, _litterName: banana),
            new Litter(_time: 2f, _locX: 122.43f, _locY: 24f, _locZ: 66.71f, _litterName: plasticBottle),
            new Litter(_time: 2f, _locX: 118f, _locY: 24f, _locZ: 61f, _litterName: can),
            new Litter(_time: 1f, _locX: 115f, _locY: 24f, _locZ: 56f, _litterName: plasticBottle),
            new Litter(_time: 2f, _locX: 117f, _locY: 24f, _locZ: 59f, _litterName: banana),
            new Litter(_time: 2.5f, _locX: 115f, _locY: 24f, _locZ: 56f, _litterName: can),
            new Litter(_time: 1f, _locX: 122.43f, _locY: 24f, _locZ: 66.71f, _litterName: banana),
            new Litter(_time: 1f, _locX: 127.37f, _locY: 24f, _locZ: 72.86f, _litterName: plasticBottle),
            new Litter(_time: 1.5f, _locX: 124.67f, _locY: 24f, _locZ: 69.49f, _litterName: can),
            new Litter(_time: 1f, _locX: 120f, _locY: 24f, _locZ: 64f, _litterName: banana),
            new Litter(_time: 2f, _locX: 124.67f, _locY: 24f, _locZ: 69.49f, _litterName: can),
            new Litter(_time: 0.5f, _locX: 122.43f, _locY: 24f, _locZ: 66.71f, _litterName: plasticBottle),
            new Litter(_time: 1f, _locX: 118f, _locY: 24f, _locZ: 61f, _litterName: banana),
            new Litter(_time: 2.5f, _locX: 120f, _locY: 24f, _locZ: 64f, _litterName: plasticBottle),
            new Litter(_time: 1f, _locX: 117f, _locY: 24f, _locZ: 59f, _litterName: can),
            new Litter(_time: 1f, _locX: 115f, _locY: 24f, _locZ: 56f, _litterName: plasticBottle),
            new Litter(_time: 1f, _locX: 118f, _locY: 24f, _locZ: 61f, _litterName: banana),
            new Litter(_time: 2f, _locX: 124.67f, _locY: 24f, _locZ: 69.49f, _litterName: plasticBottle),
            new Litter(_time: 2f, _locX: 129.5f, _locY: 24f, _locZ: 76f, _litterName: banana)
        };

        LitterSpawner();

        // Defining dwellers to be spawned on the level
        dwellerList = new List<Dwellers>()
        {
            // Immediate spawn
            new Dwellers(_time: 0f, _travelRoute: 0, _currentTravelSpot: 3, _locX: 132f, _locY: 23f, _locZ: 74f, _dwellerModel: tom, _dwellerSpeed: 3f),
            new Dwellers(_time: 0f, _travelRoute: 3, _currentTravelSpot: 1, _locX: 140f, _locY: 24f, _locZ: 59f, _dwellerModel: cat, _dwellerSpeed: 2.5f),
            new Dwellers(_time: 0f, _travelRoute: 1, _currentTravelSpot: 2, _locX: 133f, _locY: 24f, _locZ: 77f, _dwellerModel: fenneko, _dwellerSpeed: 2.5f),
            new Dwellers(_time: 0f, _travelRoute: 1, _currentTravelSpot: 3, _locX: 128f, _locY: 24f, _locZ: 73f, _dwellerModel: fox, _dwellerSpeed: 2.5f),
            new Dwellers(_time: 0f, _travelRoute: 2, _currentTravelSpot: 2, _locX: 119f, _locY: 24f, _locZ: 57f, _dwellerModel: racoon, _dwellerSpeed: 2.5f),
            new Dwellers(_time: 0f, _travelRoute: 1, _currentTravelSpot: 0, _locX: 146f, _locY: 23f, _locZ: 91f, _dwellerModel: racoon, _dwellerSpeed: 3f),

            // New in-progress spawn
            new Dwellers(_time: 3f,   _travelRoute: 0, _currentTravelSpot: 0, _locX: 111f, _locY: 23f, _locZ: 47f, _dwellerModel: fenneko, _dwellerSpeed: 3f),
            new Dwellers(_time: 2.5f, _travelRoute: 1, _currentTravelSpot: 0, _locX: 146f, _locY: 23f, _locZ: 91f, _dwellerModel: tom, _dwellerSpeed: 3f),
            new Dwellers(_time: 2.5f, _travelRoute: 2, _currentTravelSpot: 0, _locX: 111f, _locY: 23f, _locZ: 47f, _dwellerModel: fox, _dwellerSpeed: 3f),
            new Dwellers(_time: 2.5f, _travelRoute: 0, _currentTravelSpot: 0, _locX: 111f, _locY: 23f, _locZ: 47f, _dwellerModel: frog, _dwellerSpeed: 3f),
            new Dwellers(_time: 2.5f, _travelRoute: 2, _currentTravelSpot: 0, _locX: 111f, _locY: 23f, _locZ: 47f, _dwellerModel: cat, _dwellerSpeed: 3f),
            new Dwellers(_time: 2.5f, _travelRoute: 1, _currentTravelSpot: 0, _locX: 146f, _locY: 23f, _locZ: 91f, _dwellerModel: racoon, _dwellerSpeed: 3f),
            new Dwellers(_time: 2.5f, _travelRoute: 3, _currentTravelSpot: 0, _locX: 148f, _locY: 24f, _locZ: 55f, _dwellerModel: tom, _dwellerSpeed: 2.5f),
            new Dwellers(_time: 2.5f, _travelRoute: 1, _currentTravelSpot: 0, _locX: 146f, _locY: 23f, _locZ: 91f, _dwellerModel: fox, _dwellerSpeed: 3f),
            new Dwellers(_time: 2.5f, _travelRoute: 2, _currentTravelSpot: 0, _locX: 111f, _locY: 23f, _locZ: 47f, _dwellerModel: cat, _dwellerSpeed: 3f),
            new Dwellers(_time: 2.5f, _travelRoute: 0, _currentTravelSpot: 0, _locX: 111f, _locY: 23f, _locZ: 47f, _dwellerModel: racoon, _dwellerSpeed: 3f),
            new Dwellers(_time: 2.5f, _travelRoute: 3, _currentTravelSpot: 0, _locX: 148f, _locY: 24f, _locZ: 55f, _dwellerModel: fenneko, _dwellerSpeed: 2.5f),
            new Dwellers(_time: 2.5f, _travelRoute: 1, _currentTravelSpot: 0, _locX: 146f, _locY: 24f, _locZ: 91f, _dwellerModel: retsuko, _dwellerSpeed: 2.5f),
            new Dwellers(_time: 2.5f, _travelRoute: 1, _currentTravelSpot: 0, _locX: 146f, _locY: 24f, _locZ: 91f, _dwellerModel: frog, _dwellerSpeed: 2.5f),
            new Dwellers(_time: 2.5f, _travelRoute: 0, _currentTravelSpot: 0, _locX: 111f, _locY: 23f, _locZ: 47f, _dwellerModel: fenneko, _dwellerSpeed: 3f),
            new Dwellers(_time: 2.5f, _travelRoute: 1, _currentTravelSpot: 0, _locX: 146f, _locY: 23f, _locZ: 91f, _dwellerModel: frog, _dwellerSpeed: 3f),
            new Dwellers(_time: 2.5f, _travelRoute: 2, _currentTravelSpot: 0, _locX: 111f, _locY: 23f, _locZ: 47f, _dwellerModel: cat, _dwellerSpeed: 3f),
            new Dwellers(_time: 2.5f, _travelRoute: 3, _currentTravelSpot: 0, _locX: 148f, _locY: 23f, _locZ: 55f, _dwellerModel: tom, _dwellerSpeed: 3f),
            new Dwellers(_time: 2.5f, _travelRoute: 0, _currentTravelSpot: 0, _locX: 111f, _locY: 23f, _locZ: 47f, _dwellerModel: retsuko, _dwellerSpeed: 3f),
            new Dwellers(_time: 2.5f, _travelRoute: 1, _currentTravelSpot: 0, _locX: 146f, _locY: 23f, _locZ: 91f, _dwellerModel: fox, _dwellerSpeed: 3f),
            new Dwellers(_time: 2.5f, _travelRoute: 0, _currentTravelSpot: 0, _locX: 111f, _locY: 23f, _locZ: 47f, _dwellerModel: racoon, _dwellerSpeed: 3f)
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
                Instantiate(dwellerList[i].dwellerModel, spawnPos, spawnAngle).GetComponent<DwellerScript>().setParameters(dwellerList[i].travelRoute, dwellerList[i].currentTravelSpot, dwellerList[i].dwellerSpeed);
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
            bottlesCollectedText.text = string.Format("bottles: {0}", bottlesCollected);
            cansCollectedText.text = string.Format("cans: {0}", cansCollected);
            bananasCollectedText.text = string.Format("bananas: {0}", bananasCollected);
            mistakesMadeText.text = string.Format("mistakes: {0}/5", mistakesMade);
            missedText.text = string.Format("missed: {0}/3", lostLitter);
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
