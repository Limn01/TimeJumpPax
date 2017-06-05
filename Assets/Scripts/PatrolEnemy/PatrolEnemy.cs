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
    [SerializeField]
    LayerMask playerLayer;
    [SerializeField]
    GameObject FireParticle;
    [SerializeField]
    ParticleSystem fire;

    float runningSpeed = 70;
    bool hittingWall;
    bool notAtEdge;
    bool movingRight;
    bool moving = true;
    EnemyStates enemyStates;
    RaycastHit2D hit;
    Rigidbody2D rb2d;
    float force = 50f;
    Animator anim;
    

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        

        anim.SetBool("Patrolling", true);
    }

    void Update()
    {
        State();

        hit = Physics2D.Raycast(transform.position, transform.localScale.x * Vector3.left, rayDistance, playerLayer);
        Debug.DrawRay(transform.position, transform.localScale.x * Vector3.left * rayDistance); 

        if (hit.collider != null && hit.collider.tag == "Player")
        {
            enemyStates = EnemyStates.Attacking;
        }
        else
        {
            enemyStates = EnemyStates.Partolling;
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
                anim.SetBool("Attacking", false);
                FireParticle.SetActive(false);
                fire.Play();
                break;

            case EnemyStates.Attacking:
                HandleAttack();
                anim.SetBool("Attacking", true);
                FireParticle.SetActive(true);
                
                Debug.Log("Attacking");
                break;

        }
    }

    IEnumerator WaitTime()
    {
        moving = false;
        anim.SetBool("Patrolling", false);
        moveSpeed = 0;
        yield return new WaitForSeconds(patrolDelay);
        moving = true;
        anim.SetBool("Patrolling", true);
        moveSpeed = 60;
    }

    void HandleAttack()
    {
        float dist = Vector2.Distance(transform.position, player.position);

        if (dist > rayDistance)
        {
            rb2d.AddForce(Vector3.up * force + (hit.collider.transform.position - transform.position) * force);
        }
        
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(wallCheck.position, wallCheckRadius);
        Gizmos.DrawSphere(edgeCheck.position, wallCheckRadius);
    }
}
