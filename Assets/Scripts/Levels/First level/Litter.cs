using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Litter : MonoBehaviour
{
    public float time;
    public float locX;
    public float locY;
    public float locZ;
    public GameObject littertName;

    public Litter(float _time, float _locX, float _locY, float _locZ, GameObject _litterName)
    {
        time = _time;
        locX = _locX;
        locY = _locY;
        locZ = _locZ;
        littertName = _litterName;
    }
}
