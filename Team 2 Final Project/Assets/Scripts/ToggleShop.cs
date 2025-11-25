/* Adam Krenek
 * Final Project Game
 * This script manages the button to toggle the plant shop from view
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleShop : MonoBehaviour
{
    //reference to canvas that will be shown or hidden
    public CanvasGroup togglePlantStore;

    private GardenManager gm;

    public Toggle storeToggle;
    public bool openShop { get; private set; }

    private void Start()
    {
        gm = FindObjectOfType<GardenManager>();

        storeToggle.onValueChanged.AddListener(ToggleCanvas);

        ToggleCanvas(storeToggle.isOn);
    }
    public void ToggleCanvas(bool isOn)
    {
        if(togglePlantStore == null)
        {
            Debug.LogError("CanvasGroup is not Assigned");
            return;
        }
        if (isOn)
        {
            togglePlantStore.alpha = 1f;
            togglePlantStore.interactable = true;
            togglePlantStore.blocksRaycasts = true;

            openShop = true;
        }
        else
        {
            togglePlantStore.alpha = 0f;
            togglePlantStore.interactable = false;
            togglePlantStore.blocksRaycasts = false;

            openShop = false;

            if (gm != null)
            {
                gm.DeselectAll();
            }
        }

    }
}
