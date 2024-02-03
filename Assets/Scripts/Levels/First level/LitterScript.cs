using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using System;

public class LitterScript : MonoBehaviour
{
    public GameObject protagonist;
    public Transform protagonistLoc;
    private float detectionRadius = 2f;
    public GameManagerScript _GameManagerScript;
    private CancellationTokenSource cancellationTokenSource;


    public void Awake()
    {
        _GameManagerScript = FindObjectOfType<GameManagerScript>(); // !!! Change to FindByTag()
        cancellationTokenSource = new CancellationTokenSource();
        CountDown();

    }

    public void Update()
    {
        GameObject protagonist = GameObject.Find("Protagonist");
        
        if (protagonist != null)
        {
            protagonistLoc = protagonist.transform;
            if (IsInRadius() && Input.GetKeyDown(KeyCode.Z))
            {
                FirstLevelScript.score += 100;
                Destroy(gameObject);
            }
        }

        if (_GameManagerScript.currentGameState == "1level_3")
        {
            Destroy(gameObject);
        }

    }

    public bool IsInRadius()
    {
        float distance = Vector3.Distance(transform.position, protagonistLoc.position);
        return distance <= detectionRadius;
    }

/*    public async void CountDown()
    {
        try
        {
            await Task.Delay(5000, cancellationTokenSource.Token);
            Destroy(gameObject);
            FindObjectOfType<FirstLevelScript>().LevelFailed();
        }
        catch (Exception)  // delete whole catch when game is ready
        {
            //Debug.Log("Object has been already destroyed!");
        }
    }*/


    public async void CountDown()
    {
        try
        {
            await Wait(5);
            Destroy(gameObject);
            FindObjectOfType<FirstLevelScript>().LevelFailed();
        }
        catch (Exception)  // delete whole catch when game is ready
        {
            //Debug.Log("Object has been already destroyed!");
        }
    }


    private async Task Wait(float time)
    {
        float startTime = Time.time;
        float currentTime = startTime;

        if (_GameManagerScript.currentGameState != "1level_3")
        {
            while (currentTime - startTime < time)
            {
                currentTime += Time.deltaTime;
                await Task.Yield();
            }
        }


    }

    private void OnDestroy()
    {
        // Cancel the CountDown when the litter is already picked up
        cancellationTokenSource.Cancel();
    }
}
