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

    public Slider slider;

    public bool isAirPurified = false;

    public void Start()
    {
        slider = GetComponent<Slider>();
        CurrentCO2InAir = CO2inAir;
        slider.maxValue = CO2inAir;
        slider.value = CurrentCO2InAir;
    }

    public void SetValue(float value)
    {
        slider.value = value;

        fill.color = gradient.Evaluate(slider.normalizedValue);
    }

    public void AirPollution(float amount, float duration, System.Action onComplete = null)
    {
        StartCoroutine(RisingCO2(amount, duration, onComplete));
    }

    private IEnumerator RisingCO2( float amount , float duration, System.Action onComplete)
    {
        float start = CurrentCO2InAir;
        float end = Mathf.Clamp(CurrentCO2InAir +  amount, 0, CO2inAir);
        float elpased = 0f;

        while (elpased < 6.5f)
        {
            elpased += Time.deltaTime / duration;

            CurrentCO2InAir = Mathf.Lerp(start, end, elpased);

            SetValue(CurrentCO2InAir);

            yield return null;
        }
        CurrentCO2InAir = end;
        SetValue(CurrentCO2InAir);

        onComplete?.Invoke();
    }
    public void Photosynthesis(float amount)
    {
        CurrentCO2InAir -= amount;
        CurrentCO2InAir = Mathf.Clamp(CurrentCO2InAir, 0, CO2inAir);

        Debug.Log("CO2 in air after day passes" + CurrentCO2InAir);
       
        SetValue(CurrentCO2InAir);

        if(slider.value <= 0)
        {
            isAirPurified = true;
        }
    }
}
