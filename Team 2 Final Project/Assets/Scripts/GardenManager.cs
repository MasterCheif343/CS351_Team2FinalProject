/* Adam Krenek
 * FinalGameProject
 * This script helps manage stuff behind the scenes, no pun intended
 * Manages stuff with plants and tools
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GardenManager : MonoBehaviour
{
    public PlantItem selectPlant;
    public bool isPlanting = false;
    public bool isSelecting = false;
    public int money = 100;
    public TextMeshProUGUI moneyText;
    public int selectedTool = 0;
    // 1 = water, 2 = Fertillizer, and 3 = prepare plot ^
    public Color buyColor = Color.green;
    public Color cancelColor = Color.red;

    public Image[] buttonImages;
    public Sprite normalButton;
    public Sprite selectedButton;
    // Start is called before the first frame update
    void Start()
    {
        moneyText.text = "$" + money;
    }

    //can either plant or use tools, but not both
    public void SelectTool(int toolNum)
    {
        if(toolNum == selectedTool)
        {
            //deselect
            DeselectAll();
        }
        else
        {
            //select tool number and check if anything else was slected
            DeselectAll();
            isSelecting = true;
            selectedTool = toolNum;
            buttonImages[toolNum - 1].sprite = selectedButton;
        }
    }

    public void DeselectAll()
    {
        if (isPlanting)
        {
            isPlanting = false;
            if(selectPlant != null)
            {
                selectPlant.buttonImage.color = buyColor;
                selectPlant.buttonText.text = "Buy";
                selectPlant = null;
            }
        }
        if (isSelecting)
        {
            if(selectedTool > 0)
            {
                buttonImages[selectedTool - 1].sprite = normalButton;
            }
            isSelecting = false;
            selectedTool = 0;
        }
    }
    public void SelectPlant(PlantItem newPlant)
    {
        if(selectPlant == newPlant)
        {
            DeselectAll();
        }
        else
        {
            DeselectAll();
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
