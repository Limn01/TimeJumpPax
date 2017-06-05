using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.LuisPedroFonseca.ProCamera2D
{
    public class PatrolEnemy : MonoBehaviour
    {
        enum EnemyStates { Partolling, Attacking };

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

        float runningSpeed = 70;
        [SerializeField]
        float attackTimer;
        [SerializeField]
        float attackDelay;
        float attackDamage = 3f;
        float force = 40f;

        bool hittingWall;
        bool notAtEdge;
        bool movingRight;
        bool moving = true;
        bool attacking = false;
        bool playerInRange;

        EnemyStates enemyStates;
        RaycastHit2D hit;
        Rigidbody2D rb2d;
        Animator anim;
        [SerializeField]
        GameObject player;
        PlayerHealth playerHealth;



    private void Awake()
        {
            player = GameObject.FindGameObjectWithTag("Player");
            playerHealth = player.GetComponent<PlayerHealth>();
            rb2d = GetComponent<Rigidbody2D>();
            anim = GetComponentInChildren<Animator>();

            attackTrigger.enabled = false;
            anim.SetBool("Patrolling", true);
        }

        void Update()
        {
            State();

            hit = Physics2D.Raycast(transform.position, transform.localScale.x * Vector3.left, rayDistance, playerLayer);
            Debug.DrawRay(transform.position, transform.localScale.x * Vector3.left * rayDistance);

            if (hit.collider != null && hit.collider.tag == "Player")
            {
                StartCoroutine("AttackDelay");
                //enemyStates = EnemyStates.Attacking;
                attackTrigger.enabled = true;
            }


            else
            {
                enemyStates = EnemyStates.Partolling;
                attackTrigger.enabled = false;
               
            }

            //attackTimer += Time.deltaTime;
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
                    attacking = false;
                    break;

                case EnemyStates.Attacking:
                    HandleAttack();
                    anim.SetBool("Attacking", true);
                    FireParticle.SetActive(true);
                    attacking = true;
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

        IEnumerator AttackDelay()
        {
            enemyStates = EnemyStates.Attacking;
            yield return new WaitForSeconds(attackDelay);
            enemyStates = EnemyStates.Attacking;
        }

        void HandleAttack()
        {
            //float dist = Vector2.Distance(transform.position, playerTrans.position);

            //if (dist < rayDistance)
            //{
                rb2d.AddForce(Vector3.up * force + (hit.collider.transform.position - transform.position) * force);

           // }
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject == player)
            {
                //Invoke("HandleAttack", 3);
                PlayerMovement playerMovement = GameObject.FindObjectOfType<PlayerMovement>();
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
                    playerHealth.TakeDamage(attackDamage);
                    
                }
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


