using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlotManager : MonoBehaviour
{
    bool isPlanted = false;

     SpriteRenderer plant;

    public Sprite[] plantStages;

    int plantStage = 0;

    float timeBetweenStages = 2f;

    float timer;

    BoxCollider2D plantCollider;


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

     if (timer < 0 && plantStage < plantStages.Length-1)
        {
                timer = timeBetweenStages;
                plantStage++;
                UpdatePlant();
       }

      }
    }

    private void OnMouseDown()
    {
        if(isPlanted == true)
        {
            if (plantStage == plantStages.Length - 1)
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
        timer = timeBetweenStages;
        plant.gameObject.SetActive(true);
    }
    void UpdatePlant()
    {
        plant.sprite = plantStages[plantStage];
        plantCollider.size = plant.sprite.bounds.size;
        plantCollider.offset = new Vector2(0, plant.bounds.size.y / 2);
    }
}
