using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    Transform player;
    [SerializeField] float speed = 5f;
    [SerializeField] float pickUpDist = 1.5f;
    [SerializeField] float timeToLeave = 10f;
  
    private void Awake()
    {
        player = GameManager.instance.player.transform;
    }

    // Update is called once per frame
    void Update()
    {
        timeToLeave -= Time.deltaTime;
        if(timeToLeave < 0)
        {
            Destroy(gameObject);
        }
        float distance = Vector3.Distance(transform.position, player.position);
        if(distance > pickUpDist)
        {
            return;
        }
        transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        if(distance < 0.1f)
        {
            Destroy(gameObject);
        }
    }
}
