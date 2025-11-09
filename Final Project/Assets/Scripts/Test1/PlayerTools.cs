using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTools : MonoBehaviour
{
    playerMovement movement;
    Rigidbody2D rigidbody2;
    [SerializeField] float offSetDist = 1f;
    [SerializeField] float sizeofInteractableArea = 1.2f;

    private void Awake()
    {
        movement = GetComponent<playerMovement>();
        rigidbody2 = GetComponent<Rigidbody2D>();

    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            UseTool();
        }
    }

    private void UseTool()
    {
        Vector2 position = rigidbody2.position + movement.lastMotionVector * offSetDist;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, sizeofInteractableArea);

        foreach (Collider2D c in colliders)
        {
            ToolHit hit = c.GetComponent<ToolHit>();
            if (hit != null)
            {
                hit.Hit();
                break;
            }
        }
    }
}
