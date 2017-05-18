using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverEnemyDamage : MonoBehaviour
{
    public int damage;
    public float timeBetweenAttack;

    GameObject player;
    PlayerHealth playerHealth;
    PlayerMovement playerMovement;
    float timer;
    
    bool playerInRange;

	void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
        playerMovement = player.GetComponent<PlayerMovement>();
       
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject == player)
        {
            playerInRange = true;

            playerMovement.knockbackCount = playerMovement.knockbackLength;

            if (other.transform.position.x < transform.position.x)
            {
                playerMovement.knockbackFromRight = true;
            }
            else
            {
                playerMovement.knockbackFromRight = false;
            }
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject == player)
        {
            playerInRange = false;
        }
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= timeBetweenAttack && playerInRange)
        {
            Attack();
        }
    }

    void Attack()
    {
        timer = 0;

        if (playerHealth.currentHealth > 0)
        {
            playerHealth.TakeDamage(damage);
        }
    }
}
