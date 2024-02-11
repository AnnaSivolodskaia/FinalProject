using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DwellerScript : MonoBehaviour
{
    public float movementSpeed = 1f;
    // List of routes, containing list of travel spots, containing spot's coordinates
    public List<List<List<float>>> travelRoutes;
    // Selected travel route for this Dweller
    public List<List<float>> travelRoute;
    
    private int currentTravelSpot;
    private float currentPosX;
    private float currentPosZ;


    void OnEnable()
    {
        travelRoutes = new List<List<List<float>>>()
        {   
            // first route: far right to far left
            new List<List<float>>()
            {
                //pos.x, pos.z
                new List<float>(){119f, 57f},
                new List<float>(){121f, 61f},
                new List<float>(){130f, 72f},
                new List<float>(){139f, 79f},
                new List<float>(){144f, 86f},
                new List<float>(){146f, 91f}
            },
            
            // second route: far left to far right 
            new List<List<float>>()
            {
                //pos.x, pos.z
                new List<float>(){142f, 86f},
                new List<float>(){139f, 81f},
                new List<float>(){129f, 74f},
                new List<float>(){120f, 62f},
                new List<float>(){118f, 58f},
                new List<float>(){110f, 48f}

            },
            
            // third route: behind center to far right
            new List<List<float>>()
            {
                //pos.x, pos.z
                new List<float>(){119f, 57f},
                new List<float>(){121f, 61f},
                new List<float>(){126f, 68f},
                new List<float>(){133f, 63f},
                new List<float>(){140f, 58f},
                new List<float>(){146f, 54f}
            },

            // fourth route: far right to behind center
            new List<List<float>>()
            {
                //pos.x, pos.z
                new List<float>(){142f, 59f},
                new List<float>(){133f, 64f},
                new List<float>(){128f, 68f},
                new List<float>(){124f, 67f},
                new List<float>(){120f, 62f},
                new List<float>(){118f, 58f},
                new List<float>(){110f, 48f}
            },
        };


        currentPosX = gameObject.transform.position.x;
        currentPosZ = gameObject.transform.position.z;
    }

    public void setTravelRoute(int _travelRoute, int _currentTravelSpot)
    {
        travelRoute = travelRoutes[_travelRoute];
        currentTravelSpot = _currentTravelSpot;
    }

    // Update is called once per frame
    void Update()
    {
        if (!StatesManager.gameStates[StatesManager.currentGameState].isLevel)
        {
            Destroy(gameObject);
        }

        if(currentTravelSpot == travelRoute.Count)
        {
            Destroy(gameObject);
        } else
        {
            DwellerMovement(travelRoute[currentTravelSpot]);
        }
    }

    private void DwellerMovement(List<float> travelSpot)
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

        if(Mathf.Round(gameObject.transform.position.x) == locX && Mathf.Round(gameObject.transform.position.z) == locZ)
        {
           currentTravelSpot += 1;
        }
    }


}
