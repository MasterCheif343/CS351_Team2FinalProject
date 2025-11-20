/* Adam Krenek
 * Final Project Game
 * This script manages the button to toggle the plant shop from view
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleButton : MonoBehaviour
{
    //reference to canvas that will be shown or hidden
    public CanvasGroup togglePlantStore;
   
    public void ToggleCanvas()
    {
        if(togglePlantStore != null)
        {
           if(  togglePlantStore.alpha > 0)
            {
                togglePlantStore.alpha = 0f;
                togglePlantStore.interactable = false;
                togglePlantStore.blocksRaycasts = false;
            }
            else
            {
                togglePlantStore.alpha = 1f;
                togglePlantStore.interactable = true;
                togglePlantStore.blocksRaycasts = true;
            }
        }
    }
}
