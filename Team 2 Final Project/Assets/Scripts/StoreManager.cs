/* Adam Krenek
 * FinalGameProject
 * This manages the in-game store
 * players can buy seeds and services
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreManager : MonoBehaviour
{
    public GameObject plantItem;
    List<PlantObject> plantObjects = new List<PlantObject>();

     private void Awake()
    {
        //Doesn't access Plants in Asset folder, goes through Assets -> Resources -> Plants
        var loadPlants = Resources.LoadAll("Plants", typeof(PlantObject));
        foreach(var plant in loadPlants)
        {
            plantObjects.Add((PlantObject)plant);

        }
        plantObjects.Sort(SortPrices);

        foreach(var plant in plantObjects)
        {
            PlantItem newPlant = Instantiate(plantItem, transform).GetComponent<PlantItem>();
            newPlant.plant = plant;
        }
    }

    int SortPrices(PlantObject plantOb1, PlantObject plantOb2)
    {
        //sorts plants in shop by price
        return plantOb1.buyPrice.CompareTo(plantOb2.buyPrice);
    }
}
