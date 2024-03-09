using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using System.Threading.Tasks;

public class SecondLevelDwellerScript : MonoBehaviour
{
    public GameObject patienceMeter;

    public float movementSpeed = 1f;

    public string parentQueue;
    public float patienceCapacity;
    public bool isServed = false;
    public bool isStuck;
    public int placeInQueue;

    public List<List<float>> incomingTravelRoute;
    public List<List<float>> queuePlacesSpots;
    public List<List<float>> queueExitingRoute;

    public int currentTravelSpotIndex;
    public List<float> currentTravelSpotCoordinates;

    public bool isMoving = false;
    public bool incomingRouteFinished;
    public bool isExiting;

    public GameObject protagonist;
    DwellersQueue DwellerQueue;


    public void setParameters(string _parentQueue, float _patienceCapacity, bool _isStuck, int _placeInQueue)
    {
        parentQueue = _parentQueue;
        patienceCapacity = _patienceCapacity;
        isStuck = _isStuck;
        placeInQueue = _placeInQueue;

        incomingRouteFinished = false;
        isExiting = false;

        protagonist = GameObject.Find("SecondLevelProtagonist");

        patienceMeter = transform.GetChild(0).gameObject;

        SetRoutes(parentQueue);
    }

    public void servingDweller()
    {
        isServed = true;
        defineNextAction();
    }

    public void LeavingDweller()
    {
        isExiting = true;
        DwellerQueue.DwellerGaveUp();
        defineNextAction();
    }

    public void unstuckDweller()
    {
        isStuck = false;
        defineNextAction();
    }

    public void setPlaceInQueue(int index)
    {
        placeInQueue = index;
        defineNextAction();
    }

    public void SetRoutes(string parentQueue)
    {
        DwellerQueue = GameObject.Find(parentQueue).GetComponent<DwellersQueue>();

        incomingTravelRoute = DwellerQueue.queueIncomingRoute;
        queuePlacesSpots = DwellerQueue.queuePlaces;
        queueExitingRoute = DwellerQueue.queueExitingRoute;

        currentTravelSpotIndex = 0;
        currentTravelSpotCoordinates = incomingTravelRoute[currentTravelSpotIndex];

        isMoving = true;
    }

    private void Update()
    {
        // if level is already failed, destroying this instance
        if (!StatesManager.gameStates[StatesManager.currentGameState].isLevel)
        {
            DestroyDweller();
        }

        if (isMoving)
        {
            // trigger movement towards current spot
            DwellerMovement(currentTravelSpotCoordinates);
            GetComponent<Animator>().SetBool("IsMoving", true);
        } else
        {
            // trigger "idle" animation and rotate towards table
            GetComponent<Animator>().SetBool("IsMoving", false);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(new Vector3(0f, 20f, 0f)), 1f);
        }

        // mechanism for serving dweller
        if (IsInRadius() && protagonist.GetComponent<ProtagonistMovement>().isCarryingBox && !isServed && (Input.GetKeyDown(KeyCode.C) || Input.GetKeyDown(KeyCode.JoystickButton1)) )
        {
            DwellerQueue.GetComponent<DwellersQueue>().ServeDweller();
            protagonist.GetComponent<ProtagonistMovement>().HandleCrate();
            FindObjectOfType<AudioManager>().TriggerSound("CrateGive");
            FindObjectOfType<AudioManager>().TriggerSound("HappyDweller");


        }

        // mechanism for unstucking dweller
        if ( IsInRadius() && isStuck && isServed && (Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.JoystickButton0)) )
        {
            DwellerQueue.GetComponent<DwellersQueue>().AskToLeave();
            FindObjectOfType<AudioManager>().TriggerSound("OkayDweller");
        }
    }

    public bool IsInRadius()
    {
        Transform protagonistLoc = protagonist.transform;
        float distance = Vector3.Distance(transform.position, protagonistLoc.position);
        return distance <= 4.8f;
    }

    public void defineNextAction()
    {
        isMoving = true;

        if (!incomingRouteFinished && currentTravelSpotIndex < 2)
        {
            currentTravelSpotIndex += 1;
            currentTravelSpotCoordinates = incomingTravelRoute[currentTravelSpotIndex];
        } 
        else if (!incomingRouteFinished && currentTravelSpotIndex == 2)
        {
            incomingRouteFinished = true;
            currentTravelSpotIndex = 4;
            currentTravelSpotCoordinates = queuePlacesSpots[currentTravelSpotIndex];
        }
        else if (!isServed && !isExiting)
        {
            if(placeInQueue < currentTravelSpotIndex)
            {
                currentTravelSpotIndex -= 1;
                currentTravelSpotCoordinates = queuePlacesSpots[currentTravelSpotIndex];
            }
            else if (placeInQueue == currentTravelSpotIndex)
            {
                isMoving = false;
                if (placeInQueue == 0)
                {
                    patienceMeter.SetActive(true);
                    patienceMeter.GetComponent<PatienceMetetScript>().PatienceCountDown(patienceCapacity);
                }
            }
        }
        else if (isExiting)
        {
            if (currentTravelSpotIndex < queueExitingRoute.Count-1)
            {
                currentTravelSpotCoordinates = queueExitingRoute[currentTravelSpotIndex];
                currentTravelSpotIndex += 1;
            }
            else
            {
                Destroy(gameObject);
            }
        }
        else if (isServed && isStuck)
        {
            isMoving = false;
        }
        else if(isServed && !isStuck)
        {
            isExiting = true;
            currentTravelSpotIndex = 0;
            currentTravelSpotCoordinates = queueExitingRoute[currentTravelSpotIndex];
        }

    }

    public void DwellerMovement(List<float> travelSpot)
    {
        float locX = travelSpot[0];
        float locZ = travelSpot[1];

        // Calculate direction and rotation angle vectors
        Vector3 direction = new Vector3(locX - transform.position.x, 0f, locZ - transform.position.z).normalized;

        float angleY = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        Quaternion targetAngles = Quaternion.Euler(new Vector3(0f, angleY, 0f));

        // Move to the target pos
        transform.rotation = Quaternion.Slerp(transform.rotation, targetAngles, 1f);
        transform.Translate(direction * movementSpeed * Time.deltaTime, Space.World);

        // Switch to the next destinatioon spot
        if (Math.Round(gameObject.transform.position.x, 1) == Math.Round(locX, 1) && Math.Round(gameObject.transform.position.z, 1) == Math.Round(locZ, 1))
        {
            defineNextAction();
        }
    }

    public async void DestroyDweller()
    {
        await StandardWait(1.5f);
        try
        {
            Destroy(gameObject);
        }
        catch (Exception){ } 
    }

    private async Task StandardWait(float time)
    {
        float startTime = Time.time;
        float currentTime = startTime;
        while (currentTime - startTime < time)
        {
            currentTime += Time.deltaTime;
            await Task.Yield();
        }
    }
}
