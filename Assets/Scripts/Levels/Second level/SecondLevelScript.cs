using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

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

    public List<List<float>> possibleSpawnSpots;
    public List<List<float>> cratesListSpawnSpots;

    public GameObject crateModel;



    private void OnEnable()
    {
        // 0. Set level "level" variables 

        // 1. Instantiate all 4 queues 

        // 2. Respawn all fish boxes

        // 3. Start Dwellers spawner

        // Defining dwellers to be spawned on the level
        possibleSpawnSpots = new List<List<float>> { new List<float> { 140f, -21f }  };
        dwellerList.Add(new SecondLevelDwellers(_spawnAwait: 5, _dwellerModel: fox, _spawnPoint: 0, _parentQueue: "first_queue", _patienceCapacity: 100, _isServed: false, _isStuck: false));
        dwellerList.Add(new SecondLevelDwellers(_spawnAwait: 5, _dwellerModel: racoon, _spawnPoint: 0, _parentQueue: "first_queue", _patienceCapacity: 100, _isServed: false, _isStuck: false));
        dwellerList.Add(new SecondLevelDwellers(_spawnAwait: 5, _dwellerModel: frog, _spawnPoint: 0, _parentQueue: "first_queue", _patienceCapacity: 100, _isServed: false, _isStuck: false));
        dwellerList.Add(new SecondLevelDwellers(_spawnAwait: 5, _dwellerModel: tom, _spawnPoint: 0, _parentQueue: "second_queue", _patienceCapacity: 100, _isServed: false, _isStuck: false));
        dwellerList.Add(new SecondLevelDwellers(_spawnAwait: 5, _dwellerModel: retsuko, _spawnPoint: 0, _parentQueue: "second_queue", _patienceCapacity: 100, _isServed: false, _isStuck: false));
        dwellerList.Add(new SecondLevelDwellers(_spawnAwait: 5, _dwellerModel: racoon, _spawnPoint: 0, _parentQueue: "second_queue", _patienceCapacity: 100, _isServed: false, _isStuck: false));


        DwellerSpawner();

        // Defining crates to be spawned on the level
        cratesListSpawnSpots = new List<List<float>>();
        cratesListSpawnSpots.Add(new List<float> { 154f, 16f });
        cratesListSpawnSpots.Add(new List<float> { 150f, 16f });
        cratesListSpawnSpots.Add(new List<float> { 145f, 20f });

        CratesSpawner();






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
    
    
    public async void DwellerSpawner()
    {
        for (var i = 0; i < dwellerList.Count; i++)
        {
            if (dwellerList != null)
            {
                await Wait(dwellerList[i].spawnAwait);
                Quaternion spawnAngle = Quaternion.Euler(0f, 0f, 0f);
                Vector3 spawnPos = new Vector3(possibleSpawnSpots[dwellerList[i].spawnPoint][0], 20.2f, possibleSpawnSpots[dwellerList[i].spawnPoint][1]);
                GameObject spawnedDweller = Instantiate(dwellerList[i].dwellerModel, spawnPos, spawnAngle);
                int spawnedDwellerPlaceInQueue = GameObject.Find(dwellerList[i].parentQueue).GetComponent<DwellersQueue>().AddDweller(spawnedDweller);
                Debug.Log("Spawned, place in queue: " + spawnedDwellerPlaceInQueue);
                spawnedDweller.GetComponent<SecondLevelDwellerScript>().setParameters(_parentQueue: dwellerList[i].parentQueue, _patienceCapacity: dwellerList[i].patienceCapacity, _isStuck: dwellerList[i].isStuck, _placeInQueue: spawnedDwellerPlaceInQueue);
            }
            else
            {
                break;
            }
        }


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

    }
}
