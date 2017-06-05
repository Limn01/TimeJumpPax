using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolEnemy : MonoBehaviour
{
    enum EnemyStates { Partolling, Attacking};

    [SerializeField]
    float moveSpeed;
    [SerializeField]
    float rayDistance;
    [SerializeField]
    Transform player;
    [SerializeField]
    Transform wallCheck;
    [SerializeField]
    Transform edgeCheck;
    [SerializeField]
    float wallCheckRadius;
    [SerializeField]
    LayerMask whatIsWall;
    [SerializeField]
    float timer = 0.5f;
    [SerializeField]
    float patrolDelay;

    bool hittingWall;
    bool notAtEdge;
    bool movingRight;
    bool moving = true;
    EnemyStates enemyStates;
    RaycastHit2D hit;
    Rigidbody2D rb2d;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        State();

        hit = Physics2D.Raycast(transform.position, transform.localScale.x * Vector3.left, rayDistance);
        Debug.DrawRay(transform.position, transform.localScale.x * Vector3.left * rayDistance); 

        if (hit.collider != null && hit.collider.tag == "Player")
        {
            HandleAttack();
            Debug.Log("Attacking");
        }

    }

    void Movement()
    {
        hittingWall = Physics2D.OverlapCircle(wallCheck.position, wallCheckRadius, whatIsWall);
        notAtEdge = Physics2D.OverlapCircle(edgeCheck.position, wallCheckRadius, whatIsWall);

        
        if (hittingWall || !notAtEdge)
        {
            movingRight = !movingRight;
            StartCoroutine(WaitTime());
        }

        if (movingRight)
        {
            
            rb2d.velocity = new Vector2(moveSpeed, rb2d.velocity.y);
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
           
            rb2d.velocity = new Vector2(-moveSpeed, rb2d.velocity.y);
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

    void State()
    {
        switch (enemyStates)
        {
            case EnemyStates.Partolling:
                Movement();
                
                Debug.Log("Moving");
                break;

            case EnemyStates.Attacking:
                HandleAttack();
                Debug.Log("Attacking");
                break;

        }
    }

    IEnumerator WaitTime()
    {
        moving = false;
        moveSpeed = 0;
        yield return new WaitForSeconds(patrolDelay);
        moving = true;
        moveSpeed = 60;
    }

    void HandleAttack()
    {
        float dist = Vector2.Distance(transform.position, player.position);

        if (dist < rayDistance)
        {
            rb2d.velocity = new Vector2(hit.collider.transform.position.x - transform.position.x, 0);
        }
        
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
       // Gizmos.DrawLine(transform.position, transform.position + transform.localScale.x * Vector3.left * rayDistance);
        Gizmos.DrawSphere(wallCheck.position, wallCheckRadius);
        Gizmos.DrawSphere(edgeCheck.position, wallCheckRadius);
    }
}
