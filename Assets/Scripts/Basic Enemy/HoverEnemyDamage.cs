using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverEnemyDamage : MonoBehaviour
{
    public float damage;
    public float timeBetweenAttack;

    GameObject player;
    PlayerHealth playerHealth;
    Player playerMovement;
    float timer;

    bool playerInRange;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
        playerMovement = player.GetComponent<Player>();

    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject == player)
        {
            playerInRange = true;

            playerMovement.knockBackCount = playerMovement.knockBackLength;

            if (other.transform.position.x < transform.position.x)
            {
                playerMovement.knockBackFromRight = true;
            }
            else
            {
                playerMovement.knockBackFromRight = false;
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

        if (playerHealth.CurrentHealth > 0)
        {
            playerHealth.TakeDamage(damage);
        }
    }
}


