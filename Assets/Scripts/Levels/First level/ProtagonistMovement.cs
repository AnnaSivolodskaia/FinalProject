using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ProtagonistMovement : MonoBehaviour
{
    public float movementSpeed;
    public float rotateSpeed;
    public Animator animator;
    public string currentLevel;
    public bool isCarryingBox = false;
    public bool isMovingCarrying;
    public GameObject crate;
    public GameObject closestCrate;

    public GameObject[] cratesOnLevel;


    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        currentLevel = StatesManager.currentGameState;
        if (currentLevel == "1lvl_1")
        {
            gameObject.transform.position = new Vector3(120.81f, 22f, 65.89f);
        } else if (currentLevel == "2lvl_1")
        {
            gameObject.transform.position = new Vector3(144f, 21.499f, 8f);
        }
    }

    private void Update()
    {
        if (currentLevel == "1lvl_1")
        {
            firstLevelMovement();
        }
        else if (currentLevel == "2lvl_1")
        {
            secondLevelMovement();
        }

        // create drop/pick up funciton here first

        // if any crate is in radius
            // crate pick up funcion
            // crate destroy function
    }

    private void firstLevelMovement()
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
            /* Vector3 newPosition = transform.position + transform.forward * Mathf.Abs(turn) * movementSpeed * Time.deltaTime;
             if (IsPositionWithinBounds(newPosition))*/
            //{
            // Move within bounds
            transform.Translate(Vector3.forward * Mathf.Abs(turn) * movementSpeed * Time.deltaTime);
            //}
            //movement
            //transform.Translate(Vector3.forward * Mathf.Abs(turn) * movementSpeed * Time.deltaTime);
        }
        else
        {
            //stop walking
            animator.SetBool("Moving", false);     
        }
    }

    private void secondLevelMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontalInput, 0, verticalInput).normalized;
        if (Input.GetKeyDown(KeyCode.C) && !isCarryingBox)
        {
            cratesOnLevel = GameObject.FindGameObjectsWithTag("secondLevelCrate");

            if (cratesOnLevel != null)
            {
                closestCrate = null;
                float closestDistance = float.MaxValue;

                foreach (GameObject availableCrate in cratesOnLevel)
                {
                    if (availableCrate != null)
                    {
                        float distanceToCrate = Vector3.Distance(transform.position, availableCrate.transform.position);

                        if (distanceToCrate < closestDistance && distanceToCrate <= 2f)
                        {
                            closestCrate = availableCrate;
                            closestDistance = distanceToCrate;
                        }
                    }
                }

                if (closestCrate != null)
                {
                    handleCrate();
                    Destroy(closestCrate);
                }
            }
        }

        if (movement != Vector3.zero)
        {
            // Custom rotation factor (scene is not aligned with x/z coordintaes 
            float rotationFactor = 200f;

            // Rotate towards the input direction with the custom factor
            Quaternion rotation = Quaternion.LookRotation(movement) * Quaternion.Euler(0, rotationFactor, 0);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 10);




            // Trigger walking animation
            animator.SetBool("Moving", true);
            transform.Translate(Vector3.forward * movement.magnitude * movementSpeed * Time.deltaTime);

            
            //animator.SetBool("IsCarryingBox", true);
            //animator.SetBool("isMovingCarrying", true);

            // Move in the specified direction
        }
        else
        {
            // Stop walking
            animator.SetBool("Moving", false);


            //animator.SetBool("IsCarryingBox", false);
            //animator.SetBool("isMovingCarrying", false);


            //if(is carying box)
            //{one animation} else {
            //another animation}
            //same for idle pose 
        }

    }

    public void handleCrate()
    {
        animator.SetBool("IsCarryingBox", !animator.GetBool("IsCarryingBox"));
        crate.SetActive(!crate.activeSelf);
        isCarryingBox = !isCarryingBox;
    }
}