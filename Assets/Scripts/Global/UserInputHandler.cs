using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UserInputHandler : MonoBehaviour
{
    public static event Action Input_C;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.C))
        {
            Input_C?.Invoke();
        }

    }
}
