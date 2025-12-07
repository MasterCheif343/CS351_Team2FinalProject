/* Adam Krenek
 * Final Game Project
 * This script manages how much CO2 is in the game 
 * and manages how the slider bar looks
 */


using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

public class SliderController : MonoBehaviour
{
    public Gradient gradient;

    public Image fill;

    public float CO2inAir = 100f;

    public float CurrentCO2InAir;

    public bool isAirPurified = false;

    public Slider slider;

   // private bool isChangingCO2 = false;

   // public float CO2ChangeRate = 2f;

    public float dailyCO2IncreaseRate = 0.5f;

    //private float targetCO2;

    public void Start()
    {
        slider = GetComponent<Slider>();
       
        CurrentCO2InAir = CO2inAir;
        //targetCO2 = CurrentCO2InAir;

        slider.maxValue = CO2inAir;
        slider.value = CurrentCO2InAir;

        SetValue(CurrentCO2InAir);
    }

    private void OnEnable()
    {
        DayProgression.OnDayChanged += AddDailyPollution;
    }

    private void OnDisable()
    {
        DayProgression.OnDayChanged -= AddDailyPollution;
    }
  /*  public void Update()
    {
        if (isChangingCO2)
        {
            CurrentCO2InAir = Mathf.MoveTowards(CurrentCO2InAir, targetCO2, CO2ChangeRate * Time.deltaTime);

            SetValue(CurrentCO2InAir);

            if(CurrentCO2InAir == targetCO2)
            {
                isChangingCO2 = false;

                Debug.Log("CO2 in air after day passes" + CurrentCO2InAir);

                if (CurrentCO2InAir <= 0)
                {
                    isAirPurified = true;
                }
            }
        }
    } */

    private void AddDailyPollution()
    {
        AirPollution(dailyCO2IncreaseRate);
    }
    public void SetValue(float value)
    {
        slider.value = value;

        fill.color = gradient.Evaluate(slider.normalizedValue);
    }

    public void AirPollution(float amount /*, float duration, System.Action onComplete = null*/)
    {

        //StartCoroutine(RisingCO2(amount, duration, onComplete));
        CurrentCO2InAir = Mathf.Clamp(CurrentCO2InAir + amount, 0, CO2inAir);

       SetValue(CurrentCO2InAir);

        Debug.Log("Current CO2 in air after AirPollution function is called: " + CurrentCO2InAir);

        if(CurrentCO2InAir <= 0)
        {
            isAirPurified = true;
        }
    }

   /* private IEnumerator RisingCO2( float amount , float duration, System.Action onComplete)
    {
        float start = CurrentCO2InAir;
        float end = Mathf.Clamp(CurrentCO2InAir +  amount, 0, CO2inAir);
        float elpased = 0f;

        while (elpased < 1f)
        {
            elpased += Time.deltaTime / duration;

            CurrentCO2InAir = Mathf.Lerp(start, end, elpased);

            SetValue(CurrentCO2InAir);

            yield return null;
        }
        CurrentCO2InAir = end;
        SetValue(CurrentCO2InAir);

        onComplete?.Invoke();
    } */
    public void Photosynthesis(float amount)
    { 
        CurrentCO2InAir = Mathf.Clamp(CurrentCO2InAir - amount, 0, CO2inAir);

        SetValue(CurrentCO2InAir);

        Debug.Log("CO2 removed: " + amount);
        Debug.Log("CO2 after photosynthesis: " + CurrentCO2InAir);

        if (CurrentCO2InAir < 0) {
            
            isAirPurified = true;
        }
    }
}
