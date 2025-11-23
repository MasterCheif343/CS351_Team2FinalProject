/* Adam Krenek
 * Final Game Project
 * This script manages the game's status text (money + CO2)
 * and tells when game is over 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static bool gameOver;
    public static bool won;
    public TMP_Text textBox;
    public TMP_Text textBox2;

    public GardenManager gm;
    public SliderController sliderController;

    // Start is called before the first frame update
    void Start()
    {
        gameOver = false;
        won = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameOver)
        {
            textBox.text = "Money: $ " + gm.money;
            textBox2.text = "Current CO2: " + sliderController.CurrentCO2InAir; 
        }
        if (sliderController.CurrentCO2InAir <= 0)
        {
            won = true;
            gameOver = true;
          
        }
        if (gameOver)
        {
            if (won)
            {
                textBox2.text = "You have successfully purified the air! \n Press R to try again!";
            }
            else
            {
                textBox2.text = "The air pollution is overpowering your plants, we have lost... \n Press R to try again!";
            }
            if (Input.GetKeyDown(KeyCode.R)){
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }
}
