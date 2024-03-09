using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dwellers
{
    public float time;
    public float locX;
    public float locY;
    public float locZ;
    public GameObject dwellerModel;
    public float dwellerSpeed;
    public int travelRoute;
    public int currentTravelSpot;

    public Dwellers(float _time, int _travelRoute, int _currentTravelSpot, float _locX, float _locY, float _locZ, GameObject _dwellerModel, float _dwellerSpeed)
    {
        time = _time;
        locX = _locX;
        locY = _locY;
        locZ = _locZ;
        dwellerModel = _dwellerModel;
        dwellerSpeed = _dwellerSpeed;
        travelRoute = _travelRoute;
        currentTravelSpot = _currentTravelSpot;
    }
}
