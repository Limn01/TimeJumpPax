using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class PatrolEnemy : MonoBehaviour
{
    float moveSpeed;
    [SerializeField]
    float waitTime;
    [SerializeField]
    float force;
    [SerializeField]
    float rayLength;
    [SerializeField]
    LayerMask playerLayer;
    [SerializeField]
    float timer;
    [SerializeField]
    float delayAttack;
    [SerializeField]
    GameObject Fire;
    [SerializeField]
    Transform groundCheck;
    [SerializeField]
    LayerMask whatIsGround;


    const float groundCheckRadius = .2f;

    [SerializeField]
    float attackTimer;
    int currentPoint;

    float attackDamage = 2;

    bool playerInRange;
    public bool grounded;

    Animator anim;
    GameObject player;
    GameObject patrolEnemy;
    PlayerHealth playerHealth;
    Player playerMovement;
    RaycastHit2D hit;
    Rigidbody2D rb2d;
    float gravity;
    float maxJumpHeight = 6;
    float minJumpHeight = 1;
    float timeToJumpApex = .4f;
    float accerlerationTimeAirborne = .2f;
    float accelerationTimeGrounded = .1f;
    float maxJumpVelocity;
    float minJumpVelocity;

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
        playerMovement = player.GetComponent<Player>();
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        gravity = -(2 * maxJumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        maxJumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
        minJumpVelocity = Mathf.Sqrt(2 * Mathf.Abs(gravity) * minJumpVelocity);
    }

    private void FixedUpdate()
    {
        grounded = false;

        grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);

        rb2d.AddForce(Vector2.down * -gravity * Time.deltaTime, ForceMode2D.Impulse);
    }

    private void Update()
    {
        hit = Physics2D.Raycast(transform.position, transform.localScale.x * Vector2.left, rayLength, playerLayer);

        if (hit.collider != null && hit.collider.tag == "Player")
        {
            anim.SetBool("Attacking", true);
            Fire.SetActive(true);
         
            rb2d.AddForce(Vector3.up * force + (hit.collider.transform.position - transform.position) * force);
        }

        if (hit.collider == null)
        {
            Fire.SetActive(false);
            anim.SetBool("Attacking", false);
        }

        attackTimer += Time.deltaTime;

        if (attackTimer >= delayAttack && playerInRange)
        {
            Attack();
        }

        if (!grounded)
        {
            Invoke("Destroy", .5f);
        }

        if (grounded)
        {
            CancelInvoke();
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject == player)
        {
            playerMovement.knockBackCount = playerMovement.knockBackLength;

            if (player.transform.position.x < transform.position.x)
            {
                playerMovement.knockBackFromRight = true;
            }
            else
            {
                playerMovement.knockBackFromRight = false;
            }
        }
    }

    //private void OnCollisionExit2D(Collision2D other)
    //{
    //    if (other.gameObject == player)
    //    {
    //        playerInRange = false;
    //    }
    //}

    //IEnumerator Patrol()
    //{
    //    //Debug.Log("Started");
    //
    //        while (true)
    //        {
    //            if (transform.position.x == waypoints[currentPoint].position.x)
    //            {
    //
    //                currentPoint++;
    //                anim.SetBool("Patrolling", false);
    //                yield return new WaitForSeconds(waitTime);
    //                anim.SetBool("Patrolling", true);
    //            }
    //
    //            if (currentPoint >= waypoints.Length)
    //            {
    //                currentPoint = 0;
    //            }
    //
    //            transform.position = Vector2.MoveTowards(transform.position, new Vector2(waypoints//[currentPoint].position.x, transform.position.y), moveSpeed * Time.deltaTime);
    //
    //            if (transform.position.x < waypoints[currentPoint].position.x)
    //            {
    //                transform.localScale = new Vector3(-1, 1, 1);
    //            }
    //            else if (transform.position.x > waypoints[currentPoint].position.x)
    //            {
    //                transform.localScale = Vector3.one;
    //            }
    //
    //
    //            yield return null;
    //        }
    //    
    //
    //    
    //    
    //}

    void Attack()
    {
        attackTimer = 0;

        if (playerHealth.CurrentHealth > 0)
        {
            playerHealth.TakeDamage(attackDamage);
        }
    }

    private void Destroy()
    {
        gameObject.SetActive(false);
    }


    private void OnDisable()
    {
        CancelInvoke();
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + transform.localScale.x * Vector3.left * rayLength);
    }
}
