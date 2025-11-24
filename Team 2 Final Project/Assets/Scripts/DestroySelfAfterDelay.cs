using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySelfAfterDelay : MonoBehaviour
{
    //The delay before the gameobject is destoryed
    public float delay = 2f;

    // Start is called before the first frame update
    void Start()
    {
     
        //Destroy the game object after 2 seconds 
        Destroy(gameObject, delay);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
