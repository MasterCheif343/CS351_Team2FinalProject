/* Adam Krenek
 * FinalGameProject
 * This script manages the plots of land the player is going to use
 * They can grow plants and choose which plants to choose based on how much money they have
 */
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlotManager : MonoBehaviour
{
    GardenManager gm;

   public SliderController sliderController;
   
    bool isPlanted = false;
    
    SpriteRenderer plant;
   
    int plantStage = 0;
    
    int daysRemaining;
   
    BoxCollider2D plantCollider;
   
   // public Color availableColor = Color.green;
    
    //public Color unavailableColor = Color.red;
   
    public PlantObject selectedPlant;
   
    SpriteRenderer plot;

    public TextMeshProUGUI soldText;
   
   // bool isDry = true;
   
    //public Sprite dryPlot;
   
    //public Sprite normalPlot;

   // float growSpeed = 1f;

    //public bool isPrepared = true;

   // public Sprite unpreparedPlot;
    // Start is called before the first frame update
    void Start()
    {
        if (sliderController == null)
        {
            sliderController = FindObjectOfType<SliderController>();
        }
        plant = transform.GetChild(0).GetComponent<SpriteRenderer>();

        plantCollider = transform.GetChild(0).GetComponent<BoxCollider2D>();

        gm = transform.parent.GetComponent<GardenManager>();

        plot = GetComponent<SpriteRenderer>();

        plant.gameObject.SetActive(false);

        //plot.sprite = dryPlot;
        /*if (isPrepared)
        {
            plot.sprite = dryPlot;
        }
        else
        {
            plot.sprite = unpreparedPlot;
        } */
    }
    private void OnMouseDown()
    {
        if (isPlanted)
        {
            if (plantStage == selectedPlant.plantStages.Length - 1 && 
                !gm.isPlanting && !gm.isSelecting)
            {
                Harvest();
                return;
            }
        }
        else if(gm.isPlanting && !isPlanted && gm.selectPlant.plant.buyPrice <= gm.money /* && isPrepared */)
        {
            Plant(gm.selectPlant.plant);
        }
       /* if (gm.isSelecting)
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
        }*/
    }
    //visual cue
   /* private void OnMouseOver()
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
    }*/

    void Plant(PlantObject newPlant)
    {
        selectedPlant = newPlant;
        isPlanted = true;

        gm.Transaction(-selectedPlant.buyPrice);

        plantStage = 0;
        daysRemaining = selectedPlant.daysBetweenStages;

        plant.gameObject.SetActive(true);
        UpdatePlant();
    }
    void Harvest()
    {
        isPlanted = false;
        plant.gameObject.SetActive(false);
        gm.Transaction(selectedPlant.sellPrice);
        Debug.Log("Harvested!");
        if (soldText != null)
        {
            soldText.text = "Sold: " + selectedPlant.name;
            StartCoroutine(FadeText());
        }
            //isDry = true;
            //plot.sprite = dryPlot;
            //growSpeed = 1f;
        }

    private IEnumerator FadeText()
    {
        soldText.alpha = 1f;
        yield return new WaitForSeconds(2f);

        while (soldText.alpha > 0)
        {
           soldText.alpha -= Time.deltaTime;
            yield return null;
        }
    }
        void UpdatePlant()
    {
        /* if (isDry)
        {
            plant.sprite = selectedPlant.dryPlanted;
        }
        else
        {
            --daysRemaining; 
            plant.sprite = selectedPlant.plantStages[plantStage];
        } */
        plant.sprite = selectedPlant.plantStages[plantStage];
        plantCollider.size = plant.sprite.bounds.size;
        plantCollider.offset = new Vector2(0, plant.bounds.size.y / 2);
    }

    private void HandleDayPassed()
    {
        if (!isPlanted)
        {
            return;
        }
        sliderController.Photosynthesis(selectedPlant.CO2RemovingFactor);

            --daysRemaining;
        
        if (daysRemaining <= 0 && plantStage < selectedPlant.plantStages.Length - 1)
        {
            plantStage++;
            UpdatePlant();
            daysRemaining = selectedPlant.daysBetweenStages;
        }

        Debug.Log("Plot has gone through a day");
    }

    private void OnEnable()
    {
        DayProgression.OnDayChanged += HandleDayPassed;
    }

    private void OnDisable()
    {
        DayProgression.OnDayChanged -= HandleDayPassed;
    }
}
