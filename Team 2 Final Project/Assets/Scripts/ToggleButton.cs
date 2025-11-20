/* Adam Krenek
 * Final Project Game
 * This script manages the button to toggle the plant shop from view
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ToggleShop: MonoBehaviour
{
    public GameObject plantStore;
    public Toggle toggleShop;

    private void Start()
    {
        toggleShop.isOn = plantStore.activeSelf;

        toggleShop.onValueChanged.AddListener(HideOrShowShop);
    }
    void HideOrShowShop(bool isOn)
    {
        plantStore.SetActive(isOn);
    }
}
