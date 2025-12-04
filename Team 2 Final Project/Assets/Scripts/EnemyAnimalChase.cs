using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Profiling;
using UnityEditor.Experimental.RestService;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]

public class EnemyAnimalChase : MonoBehaviour
{
    public float chaseRange = 8f;

    public Transform target;

    public float speed = 3f;

    private Rigidbody2D rb;

    private Transform plotTransform;

    private Animator anim;

    private SpriteRenderer sr;
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();

        rb = GetComponent<Rigidbody2D>();

        anim = GetComponent<Animator>();

       GameObject plantObj = GameObject.FindWithTag("Plant");

        if(plantObj != null)
        {
            plotTransform = plantObj.transform;
        }
        else 
        { 
            Debug.LogError("No Object with tag 'Plant' found in scene!"); 
        }

    }
     void Update()
    {   
        if(plotTransform == null)
        {
            return;
        }

        Vector2 plotDirection = plotTransform.position - transform.position;

        float distanceToPlot = plotDirection.magnitude;

        if (distanceToPlot <= chaseRange)
        {
            plotDirection.Normalize();

            plotDirection.y = 0;

            MoveTowardsPlot(plotDirection);
        }

    }
    void MoveTowardsPlot(Vector2 plotDirection)
    {
        rb.velocity = new Vector2(plotDirection.x, rb.velocity.y);

        anim.SetBool("isMoving", true);

    }

    private void FacePlot(Vector2 plotDirection)
    {
        if (plotDirection.x < 0)
        {

            sr.flipX = false;
        }
        else
        {
            sr.flipX = true;
        }
    }
}
