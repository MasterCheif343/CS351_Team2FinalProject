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
    public float baseIncrease = 2f;
    public float multiplier = 1f;
    public int daysUntilMultInc = 5;
    public float lerpDuration = 2f;

    public static int Day;
    public static System.Action OnDayChanged;

    //set this this in the inspector
    public TMP_Text textbox;
    public Button button;
    
    
    // Start is called before the first frame update
    void Start()
    { 
        textbox.text = "Day: " + Day;
        button.onClick.AddListener(NextDay);
    }
    private void NextDay()
        {
        Day += 1;
        Debug.Log("Button was clicked!");
        if (textbox != null) textbox.text = "Day: " + Day;

        if (Day % daysUntilMultInc == 0)
        {
            multiplier *= 2f;
        }
        float airPollution = baseIncrease * multiplier;

        OnDayChanged?.Invoke();

        sc.AirPollution(airPollution, lerpDuration);
    
    }
}
