using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static GameObject mainCamera;

    public static Dictionary<string, List<float>> cameraLocations = new();

    private Coroutine cameraTransitionCoroutine;

    private void Awake()
    {
        mainCamera = Camera.main.gameObject;
    }

    public static void InitiateDictionary()
    {
        //adding values to the camera location with corresponding coordinates
        cameraLocations.Add("MainMenuLocation", new List<float> { 89.68369f, 29.427282f, 59.98083f, 9.385f, -113.581f, 0.001f });

        cameraLocations.Add("CutSceneLocation", new List<float> { 68.85627f, 25.95111f, 43.02375f, 5.26f, -79.996f, 0f });

        cameraLocations.Add("1lvl_1", new List<float> { 106.74057f, 30.695137f, 73.5946f, 12.135f, 119.089f, -2.36f });

        cameraLocations.Add("2lvl_1", new List<float> { 156.6668f, 47.25932f, 24.92f, 49f, 207f, 6.075f });

        cameraLocations.Add("OutroScene", new List<float> { 101.6833f, 30.28898f, -37.51565f, 11.38f, -3.162f, 0f });
    }

    public static void SwitchActiveCamera(string cameraLocation)
    {
        if (mainCamera != null && cameraLocations.ContainsKey(cameraLocation))
        {
            if (mainCamera.GetComponent<CameraManager>() == null)
            {
                mainCamera.AddComponent<CameraManager>();
            }

            CameraManager cameraManager = mainCamera.GetComponent<CameraManager>();

            if (cameraManager.cameraTransitionCoroutine != null)
            {
                cameraManager.StopCoroutine(cameraManager.cameraTransitionCoroutine);
            }

            cameraManager.cameraTransitionCoroutine = cameraManager.StartCoroutine(cameraManager.TransitionCamera(cameraLocation));
        }
        else
        {
            Debug.LogWarning("Camera or camera location not found.");
        }
    }

    private IEnumerator TransitionCamera(string cameraLocation)
    {
        //extract exact coordinates from the camera locations
        float locX = cameraLocations[cameraLocation][0];
        float locY = cameraLocations[cameraLocation][1];
        float locZ = cameraLocations[cameraLocation][2];
        float angleX = cameraLocations[cameraLocation][3];
        float angleY = cameraLocations[cameraLocation][4];
        float angleZ = cameraLocations[cameraLocation][5];

        Vector3 targetPosition = new Vector3(locX, locY, locZ);
        Vector3 targetAngles = new Vector3(angleX, angleY, angleZ);

        //changes the speed of the transition
        float duration = 1.5f; 
        float elapsedTime = 0f;

        Vector3 initialPosition = mainCamera.transform.position;
        Quaternion initialRotation = mainCamera.transform.rotation;

        while (elapsedTime < duration)
        {
            mainCamera.transform.position = Vector3.Lerp(initialPosition, targetPosition, elapsedTime / duration);
            mainCamera.transform.rotation = Quaternion.Slerp(initialRotation, Quaternion.Euler(targetAngles), elapsedTime / duration);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        //set final position and rotation
        mainCamera.transform.position = targetPosition;
        mainCamera.transform.rotation = Quaternion.Euler(targetAngles);

        //reset the coroutine reference
        cameraTransitionCoroutine = null;
    }
}