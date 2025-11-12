using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GardenManager : MonoBehaviour
{
    public PlantItem selectPlant;
    public bool isPlanting = false;
    public int money = 100;
    public TextMeshProUGUI moneyText;

    public Color buyColor = Color.green;
    public Color cancelColor = Color.red;
    // Start is called before the first frame update
    void Start()
    {
        moneyText.text = "$" + money;
    }
    public void SelectPlant(PlantItem newPlant)
    {
        if(selectPlant == newPlant)
        {
            selectPlant.buttonImage.color = buyColor;
            selectPlant.buttonText.text = "Buy";
            selectPlant = null;
            isPlanting = false;
            
        }
        else
        {
            if (selectPlant != null)
            {
                selectPlant.buttonImage.color = buyColor;
                selectPlant.buttonText.text = "Buy";
            }
            selectPlant  = newPlant;
            selectPlant.buttonImage.color = cancelColor;
            selectPlant.buttonText.text = "Cancel";
            isPlanting = true;
        }
    }

    public void Transaction(int value)
    {
        money += value/2;
        moneyText.text = "$" + money;
    }
}
