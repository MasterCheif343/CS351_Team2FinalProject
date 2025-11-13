using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlotManager : MonoBehaviour
{
    GardenManager gm;
    bool isPlanted = false;
    SpriteRenderer plant;
    int plantStage = 0;
    DayProgression days;
    BoxCollider2D plantCollider;
    public Color availableColor = Color.green;
    public Color unavailableColor = Color.red;
    public PlantObject selectedPlant;
    SpriteRenderer plot;
    // Start is called before the first frame update
    void Start()
    {
        plant = transform.GetChild(0).GetComponent<SpriteRenderer>();
        plantCollider = transform.GetChild(0).GetComponent<BoxCollider2D>();
        gm = transform.parent.GetComponent<GardenManager>();
        plot = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (isPlanted)
        {
            if (DayProgression.Day <= 0 && plantStage < selectedPlant.plantStages.Length - 1)
            {
                plantStage++;
                UpdatePlant();
                DayProgression.Day = selectedPlant.daysBetweenStages;
            }
        }
    }

    private void OnMouseDown()
    {
        if (isPlanted)
        {
            if (plantStage == selectedPlant.plantStages.Length - 1 && !gm.isPlanting)
            {
                Harvest();
            }
        }
        else if(gm.isPlanting && gm.selectPlant.plant.buyPrice <= gm.money)
        {
            Plant(gm.selectPlant.plant);
        }
    }
    //visual cue
    private void OnMouseOver()
    {
        if (gm.isPlanting)
        {
            if(isPlanted || gm.selectPlant.plant.buyPrice > gm.money)
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
        DayProgression.Day = selectedPlant.daysBetweenStages;
        plant.gameObject.SetActive(true);
    }
    void Harvest()
    {
        isPlanted = false;
        plant.gameObject.SetActive(false);
        gm.Transaction(selectedPlant.buyPrice);
    }
   void UpdatePlant()
    {
        plant.sprite = selectedPlant.plantStages[plantStage];
        plantCollider.size = plant.sprite.bounds.size;
        plantCollider.offset = new Vector2(0, plant.bounds.size.y / 2);
    }
}
