using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    public Slider slider;
   
    public void SetActive(float value)
    {
        slider.value = value;
    }

    public float SetMaxValue(float value)
    {
        slider.maxValue = value;

        slider.value = value;
    }
}
