using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using UnityEngine.SocialPlatforms.Impl;
using TMPro;
using System;

public class SecondLevelScript : MonoBehaviour
{

    // Dwellers prefabs
    public List<SecondLevelDwellers> dwellerList;
    public GameObject fox;
    public GameObject racoon;
    public GameObject tom;
    public GameObject cat;
    public GameObject frog;
    public GameObject fenneko;
    public GameObject retsuko;
    public GameObject crateModel;

    // Lists and flags for spawners
    public List<List<float>> possibleSpawnSpots;
    public bool? allDwellersSpawned;
    public List<List<float>> cratesListSpawnSpots;

    // Winning condition helping flag
    public bool levelCompleted;
    public bool levelIsDisabling;

    // Scoring
    public TextMeshProUGUI scoreText;
    public int score;
    public TextMeshProUGUI lostDwellersText;
    public int lostDwellers;
    public Animator scoreCanvasAnimator;




    private void OnEnable()
    {
        score = 0;
        lostDwellers = 0;
        scoreText.text = "";
        allDwellersSpawned = false;
        levelCompleted = false;
        levelIsDisabling = false;
        scoreCanvasAnimator.SetBool("Disabled", false);


        // Dwellers spawn points
        possibleSpawnSpots = new List<List<float>> { new List<float> { 176f, 3f }, new List<float> { 147f, -59f }, new List<float> { 103f, -17f }, new List<float> { 103f, -17f } };

        // Dwellers
        dwellerList = new List<SecondLevelDwellers>
        {
            new SecondLevelDwellers(_spawnAwait: 1, _dwellerModel: cat, _spawnPoint: 1, _parentQueue: "second_queue", _patienceCapacity: 5, _isServed: false, _isStuck: false),
            new SecondLevelDwellers(_spawnAwait: 1, _dwellerModel: frog, _spawnPoint: 2, _parentQueue: "third_queue", _patienceCapacity: 5, _isServed: false, _isStuck: false),
            new SecondLevelDwellers(_spawnAwait: 1, _dwellerModel: frog, _spawnPoint: 0, _parentQueue: "first_queue", _patienceCapacity: 5, _isServed: false, _isStuck: false),
            new SecondLevelDwellers(_spawnAwait: 1, _dwellerModel: fox, _spawnPoint: 3, _parentQueue: "fourth_queue", _patienceCapacity: 5, _isServed: false, _isStuck: false),

            new SecondLevelDwellers(_spawnAwait: 1, _dwellerModel: tom, _spawnPoint: 1, _parentQueue: "second_queue", _patienceCapacity: 5, _isServed: false, _isStuck: true),
            new SecondLevelDwellers(_spawnAwait: 1, _dwellerModel: racoon, _spawnPoint: 2, _parentQueue: "third_queue", _patienceCapacity: 15, _isServed: false, _isStuck: true),
            new SecondLevelDwellers(_spawnAwait: 1, _dwellerModel: cat, _spawnPoint: 0, _parentQueue: "first_queue", _patienceCapacity: 15, _isServed: false, _isStuck: true),
            new SecondLevelDwellers(_spawnAwait: 1, _dwellerModel: tom, _spawnPoint: 3, _parentQueue: "fourth_queue", _patienceCapacity: 15, _isServed: false, _isStuck: true),

            new SecondLevelDwellers(_spawnAwait: 1, _dwellerModel: retsuko, _spawnPoint: 1, _parentQueue: "second_queue", _patienceCapacity: 15, _isServed: false, _isStuck: false),
            new SecondLevelDwellers(_spawnAwait: 1, _dwellerModel: fenneko, _spawnPoint: 2, _parentQueue: "third_queue", _patienceCapacity: 15, _isServed: false, _isStuck: false),
            new SecondLevelDwellers(_spawnAwait: 1, _dwellerModel: fox, _spawnPoint: 0, _parentQueue: "first_queue", _patienceCapacity: 15, _isServed: false, _isStuck: false),
            new SecondLevelDwellers(_spawnAwait: 1, _dwellerModel: fenneko, _spawnPoint: 3, _parentQueue: "fourth_queue", _patienceCapacity: 15, _isServed: false, _isStuck: false)
        };


        DwellerSpawner();

        // Crates
        cratesListSpawnSpots = new List<List<float>>
        {
            new List<float> { 154f, 16f },
            new List<float> { 150f, 16f },
            new List<float> { 145f, 20f }
        };

        CratesSpawner();

    }

    private async Task Wait(float time)
    {
        float startTime = Time.time;
        float currentTime = startTime;
        if (StatesManager.gameStates[StatesManager.currentGameState].isLevel)
        {
            while (currentTime - startTime < time)
            {
                currentTime += Time.deltaTime;
                await Task.Yield();
            }
        }
    }
    
    
    public async void DwellerSpawner()
    {
        for (var i = 0; i < dwellerList.Count; i++)
        {
            await Wait(dwellerList[i].spawnAwait);
            if (dwellerList != null)
            {
                Quaternion spawnAngle = Quaternion.Euler(0f, 0f, 0f);
                Vector3 spawnPos = new Vector3(possibleSpawnSpots[dwellerList[i].spawnPoint][0], 20.2f, possibleSpawnSpots[dwellerList[i].spawnPoint][1]);
                GameObject spawnedDweller = Instantiate(dwellerList[i].dwellerModel, spawnPos, spawnAngle);
                int spawnedDwellerPlaceInQueue = GameObject.Find(dwellerList[i].parentQueue).GetComponent<DwellersQueue>().AddDweller(spawnedDweller);
                spawnedDweller.GetComponent<SecondLevelDwellerScript>().setParameters(_parentQueue: dwellerList[i].parentQueue, _patienceCapacity: dwellerList[i].patienceCapacity, _isStuck: dwellerList[i].isStuck, _placeInQueue: spawnedDwellerPlaceInQueue);
            }
            else
            {
                break;
            }
        }
        allDwellersSpawned = true;


    }

    public void CratesSpawner()
    {
        for (var i = 0; i < cratesListSpawnSpots.Count; i++)
        {
            if (cratesListSpawnSpots != null)
            {
                Quaternion spawnAngle = Quaternion.Euler(0f, 0f, 0f);
                Vector3 spawnPos = new Vector3(cratesListSpawnSpots[i][0], 25f, cratesListSpawnSpots[i][1]);
                Instantiate(crateModel, spawnPos, spawnAngle);
            }
            else
            {
                break;
            }
        }


    }


    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (scoreText.text != null)
        {
            scoreText.text = String.Format("Score:  {0}", score);
            lostDwellersText.text = string.Format("Dwellers left: {0}/5", lostDwellers);
        }

        if (!levelIsDisabling)
        {
            // Checking winning condition
            if (allDwellersSpawned == true)
            {
                levelCompleted = true;
                foreach (var remainedDweller in FindObjectsOfType<SecondLevelDwellerScript>())
                {
                    if (!remainedDweller.isExiting)
                    {
                        levelCompleted = false;
                    }
                }
                if (levelCompleted)
                {
                    allDwellersSpawned = null;
                    LevelSuccessed();
                }
            }

            if(lostDwellers > 4)
            {
                levelIsDisabling = true;
                LevelFailed();
            }
        }


    }

    public void LevelFailed()
    {
        scoreCanvasAnimator.SetBool("Disabled", true);
        StatesManager.NegativeGameProgression();
    }

    public void LevelSuccessed()
    {
        ScoreSystem.UpdateScore(score);
        scoreCanvasAnimator.SetBool("Disabled", true);
        StatesManager.PositiveGameProgression();
    }

    private void OnDisable()
    {
        scoreCanvasAnimator.SetBool("Disabled", true);
        Debug.Log("Canvas disabled");
        Terminate();

    }

    public void Terminate()
    {
        foreach (var crate in GameObject.FindGameObjectsWithTag("secondLevelCrate"))
        {
            Destroy(crate);
        }

        cratesListSpawnSpots = null;
        dwellerList = null;
        scoreText.text = null;
    }
}
