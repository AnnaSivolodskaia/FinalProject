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

    //defining spots for protagonist traveling route to move in one dimension only
    public List<List<float>> firstLevelProtagonistRoute = new List<List<float>> { new List<float> { 131.41f, 80f }, new List<float> { 129.73f, 77.18f }, new List<float> { 127.56f, 74f }, new List<float> { 124f, 69.56f }, new List<float> { 119.47f, 64.54f }, new List<float> { 117.62f, 61.27f }, new List<float> { 114.63f, 57.36f }, new List<float> { 111f, 53f }, new List<float> { 108.73f, 50.42f } };
    public int firstLevelProtagonistCurrentRoutePoint;
    public int travelPoint;
    public float locX;
    public float locZ;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        currentLevel = StatesManager.currentGameState;
        if (currentLevel == "1lvl_1")
        {
            gameObject.transform.position = new Vector3(firstLevelProtagonistRoute[3][0], 22f, firstLevelProtagonistRoute[3][1]);
            firstLevelProtagonistCurrentRoutePoint = 4;
            travelPoint = 0;
            locX = 0f;
            locZ = 0f;
        } else if (currentLevel == "2lvl_1")
        {
            gameObject.transform.position = new Vector3(144f, 21.499f, 8f);
        }
    }

    private void Update()
    {
        if (currentLevel == "1lvl_1")
        {
            FirstLevelMovement();
        }
        else if (currentLevel == "2lvl_1")
        {
            SecondLevelMovement();
        }
    }

    private void FirstLevelMovement()
    {
        float turn = Input.GetAxis("Horizontal");
        float protagonistMovementSpeed;

        if (turn < 0)
        {
            // turn left
            travelPoint = Math.Max(0, firstLevelProtagonistCurrentRoutePoint-1);
            locX = firstLevelProtagonistRoute[travelPoint][0];
            locZ = firstLevelProtagonistRoute[travelPoint][1];
        }
        else if (turn > 0)
        {
            //turn right
            travelPoint = Math.Min(firstLevelProtagonistRoute.Count-1, firstLevelProtagonistCurrentRoutePoint + 1);
            locX = firstLevelProtagonistRoute[travelPoint][0];
            locZ = firstLevelProtagonistRoute[travelPoint][1];
        }

        // Calculate direction and rotation angle vectors
        Vector3 direction = new Vector3(locX - transform.position.x, 0f, locZ - transform.position.z).normalized;

        float angleY = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        Quaternion targetAngles = Quaternion.Euler(new Vector3(0f, angleY, 0f));
        
        // Rotate to the target pos
        transform.rotation = Quaternion.Slerp(transform.rotation, targetAngles, 1f);

        if (Mathf.Abs(turn) > 0)
        {
            //trigger walking animation
            animator.SetBool("Moving", true);
        }
        else
        {
            //stop walking
            animator.SetBool("Moving", false);
        }

        // Define movement speed (speed zero to implement "invisible wall" mechanism)
        if ( travelPoint == 0 || travelPoint == (firstLevelProtagonistRoute.Count - 1))
        {
            protagonistMovementSpeed = 0f;
        }
        else
        {
            protagonistMovementSpeed = Mathf.Abs(turn);
        }

        // Move to the target pos
        transform.Translate(direction * protagonistMovementSpeed * movementSpeed * Time.deltaTime, Space.World);
        // Defining next travel spot
        if (Math.Round(gameObject.transform.position.x, 1) == Math.Round(locX, 1) && Math.Round(gameObject.transform.position.z, 1) == Math.Round(locZ, 1))
        {
            if (turn < 0)
            {
                firstLevelProtagonistCurrentRoutePoint = Math.Max(0, firstLevelProtagonistCurrentRoutePoint - 1);
            }
            else if(turn > 0)
            {
                firstLevelProtagonistCurrentRoutePoint = Math.Min(firstLevelProtagonistRoute.Count - 1, firstLevelProtagonistCurrentRoutePoint + 1);
            }
        }
    }

    private void SecondLevelMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontalInput, 0, verticalInput).normalized;
        
        // if level failed when protagonist carried box, next try should have reset state 
        if (!StatesManager.gameStates[StatesManager.currentGameState].isLevel && isCarryingBox)
        {
            HandleCrate();
        }

        // picking up crate and destroying nearest crate game object
        if ( (Input.GetKeyDown(KeyCode.C) || Input.GetKeyDown(KeyCode.JoystickButton1)) && !isCarryingBox)
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
                    FindObjectOfType<AudioManager>().TriggerSound("CratePickUp");
                    HandleCrate();
                    Destroy(closestCrate);
                }
            }
        }

        if (movement != Vector3.zero)
        {
            // Custom angle, since scene is not aligned with x/z coordintaes 
            float rotationFactor = 200f;

            // Rotate to the input direction with the custom angle
            Quaternion rotation = Quaternion.LookRotation(movement) * Quaternion.Euler(0, rotationFactor, 0);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 10);

            animator.SetBool("Moving", true);
            transform.Translate(Vector3.forward * movement.magnitude * movementSpeed * Time.deltaTime);
        }
        else
        {
            // Stop walking
            animator.SetBool("Moving", false);
        }

    }

    public void HandleCrate()
    {
        animator.SetBool("IsCarryingBox", !animator.GetBool("IsCarryingBox"));
        crate.SetActive(!crate.activeSelf);
        isCarryingBox = !isCarryingBox;
    }
}