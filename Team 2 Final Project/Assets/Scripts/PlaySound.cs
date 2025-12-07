using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//require the audio source component to be attached
//to the GameObject this scipt is attached to
[RequireComponent(typeof(AudioSource))]

public class PlaySound : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip soundToPlay;
    public float volume = 1f;
    // Start is called before the first frame update
    void Start()
    {
        //Set the reference for the audio source
        audioSource = GetComponent<AudioSource>();

        // play the sound on start
        audioSource.PlayOneShot(soundToPlay, volume);
    }

    
}
