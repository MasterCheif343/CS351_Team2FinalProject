/* Adam Krenek
 * FinalGameProject
 * Let's the player buy plants and show them the type of plants up for sale
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlantItem : MonoBehaviour
{
    public PlantObject plant;
    GardenManager gardenManager;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI priceText;
    public Image icon;

    public Image buttonImage;
    public TextMeshProUGUI buttonText;
    // Start is called before the first frame update
    void Start()
    {   
        gardenManager = FindObjectOfType<GardenManager>();
         InitializeUI();

    }

    public void BuyPlant()
    {
        Debug.Log("Bought " +  plant.name);
        gardenManager.SelectPlant(this);
    }
    void InitializeUI()
    {
        nameText.text = plant.name;
        priceText.text = "$" + plant.buyPrice;
        icon.sprite = plant.icon;
    }
}
