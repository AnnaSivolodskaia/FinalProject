using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatienceMetetScript : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(new Vector3(0f, 200f, 0f)), 1f);
    }

    // appears on dweller on the first place in queue

    // count-down function triggers right after activating

    // changing (dis-/enable) emoji and slider fill color basedf on the state

    // dissappearing function after 5 secs
}
