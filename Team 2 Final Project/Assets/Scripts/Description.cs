using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Description : MonoBehaviour
{
    [Header("UI Settings")]
    public TMP_Text descriptionText;

    [TextArea]
    public string message = "Hello! You are hovering over me.";
    // Start is called before the first frame update
    void Start()
    {
        descriptionText.gameObject.SetActive(false);
    }
    private void OnMouseOver()
    {
        descriptionText.text = message;
        descriptionText.gameObject.SetActive(true);
        
    }

    // Called when the mouse leaves the collider
    private void OnMouseExit()
    {
        
            descriptionText.gameObject.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
