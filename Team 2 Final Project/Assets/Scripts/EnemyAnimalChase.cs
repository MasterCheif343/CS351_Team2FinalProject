/* Adam Krenek
 * Final Game Project
 * This script manages the animal's hunger for plants
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Profiling;

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

    public float stopDistance = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();

        rb = GetComponent<Rigidbody2D>();

        anim = GetComponent<Animator>();

        FindClosestPlot();

    }
     void Update()
    {   
        if(plotTransform == null)
        {
            FindClosestPlot();
            return;
        }

      float distance = Vector2.Distance(transform.position, plotTransform.position);
        if (distance <= stopDistance)
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            anim.SetBool("isMoving", false);
            return;
        }

        if (distance <= chaseRange)
        {
            Vector2 direction = (plotTransform.position - transform.position).normalized;
            direction.y = 0;

            rb.velocity = new Vector2(direction.x * speed, rb.velocity.y);
            anim.SetBool("isMoving", true);
            FaceTarget(direction);
        }
        else
        {
            anim.SetBool("isMoving", false);
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
    }

    public void TargetDestroyed()
    {
        plotTransform = null;

        FindClosestPlot();

        rb.velocity = Vector2.zero;

        anim.SetBool("isMoving", false);
    }
    void FindClosestPlot()
    {
        GameObject[] plots = GameObject.FindGameObjectsWithTag("Plant");
        if (plots.Length == 0)
        {
            plotTransform = null;
            return;
        }

        float closestPlot = Mathf.Infinity;

        foreach (GameObject plot in plots)
        {
            float dist = Vector2.Distance(transform.position, plot.transform.position);
            if (dist < closestPlot)
            {
                closestPlot = dist;
                plotTransform = plot.transform;
            }
        }
    }
    void FaceTarget(Vector2 direction)
    {
        sr.flipX = (direction.x > 0);

    }

    
}
