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

    public PlantSliderBars plantSliderBars;
   
    bool isPlanted = false;
    
    SpriteRenderer plant;

    public int currentStageProgress;

    public int totalStages;
   
    int plantStage = 0;
    
    int daysRemaining;
   
    BoxCollider2D plantCollider;
   
    public PlantObject selectedPlant;
   
    SpriteRenderer plot;

    public float currentPlantHealth;

    public int currentPlantProgress;

    private AudioSource plantAudio;

    public AudioClip plantingSound;

    public AudioClip harventSound;


    // Start is called before the first frame update
    void Start()
    {   if(plantSliderBars == null)
        {
            plantSliderBars = FindObjectOfType<PlantSliderBars>();
        }

        if (sliderController == null)
        {
            sliderController = FindObjectOfType<SliderController>();
        }
        plant = transform.GetChild(0).GetComponent<SpriteRenderer>();

        plantCollider = transform.GetChild(0).GetComponent<BoxCollider2D>();

        gm = transform.parent.GetComponent<GardenManager>();

        plot = GetComponent<SpriteRenderer>();

        plant.gameObject.SetActive(false);

        plantAudio = GetComponent<AudioSource>();
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
    }
    
    void Plant(PlantObject newPlant)
    {
        selectedPlant = newPlant;
        isPlanted = true;

        gm.Transaction(-selectedPlant.buyPrice);

        plantStage = 0;
        daysRemaining = selectedPlant.daysBetweenStages;

        currentPlantHealth = selectedPlant.maxPlantHealth;
        currentPlantProgress = 0;

        if(plantSliderBars != null)
        {
            plantSliderBars.Initialize(selectedPlant.maxPlantHealth);
            plantSliderBars.SetMaxProgress(selectedPlant.plantStages.Length);
            plantSliderBars.SetCurrentProgress(0);
        }

        plant.gameObject.SetActive(true);
        UpdatePlant();

        plantAudio.PlayOneShot(plantingSound);

    }
    void Harvest()
    {
        isPlanted = false;
        plant.gameObject.SetActive(false);
        gm.Transaction(selectedPlant.sellPrice);
        Debug.Log("Harvested!");
        plantAudio.PlayOneShot(harventSound);
        /*if (soldText != null)
        {
            soldText.text = "Sold: " + selectedPlant.name;
            //StartCoroutine(FadeText());
        }    */
    }

    public void PlantTakeDamage(float damage, EnemyAnimalChase attackingAnimal)
    {
        currentPlantHealth -= damage;

        plantSliderBars.SetHealth(currentPlantHealth);

        if(currentPlantHealth <= 0)
        {
            PlantDeath(attackingAnimal);
        }
    }

    public void PlantDeath(EnemyAnimalChase attackingAnimal)
    {
        if (attackingAnimal != null)
        {
            attackingAnimal.TargetDestroyed();
        }
        isPlanted = false;
        plant.gameObject.SetActive(false);

        if(plantSliderBars != null)
        {
            plantSliderBars.SetHealth(0);
            plantSliderBars.gameObject.SetActive(false);
        }
    }

   /* private IEnumerator FadeText()
    {
        soldText.alpha = 1f;
        yield return new WaitForSeconds(2f);

        while (soldText.alpha > 0)
        {
           soldText.alpha -= Time.deltaTime;
            yield return null;
        }
    } */
        void UpdatePlant()
    {
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

        currentPlantProgress = plantStage;

        Debug.Log("Day passed and Calling Photosynthesis with CO2 removing factor amount: " + selectedPlant.CO2RemovingFactor);

        sliderController.Photosynthesis(selectedPlant.CO2RemovingFactor);

        --daysRemaining;
        
        if (daysRemaining <= 0 && plantStage < selectedPlant.plantStages.Length - 1)
        {
            plantStage++;
            UpdatePlant();
            daysRemaining = selectedPlant.daysBetweenStages;
            
            currentPlantProgress = plantStage;
            if (plantSliderBars != null)
            {
                plantSliderBars.SetCurrentProgress(currentPlantProgress);
            }
        }
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
