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

        }
        if (gameOver)
        {
            if (won)
            {
              
            }
        }
    }
}
