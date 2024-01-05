using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UserInputHandler : MonoBehaviour
{
    public static event Action Input_C;
    public static event Action Input_LeftRight;
    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.C))
        {
            Input_C?.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow) | Input.GetKeyDown(KeyCode.RightArrow))
        {
            Input_LeftRight?.Invoke();
        }
    }
}
