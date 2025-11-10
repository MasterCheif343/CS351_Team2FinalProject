using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarbagePickUP : MonoBehaviour
{
    private AudioSource playerAudio;
    public AudioClip pickUpSound;

    bool active = true;
    // Start is called before the first frame update
    void Start()
    {
        playerAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(active && collision.gameObject.tag == "Player")
        {
        active = false;

        ProgressManager.score++;

        playerAudio.PlayOneShot(pickUpSound, 1.0f);

        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = false;

         Destroy(gameObject, 2.0f);
        }
    }
}
