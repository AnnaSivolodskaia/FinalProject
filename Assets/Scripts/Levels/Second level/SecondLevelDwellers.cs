using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondLevelDwellers : MonoBehaviour
{
    public GameObject dwellerModel;
    public float spawnPoint;
    public int parentQueue;
    public float patienceCapacity;
    public bool isServed;
    public bool isStuck;
    public int placeInQueue;


    public SecondLevelDwellers(GameObject _dwellerModel, float _spawnPoint, int _parentQueue, float _patienceCapacity, bool _isServed, bool _isStuck, int _placeInQueue)
    {
        dwellerModel = _dwellerModel;
        spawnPoint = _spawnPoint;
        parentQueue = _parentQueue;
        patienceCapacity = _patienceCapacity;
        isStuck = _isStuck;
        placeInQueue = _placeInQueue;
        isServed = _isServed;
    }
}
