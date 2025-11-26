/* Adam Krenek
 * FinalGameProject
 * Let's the player buy plants and show them the type of plants up for sale
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class PlantItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public PlantObject plant;

    GardenManager gardenManager;

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI priceText;

    public Image icon;

    public Image buttonImage;

    public TextMeshProUGUI buttonText;

    [Header("Local Description Text")]
   public TMP_Text descriptionText;

    // Start is called before the first frame update
    void Awake()
    {
        Description descriptionScript = FindObjectOfType<Description>();
        if (descriptionScript != null)
        {
           descriptionText = descriptionScript.descriptionText;
        }
        else
        {
            Debug.LogError("Description script is not found!");
        }

        gardenManager = FindObjectOfType<GardenManager>();
    }
    void Start()
    {
        descriptionText.gameObject.SetActive(false);
        InitializeUI();
    }

    public void BuyPlant()
    {
        Debug.Log("Bought " + plant.name);
        gardenManager.SelectPlant(this);
    }
    void InitializeUI()
    {
        nameText.text = plant.name;
        priceText.text = "$" + plant.buyPrice;
        icon.sprite = plant.icon;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (descriptionText != null)
        {
            Debug.Log("Pointer Enter: " + plant.name);

            descriptionText.text = plant.description;

            descriptionText.gameObject.SetActive(true);
        }

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (descriptionText != null)
        {
            Debug.Log("Pointer Left: " + plant.name);

            descriptionText.gameObject.SetActive(false);
        }
    }
}

