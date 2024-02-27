using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DwellersQueue : MonoBehaviour
{
    // class atributes
    // coordinates
    public Dictionary<string, List<List<float>>> possibleIncomingRoutes;
    public List<List<float>> queueIncomingRoute;
    public Dictionary<string, List<List<float>>> possibleExitingRoutes;
    public List<List<float>> queueExitingRoute;
    public Dictionary<string, List<List<float>>> possibleQueuePlaces;
    public List<List<float>> queuePlaces;

    // new attribute mimicing data type "queue".
    // [dweller1, dweller2]
        // queue place = index
    
    // On construct
        // Get route flag from SecondLevelScript and assign queue values for each route


    public void AddDweller()
    {
        // add new dweller into queue (data type object, [])
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
