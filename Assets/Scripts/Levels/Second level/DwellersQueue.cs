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

    public List<GameObject> dwellersInQueue;

    private void OnEnable()
    {
        possibleIncomingRoutes = new Dictionary<string, List<List<float>>>
        {
            { "first_queue", new List<List<float>> { new List<float> { 167f, 1f }, new List<float> { 162f, -12f }, new List<float> { 149f, -9f } } },
            { "second_queue", new List<List<float>> { new List<float> { 132f, -36f }, new List<float> { 134f, -25f }, new List<float> { 138f, -16f } } },
            { "third_queue", new List<List<float>> { new List<float> { 107f, -7f }, new List<float> { 123f, -10f }, new List<float> { 128f, -12f } } },
            { "fourth_queue", new List<List<float>> { new List<float> { 107f, -7f }, new List<float> { 123f, -10f }, new List<float> { 128f, -12f } } }
        };

        possibleQueuePlaces = new Dictionary<string, List<List<float>>>
        {
            //{ "first_queue", new List<List<float>> { new List<float> { 149f, 1f }, new List<float> { 148f, -2f }, new List<float> { 147f, -6f }, new List<float> { 145f, -10f }, new List<float> { 144f, -13f } } },
            { "first_queue", new List<List<float>> { new List<float> { 150f, 1f }, new List<float> { 149f, -4f }, new List<float> { 147f, -8f }, new List<float> { 146f, -12f }, new List<float> { 145f, -15f } } },
            //{ "second_queue", new List<List<float>> { new List<float> { 145.19f, 3.5f }, new List<float> { 144, 2f }, new List<float> { 143f, -1f }, new List<float> { 142f, -4f }, new List<float> { 141f, -7f } } },
            { "second_queue", new List<List<float>> { new List<float> { 147f, 2f }, new List<float> { 146, -3f }, new List<float> { 144f, -7f }, new List<float> { 143f, -11f }, new List<float> { 142f, -14f } } },
            
            //{ "third_queue", new List<List<float>> { new List<float> { 140f, 5f }, new List<float> { 139, 2f }, new List<float> { 138f, -2f }, new List<float> { 137f, -5f }, new List<float> { 136f, -8f } } },
            { "third_queue", new List<List<float>> { new List<float> { 139f, 5f }, new List<float> { 137, 1f }, new List<float> { 136f, -3f }, new List<float> { 134f, -6f }, new List<float> { 133f, -10f } } },

            { "fourth_queue", new List<List<float>> { new List<float> { 136f, 7f }, new List<float> { 134, 2f }, new List<float> { 132f, -2f }, new List<float> { 131f, -5f }, new List<float> { 130f, -9f } } }
        };

        possibleExitingRoutes = new Dictionary<string, List<List<float>>>
        {
            { "first_queue", new List<List<float>> { new List<float> { 153f, 0f }, new List<float> { 160f, 1f }, new List<float> { 165f, 5f }, new List<float> { 163f, 9f }, new List<float> { 161f, 13f }, new List<float> { 159f, 18f }, new List<float> { 155f, 22f } } },
            //{ "second_queue", new List<List<float>> { new List<float> { 143f, 4f }, new List<float> { 141f, -2f }, new List<float> { 138f, -10f }, new List<float> { 135f, -17f }, new List<float> { 121f, -31f }, new List<float> { 112f, -27f }, new List<float> { 105f, -26f } } },
            
            { "second_queue", new List<List<float>> { new List<float> { 144f, 3f }, new List<float> { 143f, -1f }, new List<float> { 139f, -10f }, new List<float> { 135f, -17f }, new List<float> { 121f, -31f }, new List<float> { 112f, -27f }, new List<float> { 105f, -26f } } },

            //{ "third_queue", new List<List<float>> { new List<float> { 143f, 4f }, new List<float> { 141f, -2f }, new List<float> { 138f, -10f }, new List<float> { 135f, -17f }, new List<float> { 121f, -31f }, new List<float> { 112f, -27f }, new List<float> { 105f, -26f } } },
            { "third_queue", new List<List<float>> { new List<float> { 141f, 3f }, new List<float> { 139f, -2f }, new List<float> { 136f, -10f }, new List<float> { 131f, -17f }, new List<float> { 121f, -31f }, new List<float> { 112f, -27f }, new List<float> { 105f, -26f } } },

            { "fourth_queue", new List<List<float>> { new List<float> { 132f, 8f }, new List<float> { 132f, 13f }, new List<float> { 132f, 18f }, new List<float> { 132f, 22f }, new List<float> { 132f, 26f }, new List<float> { 132f, 29f } } }
        };

        queueIncomingRoute = possibleIncomingRoutes[gameObject.name];

        queuePlaces = possibleQueuePlaces[gameObject.name];

        queueExitingRoute = possibleExitingRoutes[gameObject.name];
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
        GetComponentInParent<SecondLevelScript>().score += 10;

        if (!dwellersInQueue[0].GetComponent<SecondLevelDwellerScript>().isStuck)
        {
            RemoveDweller();
        }   
    }

    public void AskToLeave()
    {
        Debug.Log("Queue length: " + dwellersInQueue.Count);
        dwellersInQueue[0].GetComponent<SecondLevelDwellerScript>().unstuckDweller();
        RemoveDweller();
    }

    public void DwellerGaveUp()
    {
        RemoveDweller();
        GetComponentInParent<SecondLevelScript>().lostDwellers += 1;
    }


}
