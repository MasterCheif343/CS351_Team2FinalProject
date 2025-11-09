using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    Rigidbody2D rb;
    Vector2 motionVector;
   public Vector2 lastMotionVector;
    Animator animator;
    [SerializeField] public float speed = 10f;
    public bool isMoving;
    public float horizontalInput;
    public float verticalInput;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        motionVector = new Vector2(horizontalInput, verticalInput );

        animator.SetFloat("horizontal", Input.GetAxisRaw("Horizontal"));
        animator.SetFloat("vertical", Input.GetAxisRaw("Vertical"));
        isMoving = horizontalInput != 0 || verticalInput != 0;
        //animator.SetBool("moving", isMoving);

        if(horizontalInput!=0 || verticalInput != 0)
        {
            lastMotionVector = new Vector2(horizontalInput,verticalInput).normalized;
            animator.SetFloat("lastHorizontal", horizontalInput);
            animator.SetFloat("lastVertical", verticalInput);
        }
    }

    private void FixedUpdate()
    {
        Move();
    }
    private void Move()
    {
       rb.velocity = motionVector * speed;
    }
}
