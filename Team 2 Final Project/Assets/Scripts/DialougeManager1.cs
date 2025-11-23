/* Adam Krenek
 * Final Game Project
 * This script manages the panel that shows up when the game is first launched
 * will tell the player the story and how things work
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialougeManager1 : MonoBehaviour
{
    public TMP_Text textbox;
    public string[] sentences;
    private int index;
    public float typingSpeed;

    public GameObject continueButton;
    public GameObject dialougePannel;
    void OnEnable()
    {
        continueButton.SetActive(false);
        StartCoroutine(Type());
    }


    IEnumerator Type()
    {
        textbox.text = "";

        foreach (char letter in sentences[index])
        {
            textbox.text += letter; ;
            yield return new WaitForSeconds(typingSpeed);
        }
        continueButton.SetActive(true);
    }
    public void NextSentence()
    {
        continueButton.SetActive(false);
        if (index < sentences.Length - 1)
        {
            index++;
            textbox.text = "";
            StartCoroutine(Type());
        }
        else
        {
            textbox.text = "";
            dialougePannel.SetActive(false);
        }
    }

}
