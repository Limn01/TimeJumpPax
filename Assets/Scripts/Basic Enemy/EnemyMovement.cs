using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : Enemy
{
    public float moveSpeed;
    public bool moveRight;

    public Transform wallCheck;
    public float wallCheckRadius;
    public LayerMask whatIsWall;
    public Transform edgeCheck;
    public float rayDistance;
    public LayerMask groundCollision;

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

    protected override void Awake()
    {
        base.Awake();
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
       // gravity = -(2 * maxJumpHeight) / Mathf.Pow(timeToJumpApex, 2);
       // maxJumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
       // minJumpVelocity = Mathf.Sqrt(2 * Mathf.Abs(gravity) * minJumpVelocity);
    }

    protected override void Update()
    {
        base.Update();
        StickToGround();
    }

    private void FixedUpdate()
    {
        hittingWall = Physics2D.OverlapCircle(wallCheck.position, wallCheckRadius, whatIsWall);

        notAtEdge = Physics2D.OverlapCircle(edgeCheck.position, wallCheckRadius, whatIsWall);

        Movement();
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawSphere(wallCheck.position, wallCheckRadius);
        Gizmos.DrawSphere(edgeCheck.position, wallCheckRadius);
    }

    void Movement()
    {
        if (hittingWall || !notAtEdge)
        {
            moveRight = !moveRight;
        }

        if (moveRight)
        {
            rb2d.velocity = new Vector2(moveSpeed, rb2d.velocity.y);
            transform.localScale = new Vector3(-1.5f, 1.5f, 1.5f);
        }
        else
        {
            transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
            rb2d.velocity = new Vector2(-moveSpeed, rb2d.velocity.y);
        }
    }
    
    void StickToGround()
    {
        Vector3 pos = transform.position;
        Vector3 down = -transform.up;
        Vector3 size = transform.localScale;

        Vector3 halfSize = size * 0.5f;

        Vector3 rayFromScale = pos + down * halfSize.y;

        Ray ray = new Ray(rayFromScale, down);

        Debug.DrawLine(ray.origin, ray.origin + ray.direction * rayDistance, Color.magenta);

        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, rayDistance, groundCollision);

        if (hit)
        {
            Vector2 targetLocation = hit.point;

            targetLocation += new Vector2(0, size.y / 2);

            transform.position = targetLocation;
        }
    }   
}
