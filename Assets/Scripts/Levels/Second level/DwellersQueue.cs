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

    // custom queue data type

    private void OnEnable()
    {
        possibleIncomingRoutes.Add("first_queue", new List<List<float>> { new List<float>{ 138f, -23f }, new List<float> { 140f, -20f }, new List<float> { 141f, -17f }});
        
        possibleQueuePlaces.Add("first_queue", new List<List<float>> { new List<float> { 147f, -2f }, new List<float> { 146, -5f }, new List<float> { 145f, -8f }, new List<float> { 144f, -10f }, new List<float> { 142f, -13f } });

        queueIncomingRoute = possibleIncomingRoutes[gameObject.name];

        queuePlaces = possibleQueuePlaces[gameObject.name];
    }

    // new attribute mimicing data type "queue".
    // [dweller1, dweller2]
    // queue place = index

    // On construct
    // Get route flag from SecondLevelScript and assign queue values for each route


    public void AddDweller()
    {
        // add new dweller into queue (data type object, [])

        // if queue is empty - new dweller is on firsst place
        // if there is one dweller already - new dweller is second
        // once first dweller is served - he pops out from HEAD and second dweller becomes first
    }

    public void RemoveDweller()
    {
        // remove exiting dweller from the queue
    }

    public void ServeDweller()
    {
        // find [0] dweller (currently waiting)
        // tell him to change state to "isServed"
    }

    public void AskToLeave()
    {
        // find [0] dweller
        // tell him to change state to "isStuck"
    }


}
