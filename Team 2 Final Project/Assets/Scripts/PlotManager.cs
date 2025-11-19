/* Adam Krenek
 * FinalGameProject
 * This script manages the plots of land the player is going to use
 * They can grow plants and choose which plants to choose based on how much money they have
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlotManager : MonoBehaviour
{
    GardenManager gm;
   
    bool isPlanted = false;
    
    SpriteRenderer plant;
   
    int plantStage = 0;
    
    float daysRemaining;
   
    BoxCollider2D plantCollider;
   
    public Color availableColor = Color.green;
    
    public Color unavailableColor = Color.red;
   
    public PlantObject selectedPlant;
   
    SpriteRenderer plot;
   
    bool isDry = true;
   
    public Sprite dryPlot;
   
    public Sprite normalPlot;

    float growSpeed = 1f;

    public bool isPrepared = true;

    public Sprite unpreparedPlot;
    // Start is called before the first frame update
    void Start()
    {
        plant = transform.GetChild(0).GetComponent<SpriteRenderer>();
        plantCollider = transform.GetChild(0).GetComponent<BoxCollider2D>();
        gm = transform.parent.GetComponent<GardenManager>();
        plot = GetComponent<SpriteRenderer>();
        plot.sprite = dryPlot;
        if (isPrepared)
        {
            plot.sprite = dryPlot;
        }
        else
        {
            plot.sprite = unpreparedPlot;
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (isPlanted && !isDry)
        {
            daysRemaining -= (1 * growSpeed);
            if (daysRemaining <= 0 && plantStage < selectedPlant.plantStages.Length - 1)
            {
                plantStage++;
                UpdatePlant();
                daysRemaining = selectedPlant.daysBetweenStages;
            }
        }
    }

    private void OnMouseDown()
    {
        if (isPlanted)
        {
            if (plantStage == selectedPlant.plantStages.Length - 1 && !gm.isPlanting && !gm.isSelecting)
            {
                Harvest();
            }
        }
        else if(gm.isPlanting && gm.selectPlant.plant.buyPrice <= gm.money && isPrepared)
        {
            Plant(gm.selectPlant.plant);
        }
        if (gm.isSelecting)
        {
            switch (gm.selectedTool)
            {
                //Watering plot
                case 1:
                    if (isPrepared)
                    {
                        Debug.Log("Watered Plot!");
                        isDry = false;
                        plot.sprite = normalPlot;
                        if (isPlanted)
                        {
                            UpdatePlant();
                        }
                    }
                    break;

                case 2:
                    Debug.Log("Fertilized!");
                    if (gm.money >= 10)
                    {
                        gm.Transaction(-10);
                        if (growSpeed < 2) { growSpeed += .2f; }
                    }
                    break;

                case 3:
                    
                    if (gm.money >= 20 && !isPrepared)
                    {
                        Debug.Log("Prepared Plot!");
                        gm.Transaction(-20);
                    }
                        isPrepared = true;
                        plot.sprite = dryPlot;
                        break;
                    
                default:
                    break;
            }
        }
    }
    //visual cue
    private void OnMouseOver()
    {
        if (gm.isPlanting)
        {
            if(isPlanted || gm.selectPlant.plant.buyPrice > gm.money ||!isPrepared )
            {
                //can't buy
                plot.color = unavailableColor;
            }
            else
            {
                //can buy
                plot.color = availableColor;
            }
        }
        if (gm.isSelecting)
        {
            switch (gm.selectedTool)
            {
                case 1:
                case 2:
                    if (isPrepared && gm.money >= (gm.selectedTool -1) * 10)
                    {
                        plot.color = availableColor;
                    }
                    else
                    {
                        plot.color = unavailableColor;
                    }
                        break;
                case 3:
                    if (!isPrepared && gm.money >= 20)
                    {
                        plot.color = availableColor;
                    }
                    else
                    {
                        plot.color = unavailableColor;
                    }
                    break;
                default:
                    plot.color = unavailableColor;
                    break;
            }
        }
    }
    private void OnMouseExit()
    {
        plot.color = Color.white;
    }

    void Plant(PlantObject newPlant)
    {
        selectedPlant = newPlant;
        isPlanted = true;

        gm.Transaction(-selectedPlant.buyPrice);

        plantStage = 0;
        UpdatePlant();
        daysRemaining = selectedPlant.daysBetweenStages;
        plant.gameObject.SetActive(true);
    }
    void Harvest()
    {
        isPlanted = false;
        plant.gameObject.SetActive(false);
        gm.Transaction(selectedPlant.sellPrice);
        isDry = true;
        plot.sprite = dryPlot;
        growSpeed = 1f;
    }
   void UpdatePlant()
    {
        if (isDry)
        {
            plant.sprite = selectedPlant.dryPlanted;
        }
        else
        {
            plant.sprite = selectedPlant.plantStages[plantStage];
        }
        plantCollider.size = plant.sprite.bounds.size;
        plantCollider.offset = new Vector2(0, plant.bounds.size.y / 2);
    }
}
