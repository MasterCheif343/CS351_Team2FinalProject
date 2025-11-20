using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

public class SliderController : MonoBehaviour
{
    public Gradient gradient;

    public Image fill;

    public float CO2inAir = 100;

    public float CurrentCO2InAir;

    public Slider slider;

    PlantObject PlantObject;

    public bool isAirPurified = false;

    public void Start()
    {
        CurrentCO2InAir = CO2inAir;
        slider.maxValue = CO2inAir;
        slider.value = CurrentCO2InAir;
        isAirPurified = false;
    }

    public void SetValue(float value)
    {
        slider.value = value;

        fill.color = gradient.Evaluate(slider.normalizedValue);
    }

    public void SetMaxValue(float value)
    {
        slider.maxValue = value;

        slider.value = value;

        fill.color = gradient.Evaluate(1f);
    }

    public void Photosynthesis(float amount)
    {
        CurrentCO2InAir -= amount;
        CurrentCO2InAir = Mathf.Clamp(CurrentCO2InAir, 0, CO2inAir);
        slider.value = CurrentCO2InAir;
        if(slider.value <= 0)
        {
            isAirPurified = true;
        }
    }
}
