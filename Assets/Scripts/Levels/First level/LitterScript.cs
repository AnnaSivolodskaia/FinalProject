using System.Collections;
using System.Collections.Generic;
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

    public void Awake()
    {
        _GameManagerScript = FindObjectOfType<GameManagerScript>(); // !!! Change to FindByTag()
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

    public async void CountDown()
    {
        await Task.Delay(5000);
        try
        {
            Destroy(gameObject);
            FindObjectOfType<FirstLevelScript>().LevelFailed();
        }
        catch (Exception)  // delete whole catch when game is ready
        {
            Debug.Log("Object has been already destroyed!");
        }
    }
}
