/* Adam Krenek
 * FinalGameProject
 * This script lets us creat plant objects
 * Allows for different plants to be easily implemented in the store and in the game
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Plant" , menuName = "Plant")]
public class PlantObject : ScriptableObject
{

    public string plantName;
    public Sprite[] plantStages;
    public int daysBetweenStages;
    public int buyPrice;
    public int sellPrice;
    public Sprite icon;
    public  float CO2RemovingFactor;
    //put sprite of dry plot here
    //public Sprite dryPlanted;
}
