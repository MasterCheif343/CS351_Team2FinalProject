using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.UI;

public class PlantSliderBars : MonoBehaviour
{
    public Gradient healthGradient;

    public Gradient progressGradient;

    public Image fill1;

    public Slider healthSlider;

    public Slider progressSlider;

    public Image fill2;

    public int currentStage;

    public int maxStages;
    // Start is called before the first frame update
    public void Initialize(float maxHealth)
    {
        SetMaxHealth(maxHealth);
        SetHealth(maxHealth);
        SetCurrentProgress(0);
       
    }

    public void SetMaxHealth(float health)
    {
        healthSlider.maxValue = health;

        healthSlider.value = health;

        fill1.color = healthGradient.Evaluate(1f);
    }

    public void SetHealth(float health)
    {
        healthSlider.value = health;

        fill1.color = healthGradient.Evaluate(healthSlider.normalizedValue);
    }

    public void SetMaxProgress(int stages)
    {
        progressSlider.maxValue = stages - 1;
        progressSlider.value = 0;
    }

    public void SetCurrentProgress(int stage) { 
       progressSlider.value = stage;
    }
}
