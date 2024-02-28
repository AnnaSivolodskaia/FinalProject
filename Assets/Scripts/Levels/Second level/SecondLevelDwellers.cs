using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondLevelDwellers : MonoBehaviour
{
    public GameObject dwellerModel;
    public int spawnPoint;
    public string parentQueue;
    public float patienceCapacity;
    public bool isServed;
    public bool isStuck;
    public int placeInQueue;


    public SecondLevelDwellers(GameObject _dwellerModel, int _spawnPoint, string _parentQueue, float _patienceCapacity, bool _isServed, bool _isStuck)
    {
        dwellerModel = _dwellerModel;
        spawnPoint = _spawnPoint;
        parentQueue = _parentQueue;
        patienceCapacity = _patienceCapacity;
        isStuck = _isStuck;
        isServed = _isServed;
    }
}
