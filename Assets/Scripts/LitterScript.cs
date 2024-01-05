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
    
    public void Awake()
    {
        CountDown();
    }

    public void Update()
    {
        GameObject protagonist = GameObject.Find("Protagonist");
        protagonistLoc = protagonist.transform;
        if (IsInRadius() && Input.GetKeyDown(KeyCode.Z))
        {
            FindObjectOfType<FirstLevelScript>().score += 100;
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
            FindObjectOfType<FirstLevelScript>().Terminate();
        }
        catch (Exception)
        {
            Debug.Log("Object has been already destroyed!");
        }
    }
}
