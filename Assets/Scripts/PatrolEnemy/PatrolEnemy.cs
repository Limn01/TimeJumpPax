using System.Collections;
using System.Collections.Generic;
using Com.LuisPedroFonseca.ProCamera2D;
using UnityEngine;


public class PatrolEnemy : MonoBehaviour
{
  
    public Transform[] waypoints;
    [SerializeField]
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
    bool grounded;

    Animator anim;
    GameObject player;
    GameObject patrolEnemy;
    PlayerHealth playerHealth;
    PlayerMovement playerMovement;
    RaycastHit2D hit;
    Rigidbody2D rb2d;
    WaypointManager waypointManager;

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
        playerMovement = player.GetComponent<PlayerMovement>();
        rb2d = GetComponent<Rigidbody2D>();
        waypointManager = GameObject.FindGameObjectWithTag("_GM").GetComponent<WaypointManager>();

        waypoints = waypointManager.waypoints;
        
        StartCoroutine("Patrol");
        anim.SetBool("Patrolling", true);
    }

    private void FixedUpdate()
    {
        grounded = false;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, groundCheckRadius, whatIsGround);

        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                grounded = true;
                break;
            }
        }
    }

    private void Update()
    {
        hit = Physics2D.Raycast(transform.position, transform.localScale.x * Vector2.left,rayLength, playerLayer);

        if (hit.collider != null && hit.collider.tag == "Player")
        {
            anim.SetBool("Attacking", true);
            Fire.SetActive(true);
            float dist = Vector2.Distance(transform.position, player.transform.position);

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
            Invoke("Destroy", 2);
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
            playerInRange = true;
        }

        playerMovement.knockbackCount = playerMovement.knockbackLength;

        if (player.transform.position.x < transform.position.x)
        {
            playerMovement.knockbackFromRight = true;
        }
        else
        {
            playerMovement.knockbackFromRight = false;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject == player)
        {
            playerInRange = false;
        }
    }

    IEnumerator Patrol()
    {
        Debug.Log("Started");
    
        while (true)
        {
            if (transform.position.x == waypoints[currentPoint].position.x)
            {
                
                currentPoint++;
                anim.SetBool("Patrolling", false);
                yield return new WaitForSeconds(waitTime);
                anim.SetBool("Patrolling", true);
            }
    
            if (currentPoint >= waypoints.Length)
            {
                currentPoint = 0;
            }
    
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(waypoints[currentPoint].position.x, transform.position.y), moveSpeed * Time.deltaTime);
    
            if (transform.position.x < waypoints[currentPoint].position.x)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else if (transform.position.x > waypoints[currentPoint].position.x)
            {
                transform.localScale = Vector3.one;
            }
    
            
            yield return null;
        }
        
    }

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