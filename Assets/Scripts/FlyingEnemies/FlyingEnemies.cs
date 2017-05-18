using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemies : MonoBehaviour
{
    public float moveSpeed;
    public float playerRange;
    public LayerMask playerLayer;
    public int damage;
    public int startingHealth = 2;
    public int currentHealth;
    

    public bool playerInRange;
    bool damaged;
    bool isDead;

    PlayerMovement playerMovement;
    GameObject player;
    PlayerHealth playerHealth;
   

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
        playerMovement = player.GetComponent<PlayerMovement>();

        currentHealth = startingHealth;
    }

    void Update()
    {
        playerInRange = Physics2D.OverlapCircle(transform.position, playerRange, playerLayer);

        if (playerInRange)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);
        }
        
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, playerRange);
    }

    void OnCollisionEnter2D(Collision2D other)
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

            if (playerHealth.currentHealth > 0)
            {
                playerHealth.TakeDamage(damage);
            }
        }
    }

    public void TakeDamage(int amount)
    {
        damaged = true;

        currentHealth -= amount;

        gameObject.GetComponentInChildren<Animator>().Play("FlyingDamage");

        if (currentHealth <= 0 && !isDead)
        {
            Death();
        }
    }

    void Death()
    {
        gameObject.SetActive(false);
    }
}
