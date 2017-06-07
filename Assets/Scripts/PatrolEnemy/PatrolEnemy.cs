using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.LuisPedroFonseca.ProCamera2D
{
    public class PatrolEnemy : MonoBehaviour
    {
        enum EnemyStates { Partolling, Attacking, Stop };
        enum IdleState { Idle};

        [SerializeField]
        float moveSpeed;
        [SerializeField]
        float rayDistance;
        [SerializeField]
        Transform playerTrans;
        [SerializeField]
        Transform wallCheck;
        [SerializeField]
        Transform edgeCheck;
        [SerializeField]
        float wallCheckRadius;
        [SerializeField]
        LayerMask whatIsWall;
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

        float runningSpeed = 70;
        [SerializeField]
        float attackTimer;
        [SerializeField]
        float attackDelay;
        float attackDamage = 3f;
        float force = 30f;
        [SerializeField]
        float timer;

        bool hittingWall;
        bool notAtEdge;
        bool movingRight;
        bool moving = true;
        bool attacking = false;
        bool playerInRange;

        EnemyStates enemyStates;
        IdleState idleState;
        RaycastHit2D hit;
        Rigidbody2D rb2d;
        Animator anim;
        [SerializeField]
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

           
            attackTrigger.enabled = false;
            anim.SetBool("Patrolling", true);
        }

        void Update()
        {
            State();
            Idle();

            hit = Physics2D.Raycast(transform.position, transform.localScale.x * Vector3.left, rayDistance, playerLayer);
            Debug.DrawRay(transform.position, transform.localScale.x * Vector3.left * rayDistance);

            if (hit.collider != null && hit.collider.tag == "Player")
            {
                //moveSpeed = 0;
                // moving = false;
                idleState = IdleState.Idle;
                anim.SetBool("Patrolling", false);
                chargeParticle.SetActive(true);

                timer += Time.deltaTime;

                if (timer >= waitTime)
                {
                    chargeParticle.SetActive(false);
                    enemyStates = EnemyStates.Attacking;
                    timer = 0;
                    
                }
            }

            else
            {
                if (hit.collider == null)
                {
                    timer = 0;
                    moving = true;
                    moveSpeed = 60;
                   // State();
                    enemyStates = EnemyStates.Partolling;
                   // StartCoroutine(WaitTime());
                }
                
            }

            attackTimer += Time.deltaTime;

            if (attackTimer >= attackDelay && playerInRange)
            {
                HandleAttack();
                //moveSpeed = 60;
            }
        }

        void Movement()
        {
            moving = true;
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
                    //moveSpeed = 60;
                    Debug.Log("Moving");
                    anim.SetBool("Attacking", false);
                    //anim.SetBool("Patrolling", true);
                    FireParticle.SetActive(false);
                   // fire.Play();
                    chargeParticle.SetActive(false);
                    attacking = false;
                    moving = true;
                    break;

                case EnemyStates.Attacking:
                    HandleBoost();
                    chargeParticle.SetActive(false);
                    
                    anim.SetBool("Attacking", true);
                    FireParticle.SetActive(true);
                   
                    attacking = true;
                    Debug.Log("Attacking");
                    moving = false;
                    break;

                //case EnemyStates.Stop:
                //    Debug.Log("Stop");
                //    Stop();
                //    moving = false;
                //    attacking = false;
                //    anim.SetBool("Patrolling", false);
                //    //chargeParticle.SetActive(true);
                //    //StopCoroutine(WaitTime());
                //   // charge.Play();
                //    
                //    break;
            }

           
        }

        void Idle()
        {
            switch (idleState)
            {
                case IdleState.Idle:
                    moving = false;
                    moveSpeed = 0;
                    break;
            }
        }

        IEnumerator WaitTime()
        {
            moving = false;
            Idle();

            yield return new WaitForSeconds(patrolDelay);
            moving = true;
            enemyStates = EnemyStates.Partolling;
           
        }

        void HandleBoost()
        {
            //float dist = Vector2.Distance(transform.position, playerTrans.position);
            
            //if (dist < rayDistance)
            //{
                Debug.Log(rb2d);
              rb2d.AddForce(Vector3.up * force + (hit.collider.transform.position - transform.position)*force);
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

        void Stop()
        {
            moving = false;
            rb2d.velocity = new Vector2(0, rb2d.velocity.y);
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
}


