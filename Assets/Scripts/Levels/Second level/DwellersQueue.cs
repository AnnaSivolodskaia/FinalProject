using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DwellersQueue : MonoBehaviour
{
    // class atributes
    // coordinates
    public Dictionary<string, List<List<float>>> possibleIncomingRoutes = new Dictionary<string, List<List<float>>>();
    public List<List<float>> queueIncomingRoute;
    public Dictionary<string, List<List<float>>> possibleExitingRoutes = new Dictionary<string, List<List<float>>>();
    public List<List<float>> queueExitingRoute;
    public Dictionary<string, List<List<float>>> possibleQueuePlaces = new Dictionary<string, List<List<float>>>();
    public List<List<float>> queuePlaces;

    public List<GameObject> dwellersInQueue;

    private void OnEnable()
    {
        possibleIncomingRoutes.Add("first_queue", new List<List<float>> { new List<float>{ 141f, -22f }, new List<float> { 142f, -19f }, new List<float> { 143f, -16f }});
        // possibleIncomingRoutes.Add("second_queue",
        // possibleIncomingRoutes.Add("third_queue",
        // possibleIncomingRoutes.Add("fourth_queue",

        possibleQueuePlaces.Add("first_queue", new List<List<float>> { new List<float> { 150f, 2f }, new List<float> { 149, -2f }, new List<float> { 147f, -6f }, new List<float> { 146f, -9f }, new List<float> { 145f, -13f } });
        // possibleQueuePlaces.Add("second_queue",
        // possibleQueuePlaces.Add("third_queue",
        // possibleQueuePlaces.Add("fourth_queue",

        // possibleExitingRoutes.Add("first_queue",   SHOULD BE 9 SPOTS!
        // possibleExitingRoutes.Add("second_queue",
        // possibleExitingRoutes.Add("third_queue",
        // possibleExitingRoutes.Add("fourth_queue",

        queueIncomingRoute = possibleIncomingRoutes[gameObject.name];

        queuePlaces = possibleQueuePlaces[gameObject.name];
    }

    public int AddDweller(GameObject incomingDweller)
    {
        dwellersInQueue.Add(incomingDweller);

        return dwellersInQueue.IndexOf(incomingDweller);
    }

    public void RemoveDweller()
    {
        dwellersInQueue.RemoveAt(0);
        for (var i = 0; i < dwellersInQueue.Count; i++)
        {
            dwellersInQueue[i].GetComponent<SecondLevelDwellerScript>().setPlaceInQueue(i);
        }
    }

    public void ServeDweller()
    {
        dwellersInQueue[0].GetComponent<SecondLevelDwellerScript>().servingDweller();

        if (!dwellersInQueue[0].GetComponent<SecondLevelDwellerScript>().isStuck)
        {
            RemoveDweller();
        }   
    }

    public void AskToLeave()
    {
        dwellersInQueue[0].GetComponent<SecondLevelDwellerScript>().unstuckDweller();
        RemoveDweller();
    }


}
