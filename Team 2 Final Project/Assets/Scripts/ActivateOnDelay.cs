using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActivateOnDelay : MonoBehaviour
{
    public static int NewDay = 1;
    public void Activate()
    {
        NewDay +=1;
        
        if (NewDay % 7 == 0)
        {
            gameObject.SetActive(true);
            Debug.Log($"{name} activated on Day {DayProgression.Day}");
            
        }
        else
        {
            Debug.Log($"{name} NOT activated — Day {DayProgression.Day} is not a multiple of 7");
        }
    }
    public void Deactivate()
    {
        gameObject.SetActive (false);
    }
    


    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        
    }
}
