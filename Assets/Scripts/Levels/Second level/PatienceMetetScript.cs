using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;

public class PatienceMetetScript : MonoBehaviour
{
    public float remainingPatienceValue = 1f;
    public Slider slider;
    public GameObject sliderFill;
    public SecondLevelDwellerScript parentScript;

    public GameObject happy;
    public GameObject sad;
    public GameObject neutral;
    public GameObject satisfied;
    public GameObject angry;



    public bool deactivationFlag = false;

    // Update is called once per frame
    void Update()
    {
        if (!StatesManager.gameStates[StatesManager.currentGameState].isLevel)
        {
            try
            {
                gameObject.SetActive(false);
            }
            catch (System.Exception){}
        }
        else
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(new Vector3(0f, 200f, 0f)), 1f);
            // coloring the patience meter
            SetSliderValueAndColor(remainingPatienceValue);
        }

        if(parentScript.isExiting && !deactivationFlag)
        {
            deactivatePatienceMeter();
            deactivationFlag = true;
        }
    }

    public async void deactivatePatienceMeter()
    {
        await Wait(5f);
        gameObject.SetActive(false);
    }

    public void SetSliderValueAndColor(float _remainingPatienceValue)
    {
        if (parentScript.isServed)
        {
            slider.value = 1f;
            sliderFill.GetComponent<Image>().color = Color.green;
            satisfied.SetActive(true);
            angry.SetActive(false);
            happy.SetActive(false);
            neutral.SetActive(false);
            sad.SetActive(false);
        }
        else if (parentScript.isExiting)
        {
            slider.value = 1f;
            sliderFill.GetComponent<Image>().color = Color.red;
            angry.SetActive(true);
            satisfied.SetActive(false);
            happy.SetActive(false);
            neutral.SetActive(false);
            sad.SetActive(false);
        }
        else
        {
            slider.value = _remainingPatienceValue;
            if (remainingPatienceValue > 0.7f)
            {
                sliderFill.GetComponent<Image>().color = Color.green;
                happy.SetActive(true);
                angry.SetActive(false);
                satisfied.SetActive(false);
                neutral.SetActive(false);
                sad.SetActive(false);
            }
            else if (remainingPatienceValue > 0.4f)
            {
                sliderFill.GetComponent<Image>().color = Color.yellow;
                neutral.SetActive(true);
                angry.SetActive(false);
                happy.SetActive(false);
                satisfied.SetActive(false);
                sad.SetActive(false);
            }
            else
            {
                sliderFill.GetComponent<Image>().color = Color.red;
                sad.SetActive(true);
                angry.SetActive(false);
                happy.SetActive(false);
                neutral.SetActive(false);
                satisfied.SetActive(false);
            }
        }

    }

    private async Task Wait(float time)
    {
        float startTime = Time.time;
        float currentTime = startTime;
        if (StatesManager.gameStates[StatesManager.currentGameState].isLevel)
        {
            while (currentTime - startTime < time)
            {
                currentTime += Time.deltaTime;
                remainingPatienceValue = (time - (currentTime - startTime)) / time;
                await Task.Yield();
            }
        }
    }

    public async void PatienceCountDown(float patienceCapacity)
    {
        parentScript = transform.GetComponentInParent<SecondLevelDwellerScript>();
        await Wait(patienceCapacity);
        Debug.Log("Count Down completed");
        if (StatesManager.gameStates[StatesManager.currentGameState].isLevel)
        {
            Debug.Log("Leaving dweller is called");
            parentScript.LeavingDweller();
        }

    }
}