using System.Collections;
using System.Collections.Generic;
using Com.LuisPedroFonseca.ProCamera2D;
using UnityEngine;


public class PatrolEnemy : MonoBehaviour
{
    public float moveSpeed;
        
    [SerializeField]
    float rayDistance;
    [SerializeField]
    Transform playerTrans;
    [SerializeField]
    float patrolDelay;
    [SerializeField]
    LayerMask playerLayer;
    [SerializeField]
    GameObject FireParticle;
    [SerializeField]
    ParticleSystem fire;
    [SerializeField]
    Collider2D attackTrigger;
    [SerializeField]
    float waitTime;
    [SerializeField]
    GameObject chargeParticle;
    [SerializeField]
    ParticleSystem charge;
    [SerializeField]
    Transform wallCheck;
    [SerializeField]
    Transform edgeCheck;
    [SerializeField]
    float wallCheckRadius;
    [SerializeField]
    LayerMask whatIsWall;


    float runningSpeed = 70;
    [SerializeField]
    float attackTimer;
    [SerializeField]
    float delayBoost;
    [SerializeField]
    float attackDelay;
    float attackDamage = 3f;
    float force = 150f;
    [SerializeField]
    float timer;

    bool hittingWall;
    bool notAtEdge;
    bool movingRight;
    bool moving = true;
    bool attacking;
    bool playerInRange;

    RaycastHit2D hit;
    Rigidbody2D rb2d;
    [HideInInspector]
    public Animator anim;
   
    GameObject player;
    PlayerHealth playerHealth;
    PlayerMovement playerMovement;


    private void Awake()
        {
            player = GameObject.FindGameObjectWithTag("Player");
            playerMovement = player.GetComponent<PlayerMovement>();
            playerHealth = player.GetComponent<PlayerHealth>();
            rb2d = GetComponent<Rigidbody2D>();
            anim = GetComponentInChildren<Animator>();
        //currentState = GetComponent<IEnemyState>();
        
          
        }

        void Update()
        {

        Movement();

        hit = Physics2D.Raycast(transform.position, transform.localScale.x * Vector3.left, rayDistance, playerLayer);
        Debug.DrawRay(transform.position, transform.localScale.x * Vector3.left * rayDistance);

        if (hit.collider != null && hit.collider.tag == "Player")
        {
            
            moveSpeed = 0;
            timer += Time.deltaTime;

            if (timer >= delayBoost)
            {
                HandleBoost();
                
            }
            
            
        }

        //if (hit.collider != player)
        //{
        //    moveSpeed = 60;
        //}

        attackTimer += Time.deltaTime;

        if (attackTimer >= attackDelay && playerInRange)
        {
            //timer = 0;
            HandleAttack();
        }

        
    }

     void Movement()
        {
        if (!attacking)
        {
            
            moving = true;
            hittingWall = Physics2D.OverlapCircle(wallCheck.position, wallCheckRadius, whatIsWall);
            notAtEdge = Physics2D.OverlapCircle(edgeCheck.position, wallCheckRadius, whatIsWall);

            if (hittingWall || !notAtEdge)
            {
                movingRight = !movingRight;
                StartCoroutine("Patrol");
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


        }

        void HandleBoost()
        {
        //moving = false;
        //float dis = Vector2.Distance(transform.position, player.transform.position);

        //if (dis < rayDistance)
        //{
            rb2d.AddForce(Vector3.up * force + (hit.collider.transform.position - transform.position) * force);
        //}
        
        
        }

        void HandleAttack()
        {
            attackTimer = 0;
       
            if (playerHealth.CurrentHealth > 0)
            {
                playerHealth.TakeDamage(attackDamage);
            }
        }

    IEnumerator Patrol()
    {
        moving = false;
        moveSpeed = 0;

        yield return new WaitForSeconds(patrolDelay);
        moveSpeed = 60;
        moving = true;
        
    }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject == player)
            {
                playerMovement.knockbackCount = playerMovement.knockbackLength;

                if (player.transform.position.x < transform.position.x)
                {
                    playerMovement.knockbackFromRight = true;
                   
                }
                else
                {
                    playerMovement.knockbackFromRight = false;
                    
                }

                if (playerHealth.CurrentHealth > 0)
                {
                    HandleAttack();
                }
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject == player)
            {
                playerInRange = true;
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject == player)
            {
                playerInRange = false;
            }
        }

        void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(wallCheck.position, wallCheckRadius);
            Gizmos.DrawSphere(edgeCheck.position, wallCheckRadius);
        }
    }



