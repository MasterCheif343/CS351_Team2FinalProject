using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEngine;

public class ProgressManager : MonoBehaviour
{
    public TMP_Text textBox;

    public int scoreToProgress;

    public static bool levelEnd;
    public static bool passedLevel;
    public static int score;
    void Awake()
    {
        scoreToProgress = 10;
    }
    private void Start()
    {
        levelEnd = false;
        passedLevel = false;
        score = 0;
    }
    void Update()
    {
        if (!levelEnd)
        {
            textBox.text = "Garbage collected: " + score;
        }
        if (score >= scoreToProgress)
        {
            passedLevel = true;
            levelEnd = true;

            score = 0;

            textBox.text = "Press R to load the next level";
        }
       
        if (levelEnd)
        {
            if (passedLevel)
            {
                textBox.text = "You have collected all of the garbage in this part of the forest, Press R to continue to the next level!";
            }
        
        }
    }
}
