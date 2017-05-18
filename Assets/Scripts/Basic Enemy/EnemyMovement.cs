using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float moveSpeed;
    public bool moveRight;

    public Transform wallCheck;
    public float wallCheckRadius;
    public LayerMask whatIsWall;
    public Transform edgeCheck;

    Rigidbody2D rb2d;
    bool hittingWall;
    bool notAtEdge;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        hittingWall = Physics2D.OverlapCircle(wallCheck.position, wallCheckRadius, whatIsWall);

        notAtEdge = Physics2D.OverlapCircle(edgeCheck.position, wallCheckRadius, whatIsWall);

        if (hittingWall || !notAtEdge)
        {
            moveRight = !moveRight;
        }

        //if (!notAtEdge && !hittingWall)
        //{
        //    rb2d.velocity = new Vector2(rb2d.velocity.x, -moveSpeed);
        //    transform.rotation = Quaternion.Euler(0,0,90);
        //    rb2d.AddForce(-Vector2.right, ForceMode2D.Force);
        //   
        //
        //    moveRight = false;
        //}

        if (moveRight)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            rb2d.velocity = new Vector2(moveSpeed, rb2d.velocity.y);
            moveRight = true;
        }
        else
        {
            if (!moveRight || !hittingWall && !notAtEdge)
            {
              transform.localScale = new Vector3(1, 1, 1);
              rb2d.velocity = new Vector2(-moveSpeed, rb2d.velocity.y);
              //enemyCloneSprite.transform.rotation = Quaternion.Euler(0, 0, 0);

              //rb2d.gravityScale = 10;
            }
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawSphere(wallCheck.position, wallCheckRadius);
        Gizmos.DrawSphere(edgeCheck.position, wallCheckRadius);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "MovingPlatform")
        {
            transform.parent = other.transform;
        }
    }
}
