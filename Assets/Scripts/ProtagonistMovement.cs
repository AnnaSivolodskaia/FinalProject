using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtagonistMovement : MonoBehaviour
{
    public float movementSpeed;
    public float rotateSpeed;
    public Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        float turn = Input.GetAxis("Horizontal");

        if (turn < 0)
        {
            //turn left
            transform.rotation = Quaternion.Euler(0, 45, 0);
        }
        else if (turn > 0)
        {
            //turn right
            transform.rotation = Quaternion.Euler(0, 225, 0);
        }

        if (Mathf.Abs(turn) > 0)
        {
            //trigger walking animation
            animator.SetBool("Moving", true);

            //movement
            transform.Translate(Vector3.forward * Mathf.Abs(turn) * movementSpeed * Time.deltaTime);
        }
        else
        {
            //stop walking
            animator.SetBool("Moving", false);
        }
    }

}
