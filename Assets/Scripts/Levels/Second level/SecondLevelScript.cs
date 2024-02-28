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



    private void OnEnable()
    {
        // 0. Set level "level" variables 

        // 1. Instantiate all 4 queues 

        // 2. Respawn all fish boxes

        // 3. Start Dwellers spawner

        // Defining dwellers to be spawned on the level
        possibleSpawnSpots = new List<List<float>> { new List<float> { 140f, -21f }  };
        dwellerList.Add(new SecondLevelDwellers(_dwellerModel: tom, _spawnPoint: 0, _parentQueue: "first_queue", _patienceCapacity: 100, _isServed: false, _isStuck: false));


        DwellerSpawner();






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
        await Wait(5);
        Quaternion spawnAngle = Quaternion.Euler(0f, 0f, 0f);
        Vector3 spawnPos = new Vector3(possibleSpawnSpots[dwellerList[0].spawnPoint][0], 25f, possibleSpawnSpots[dwellerList[0].spawnPoint][1]);
        Instantiate(dwellerList[0].dwellerModel, spawnPos, spawnAngle).GetComponent<SecondLevelDwellerScript>().setParameters(_parentQueue: dwellerList[0].parentQueue, _patienceCapacity: dwellerList[0].patienceCapacity, _isServed: dwellerList[0].isServed, _isStuck: dwellerList[0].isStuck, _placeInQueue: 1);
    }


    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
