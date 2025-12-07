/*
* Donovan Clark
* FinalProjectGame
* Changes the text being shown and 
* enables the button to be used and increase the day when it is clicked.
*/
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class DayProgression : MonoBehaviour
{
    public SliderController sc;
    //public float baseIncrease = 2f;
    //public float multiplier = 1f;
   // public int daysUntilMultInc = 5;
    //public float lerpDuration = 2f;

    public static int Day = 1;
    public static System.Action OnDayChanged;
    public Light directionalLight;  
    public float dimIntensity = 0.2f;
    public float normalIntensity = 1f;
    public float dimDuration = 1f;   
    public float holdDimTime = 2f;   
    public float brightenDuration = 1f;

    //set this this in the inspector
    public TMP_Text textbox;
    //public TMP_Text statusText;
    public Button button;
    //public Button statusReport;
    public Animator dayAnimator;
   // public GameObject DayToNight;
   // public GameObject NightToDay;
    public GameObject PlayerInput;
    public float delay = 5f;
   
   

    // Start is called before the first frame update
    void Start()
    { 
        textbox.text = "Day: " + Day;
        button.onClick.AddListener(NextDay);
      //  statusReport.onClick.AddListener(OnDestroy);
      //  statusText.text = "Day's passed:" + Day;
    }
    private void NextDay()
        {
        if (GameManager.gameOver)
            return;
        Day += 1;
        Debug.Log("Button was clicked!");
        if (textbox != null) textbox.text = "Day: " + Day;
      /*  if (textbox != null) statusText.text = "Day's passed: " + Day;
        if (Day % 7 == 0)
        {
            spawnedSunAndMoon = Instantiate(DayToNight);
            button.gameObject.SetActive(false);
        }
      */
        
            StartCoroutine(HideShowButton(delay));
            StartCoroutine(DimLightRoutine());

        OnDayChanged?.Invoke();

      /*  if (Day % daysUntilMultInc == 0)
        {
            multiplier *= 2f;
        }
        float airPollution = baseIncrease * multiplier;

        sc.AirPollution(airPollution , lerpDuration ); */
    
    } 
  /*  private void OnDestroy()
    {
        Debug.Log("Button was clicked!");
        if (mainCamera != null)
        {
            // Stop any movement by resetting position
            mainCamera.transform.position = cameraStartPos;
        }
        if (spawnedSunAndMoon != null)
        {
            Debug.Log("Destroying clone: " + spawnedSunAndMoon.name);
            Destroy(spawnedSunAndMoon);
            spawnedSunAndMoon = Instantiate(NightToDay);
            StartCoroutine(HideShowButton(delay));

        }
        else
        {
            Debug.Log("No clone exists to destroy.");
        }
    }
  */
    private IEnumerator HideShowButton(float delay)
    {
        if (button == null)
        {
            Debug.LogWarning("HideShowButton called but 'button' is null.");
            yield break;
        }

        // hides the button
        button.gameObject.SetActive(false);
        PlayerInput.gameObject.SetActive(false);

        // wait for the inputer amount of seconds
        yield return new WaitForSeconds(delay);

        // show the button again
        button.gameObject.SetActive(true);
        PlayerInput.gameObject.SetActive(true);
    }
    private IEnumerator DimLightRoutine()
    {
        if (directionalLight == null)
            yield break;

        float startIntensity = directionalLight.intensity;

        float t = 0;
        while (t < dimDuration)
        {
            t += Time.deltaTime;
            directionalLight.intensity = Mathf.Lerp(startIntensity, dimIntensity, t / dimDuration);
            yield return null;
        }

        yield return new WaitForSeconds(holdDimTime);

        t = 0;
        while (t < brightenDuration)
        {
            t += Time.deltaTime;
            directionalLight.intensity = Mathf.Lerp(dimIntensity, normalIntensity, t / brightenDuration);
            yield return null;
        }

        directionalLight.intensity = normalIntensity;
    }
}
