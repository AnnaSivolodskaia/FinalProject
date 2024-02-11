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

    private void OnEnable()
    {
        gameObject.transform.position = new Vector3(120.81f, 22f, 65.89f);
    }

    private void Update()
    {
        float turn = Input.GetAxis("Horizontal");

        if (turn < 0)
        {
            //turn left
            transform.rotation = Quaternion.Euler(0, 35, 0);
        }
        else if (turn > 0)
        {
            //turn right
            transform.rotation = Quaternion.Euler(0, 215, 0);
        }

        if (Mathf.Abs(turn) > 0)
        {
            //trigger walking animation
            animator.SetBool("Moving", true);

            //movement
            transform.Translate(Vector3.forward * Mathf.Abs(turn) * movementSpeed * Time.deltaTime);
            Debug.Log("Horizontal input: " + turn);
        }
        else
        {
            //stop walking
            animator.SetBool("Moving", false);
        }
    }

}