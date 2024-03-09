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
    public List<List<float>> cratesListSpawnSpots;
    public bool? allDwellersSpawned;

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
        // Starting the level
        score = 0;
        lostDwellers = 0;
        scoreText.text = "";
        allDwellersSpawned = false;
        levelCompleted = false;
        levelIsDisabling = false;
        scoreCanvasAnimator.SetBool("Disabled", false);


        // Dwellers spawn points
        possibleSpawnSpots = new List<List<float>> { new List<float> { 176f, 3f }, new List<float> { 147f, -59f }, new List<float> { 103f, -17f }, new List<float> { 103f, -17f } };

        // Defining dwellers to be spawned on the level
        dwellerList = new List<SecondLevelDwellers>
        {
            new SecondLevelDwellers(_spawnAwait: 1, _dwellerModel: cat, _spawnPoint: 1, _parentQueue: "second_queue", _patienceCapacity: 10, _isServed: false, _isStuck: false),
            new SecondLevelDwellers(_spawnAwait: 1, _dwellerModel: racoon, _spawnPoint: 2, _parentQueue: "third_queue", _patienceCapacity: 6, _isServed: false, _isStuck: false),
            new SecondLevelDwellers(_spawnAwait: 1, _dwellerModel: frog, _spawnPoint: 0, _parentQueue: "first_queue", _patienceCapacity: 8, _isServed: false, _isStuck: false),
            new SecondLevelDwellers(_spawnAwait: 1, _dwellerModel: fox, _spawnPoint: 3, _parentQueue: "fourth_queue", _patienceCapacity: 13, _isServed: false, _isStuck: false),

            new SecondLevelDwellers(_spawnAwait: 1, _dwellerModel: tom, _spawnPoint: 1, _parentQueue: "second_queue", _patienceCapacity: 13, _isServed: false, _isStuck: false),
            new SecondLevelDwellers(_spawnAwait: 1, _dwellerModel: racoon, _spawnPoint: 2, _parentQueue: "third_queue", _patienceCapacity: 13, _isServed: false, _isStuck: false),
            new SecondLevelDwellers(_spawnAwait: 1, _dwellerModel: cat, _spawnPoint: 0, _parentQueue: "first_queue", _patienceCapacity: 13, _isServed: false, _isStuck: true),
            new SecondLevelDwellers(_spawnAwait: 1, _dwellerModel: tom, _spawnPoint: 3, _parentQueue: "fourth_queue", _patienceCapacity: 13, _isServed: false, _isStuck: false),

            new SecondLevelDwellers(_spawnAwait: 2, _dwellerModel: retsuko, _spawnPoint: 1, _parentQueue: "second_queue", _patienceCapacity: 13, _isServed: false, _isStuck: false),
            new SecondLevelDwellers(_spawnAwait: 2, _dwellerModel: frog, _spawnPoint: 2, _parentQueue: "third_queue", _patienceCapacity: 13, _isServed: false, _isStuck: true),
            new SecondLevelDwellers(_spawnAwait: 2, _dwellerModel: fox, _spawnPoint: 0, _parentQueue: "first_queue", _patienceCapacity: 13, _isServed: false, _isStuck: false),
            new SecondLevelDwellers(_spawnAwait: 2, _dwellerModel: tom, _spawnPoint: 3, _parentQueue: "fourth_queue", _patienceCapacity: 13, _isServed: false, _isStuck: true),

            new SecondLevelDwellers(_spawnAwait: 2, _dwellerModel: racoon, _spawnPoint: 1, _parentQueue: "second_queue", _patienceCapacity: 13, _isServed: false, _isStuck: true),
            new SecondLevelDwellers(_spawnAwait: 2, _dwellerModel: cat, _spawnPoint: 2, _parentQueue: "third_queue", _patienceCapacity: 13, _isServed: false, _isStuck: false),
            new SecondLevelDwellers(_spawnAwait: 2, _dwellerModel: tom, _spawnPoint: 0, _parentQueue: "first_queue", _patienceCapacity: 13, _isServed: false, _isStuck: false),
            new SecondLevelDwellers(_spawnAwait: 2, _dwellerModel: frog, _spawnPoint: 3, _parentQueue: "fourth_queue", _patienceCapacity: 13, _isServed: false, _isStuck: false),

            new SecondLevelDwellers(_spawnAwait: 1, _dwellerModel: fox, _spawnPoint: 1, _parentQueue: "second_queue", _patienceCapacity: 13, _isServed: false, _isStuck: false),
            new SecondLevelDwellers(_spawnAwait: 1, _dwellerModel: cat, _spawnPoint: 2, _parentQueue: "third_queue", _patienceCapacity: 13, _isServed: false, _isStuck: true),
            new SecondLevelDwellers(_spawnAwait: 1, _dwellerModel: frog, _spawnPoint: 0, _parentQueue: "first_queue", _patienceCapacity: 13, _isServed: false, _isStuck: true),
            new SecondLevelDwellers(_spawnAwait: 1, _dwellerModel: racoon, _spawnPoint: 3, _parentQueue: "fourth_queue", _patienceCapacity: 13, _isServed: false, _isStuck: true)
        };


        DwellerSpawner();

        // Defining crates to be spawned on the level
        cratesListSpawnSpots = new List<List<float>>
        {
            new List<float> { 155.58f, 18.22f, 15.47f },
            new List<float> { 153.52f, 18.22f, 16.26f },
            new List<float> { 151.48f, 18.22f, 17.05f },
            new List<float> { 149.43f, 18.22f, 17.86f },
            new List<float> { 147.37f, 18.22f, 18.68f },
            new List<float> { 145.22f, 18.22f, 19.55f },
            new List<float> { 143.05f, 18.22f, 20.38f },
            new List<float> { 140.85f, 18.22f, 21.25f },
            new List<float> { 155.58f, 21.22f, 15.47f },
            new List<float> { 153.52f, 21.22f, 16.26f },
            new List<float> { 151.48f, 21.22f, 17.05f },
            new List<float> { 149.43f, 21.22f, 17.86f },
            new List<float> { 147.37f, 21.22f, 18.68f },
            new List<float> { 145.22f, 21.22f, 19.55f },
            new List<float> { 143.05f, 21.22f, 20.38f },
            new List<float> { 140.85f, 21.22f, 21.25f },
            new List<float> { 154.87f, 21.22f, 13.74f },
            new List<float> { 152.81f, 21.22f, 14.53f },
            new List<float> { 150.77f, 21.22f, 15.32f },
            new List<float> { 148.72f, 21.22f, 16.13f },
            new List<float> { 146.66f, 21.22f, 16.95f },
            new List<float> { 144.51f, 21.22f, 17.82f },
            new List<float> { 142.34f, 21.22f, 18.65f },
            new List<float> { 140.14f, 21.22f, 19.52f }
        };

        CratesSpawner();

    }

    private async Task Wait(float time)
    {
        float startTime = Time.time;
        float currentTime = startTime;
        while (currentTime - startTime < time)
        {
            if (StatesManager.gameStates[StatesManager.currentGameState].isLevel)
            {
                currentTime += Time.deltaTime;
                await Task.Yield();
            }
            else
            {
                break;
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
                Quaternion spawnAngle = Quaternion.Euler(0f, -68.756f, 0f);
                Vector3 spawnPos = new Vector3(cratesListSpawnSpots[i][0], cratesListSpawnSpots[i][1], cratesListSpawnSpots[i][2]);
                Instantiate(crateModel, spawnPos, spawnAngle);
            }
            else
            {
                break;
            }
        }


    }

    void Update()
    {
        // Updating score
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
