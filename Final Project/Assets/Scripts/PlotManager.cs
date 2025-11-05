using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlotManager : MonoBehaviour
{
    bool isPlanted = false;

    SpriteRenderer plant;

    int plantStage = 0;

    float timer;

    BoxCollider2D plantCollider;

    public PlantObject selectedPlant;


    // Start is called before the first frame update
    void Start()
    {
        plant = transform.GetChild(0).GetComponent<SpriteRenderer>();
        plantCollider = transform.GetChild(0).GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    { if (isPlanted == true) {
        timer -= Time.deltaTime;

     if (timer < 0 && plantStage < selectedPlant.plantStages.Length-1)
        {
                timer = selectedPlant.timeBtwStages;
                plantStage++;
                UpdatePlant();
       }

      }
    }

    private void OnMouseDown()
    {
        if(isPlanted == true)
        {
            if (plantStage == selectedPlant.plantStages.Length - 1)
            {
                Harvest();
            }
        }
        else
        {
            Plant();
        }
    }

    void Harvest()
    {  
            Debug.Log("Harvested");
            isPlanted = false;
            plant.gameObject.SetActive(false);
        
    }
    void Plant()
    {
        Debug.Log("Planted");
        isPlanted = true;
        plantStage = 0;
        UpdatePlant();
        timer = selectedPlant.timeBtwStages;
        plant.gameObject.SetActive(true);
    }
    void UpdatePlant()
    {
        plant.sprite = selectedPlant.plantStages[plantStage];
        plantCollider.size = plant.sprite.bounds.size;
        plantCollider.offset = new Vector2(0, plant.bounds.size.y / 2);
    }
}
