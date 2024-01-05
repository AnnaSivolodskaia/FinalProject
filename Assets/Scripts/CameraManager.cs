using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static GameObject mainCamera = Camera.main.gameObject;

    public static Dictionary<string, List<float>> cameraLocations = new Dictionary<string, List<float>>();

    public static void InitialiseDictionary()
    {
        //Adding values to the camera location with correponding coordinates
        cameraLocations.Add("MainMenuLocation", new List<float> { 80.43f, 25.55f, 56.58f, 7.484f, -118.73f, 0f });
        cameraLocations.Add("IntroLocation", new List<float> { 63.22f, 25.55f, 43.13f, 8.766f, -104.075f, -0.324f });
        cameraLocations.Add("1level_1", new List<float> { 100.1115f, 27.2599f, 86.3932f, 6.81f, 135f, 0f });
        cameraLocations.Add("2level_1", new List<float> { 87.60429f, 5.910524f, -18.13215f, 3.369f, 205.337f, 0f });
    }

    public static void SwitchActiveCamera(string cameraLocation)
    {
        //Extract exact coordinates from the camera locations
        float locX = cameraLocations[cameraLocation][0];
        float locY = cameraLocations[cameraLocation][1];
        float locZ = cameraLocations[cameraLocation][2];
        float angleX = cameraLocations[cameraLocation][3];
        float angleY = cameraLocations[cameraLocation][4];
        float angleZ = cameraLocations[cameraLocation][5];
        //Move Camera
        Vector3 newPosition = new Vector3(locX, locY, locZ);
        Vector3 newAngle = new Vector3(angleX, angleY, angleZ);
        mainCamera.transform.position = newPosition;
        mainCamera.transform.eulerAngles = newAngle;
    }
}
