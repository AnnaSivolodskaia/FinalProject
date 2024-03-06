using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using System;
using static FirstLevelScript;

public class LitterScript : MonoBehaviour
{
    public GameObject protagonist;
    public Transform protagonistLoc;
    //public Animator animator;

    private float detectionRadius = 2f;
    public GameObject plasticBin;
    public GameObject metalBin;
    public GameObject bioBin;

    public void Awake()
    {
        CountDown();
    }

    public void Update()
    {
        GameObject protagonist = GameObject.Find("Protagonist");
        GameObject plasticBin = GameObject.Find("PlasticTrashBin");
        GameObject metalBin = GameObject.Find("MetalTrashBin");
        GameObject bioBin = GameObject.Find("BioTrashBin");

        //animator = protagonist.GetComponent<Animator>();


        if (protagonist != null)
        {
            protagonistLoc = protagonist.transform;
            if (IsInRadius())
            {
                if(gameObject.tag == "BananaLitter")
                {
                    if (Input.GetKeyDown(KeyCode.Z))
                    {
                        //animator.SetBool("PickingUp", true);

                        FirstLevelScript.score += 10;
                        FirstLevelScript.bananasCollected += 1;
                        Shake(bioBin);
                        Destroy(gameObject);    
                    } else if (Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.C))
                    {
                        //animator.SetBool("PickingUp", true);

                        FirstLevelScript.score += 5;
                        FirstLevelScript.mistakesMade += 1;
                        Shake(bioBin);
                        Destroy(gameObject);
                    }
                }
                if (gameObject.tag == "BottleLitter")
                {
                    if (Input.GetKeyDown(KeyCode.X))
                    {
                        //animator.SetBool("PickingUp", true);

                        FirstLevelScript.score += 10;
                        FirstLevelScript.bottlesCollected += 1;
                        //shakes blue bin correctly
                        Shake(plasticBin);
                        Destroy(gameObject);
                    }
                    else if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.C))
                    {
                        //animator.SetBool("PickingUp", true);

                        FirstLevelScript.score += 5;
                        FirstLevelScript.mistakesMade += 1;
                        Shake(plasticBin);
                        Destroy(gameObject);
                    }
                }
                if (gameObject.tag == "CanLitter")
                {
                    if (Input.GetKeyDown(KeyCode.C))
                    {
                        //animator.SetBool("PickingUp", true);
                        FirstLevelScript.score += 10;
                        FirstLevelScript.cansCollected += 1;
                        Shake(metalBin);
                        Destroy(gameObject);
                    }
                    else if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.X))
                    {
                        //animator.SetBool("PickingUp", true);
                        FirstLevelScript.score += 5;
                        FirstLevelScript.mistakesMade = Math.Min(3, FirstLevelScript.mistakesMade + 1);
                        Shake(metalBin);
                        Destroy(gameObject);
                    }
                }

                if (FirstLevelScript.mistakesMade > 4)
                {
                    FindObjectOfType<FirstLevelScript>().LevelFailed();
                }

            }


        }

        if (!StatesManager.gameStates[StatesManager.currentGameState].isLevel)
        {
            Destroy(gameObject);
        }

    }

    public bool IsInRadius()
    {
        float distance = Vector3.Distance(transform.position, protagonistLoc.position);
        return distance <= detectionRadius;
    }

    public void Shake(GameObject bin)
    {
       bin.GetComponent<Animator>().SetTrigger("enabled");
    }

    public async void CountDown()
    {
        try
        {
            await Wait(5);
            Destroy(gameObject);
            FirstLevelScript.lostLitter += 1;
            if (FirstLevelScript.lostLitter > 2)
            {
                FindObjectOfType<FirstLevelScript>().LevelFailed();
            }
        }
        catch (Exception)
        {
            
        }
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
}
