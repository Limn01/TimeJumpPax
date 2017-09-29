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
    bool stuckToPlatform = false;
    float gravity;

    float maxJumpHeight = 6;
    float minJumpHeight = 1;
    float timeToJumpApex = .4f;
    float accerlerationTimeAirborne = .2f;
    float accelerationTimeGrounded = .1f;
    float maxJumpVelocity;
    float minJumpVelocity;

    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        //gameObject.transform.parent = null;
    }

    private void Start()
    {
        gravity = -(2 * maxJumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        maxJumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
        minJumpVelocity = Mathf.Sqrt(2 * Mathf.Abs(gravity) * minJumpVelocity);
    }

    private void FixedUpdate()
    {
        hittingWall = Physics2D.OverlapCircle(wallCheck.position, wallCheckRadius, whatIsWall);

        notAtEdge = Physics2D.OverlapCircle(edgeCheck.position, wallCheckRadius, whatIsWall);

        rb2d.AddForce(Vector2.down * -gravity * Time.deltaTime, ForceMode2D.Impulse);

        Movement();
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
            if (gameObject.activeInHierarchy)
            {
                transform.parent = other.transform;
            }
        }

        if (other.gameObject.tag == "Ground")
        {
            transform.parent = null;
        }
    }

    void Movement()
    {
        if (hittingWall || !notAtEdge)
        {
            moveRight = !moveRight;
        }

        //if (hittingWall)
        //{
        //    rb2d.velocity = new Vector2(rb2d.velocity.x, -moveSpeed);
        //    //transform.rotation = Quaternion.Euler(0,0,90);
        //    //rb2d.AddForce(Vector2.right, ForceMode2D.Force);
        //    rb2d.gravityScale = 0;
        //
        //   // moveRight = false;
        //}

        //if (!notAtEdge)
        //{
        //    //transform.localScale = new Vector3(1, 1, 1);
        //    transform.rotation = Quaternion.Euler(0,0,90);
        //    rb2d.velocity = new Vector2(rb2d.velocity.x, -moveSpeed);
        //    //rb2d.gravityScale = 10;
        //}

        if (moveRight)
        {
            //rb2d.gravityScale = -10;
            rb2d.velocity = new Vector2(moveSpeed, rb2d.velocity.y);
            transform.localScale = new Vector3(-1, 1, 1);
            // transform.rotation = Quaternion.Euler(0,0,90);
        }
        else
        {
            if (!moveRight || !hittingWall && !notAtEdge)
            {
                transform.localScale = new Vector3(1, 1, 1);
                rb2d.velocity = new Vector2(-moveSpeed, rb2d.velocity.y);
                //enemyCloneSprite.transform.rotation = Quaternion.Euler(0, 0, 0);

                // rb2d.gravityScale = -10;
            }
        }
    }   
}
