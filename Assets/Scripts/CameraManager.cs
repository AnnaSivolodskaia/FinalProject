using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static GameObject mainCamera = Camera.main.gameObject;

    public static Dictionary<string, List<float>> cameraLocations = new Dictionary<string, List<float>>();

    private Coroutine cameraTransitionCoroutine;

    private void Start()
    {
        InitialiseDictionary();
    }

    public static void InitialiseDictionary()
    {
        //adding values to the camera location with corresponding coordinates
        cameraLocations.Add("MainMenuLocation", new List<float> { 80.43f, 25.55f, 56.58f, 7.484f, -118.73f, 0f });
        cameraLocations.Add("IntroLocation", new List<float> { 63.22f, 25.55f, 43.13f, 8.766f, -104.075f, -0.324f });
        cameraLocations.Add("1level_1", new List<float> { 100.1115f, 27.2599f, 86.3932f, 6.81f, 135f, 0f });
        cameraLocations.Add("2level_1", new List<float> { 76.8f, 19.5f, -23.86f, 55.682f, 209.507f, 7.022f });
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