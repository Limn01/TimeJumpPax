using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSliding : MonoBehaviour
{
    public float distance;
    public float speed = 2;
    PlayerMovement playerMovement;

    bool wallJumping;

    void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        Physics2D.queriesStartInColliders = false;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right * transform.localScale.x, distance);

        if (!playerMovement.grounded && hit.collider != null)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(speed * hit.normal.x, speed);
            playerMovement.moveSpeed = speed * hit.normal.x;
            wallJumping = true;
            transform.localScale = transform.localScale.x == 1 ? new Vector2(-1, 1) : Vector2.one;
        }
        else if (hit.collider != null && wallJumping)
        {
            wallJumping = false;
        }
        
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (!wallJumping || playerMovement.grounded)
        {
            playerMovement.moveSpeed = playerMovement.moveSpeed;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position,transform.position + Vector3.right * transform.localScale.x * distance);
    }
}
