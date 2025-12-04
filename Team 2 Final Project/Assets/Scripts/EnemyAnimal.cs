using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimal : MonoBehaviour
{
    public Transform target;

    public float speed = 3f;

    public Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

    }
    private void Update()
    {
        //Get target 
        if (!target)
        {
            GetTarget();
        }
        
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        //move forwads
    }

    private void GetTarget()
    {
        target = GameObject.FindGameObjectWithTag("Plant").transform;
    }
}
