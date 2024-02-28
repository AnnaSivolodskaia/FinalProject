using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondLevelDwellerScript : MonoBehaviour
{

    // request for routes and places coordinates
    // Dweller movement
    // call-backs to queue script to initiate events (add/remove dweller)
    // Patience count-down

    public float movementSpeed = 1f;

    public string parentQueue;
    public float patienceCapacity;
    public bool isServed;
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


    public void setParameters(string _parentQueue, float _patienceCapacity, bool _isServed, bool _isStuck, int _placeInQueue)
    {
        parentQueue = _parentQueue;
        patienceCapacity = _patienceCapacity;
        isStuck = _isStuck;
        placeInQueue = _placeInQueue;
        isServed = _isServed;

        incomingRouteFinished = false;
        isExiting = false;

        SetRoutes(parentQueue);
    }

    public void SetRoutes(string parentQueue)
    {
        Debug.Log("SetRoutes parentQueue = " + parentQueue);
        DwellersQueue DwellerQueue = GameObject.Find(parentQueue).GetComponent<DwellersQueue>();

        incomingTravelRoute = DwellerQueue.queueIncomingRoute;
        queuePlacesSpots = DwellerQueue.queuePlaces;
        queueExitingRoute = DwellerQueue.queueExitingRoute;

        currentTravelSpotIndex = 0;
        Debug.Log("first queue incoming route len: " + incomingTravelRoute.Count);
        Debug.Log("first spot incoming route len: " + incomingTravelRoute[currentTravelSpotIndex].Count);
        currentTravelSpotCoordinates = incomingTravelRoute[currentTravelSpotIndex];

        isMoving = true;
    }

    private void Update()
    {
        if (isMoving)
        {
            DwellerMovement(currentTravelSpotCoordinates);
        } else
        {
            // trigger "idle" animation 
        }
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
        else if (!isServed)
        {
            if(placeInQueue < currentTravelSpotIndex)
            {
                currentTravelSpotIndex -= 1;
                currentTravelSpotCoordinates = queuePlacesSpots[currentTravelSpotIndex];
            }
            else if (placeInQueue == currentTravelSpotIndex)
            {
                isMoving = false;
            }
        }
        else if (isExiting)
        {
            if (currentTravelSpotIndex < 8)
            {
                currentTravelSpotIndex += 1;
                currentTravelSpotCoordinates = queueExitingRoute[currentTravelSpotIndex];
            }
            else
            {
                Destroy(gameObject);
            }
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

        // Calculate the direction vector from current position to target position
        Vector3 direction = new Vector3(locX - transform.position.x, 0f, locZ - transform.position.z).normalized;

        // Calculate the rotation angle based on the direction vector
        float angleY = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;

        // Set the target rotation
        Quaternion targetAngles = Quaternion.Euler(new Vector3(0f, angleY, 0f));

        // Smoothly rotate towards the target rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, targetAngles, 1f);

        // Move towards the target position
        transform.Translate(direction * movementSpeed * Time.deltaTime, Space.World);

        if (Mathf.Round(gameObject.transform.position.x) == locX && Mathf.Round(gameObject.transform.position.z) == locZ)
        {
            Debug.Log("Travel point reached, NEXT ACTION!");
            defineNextAction();
        }
    }
}
