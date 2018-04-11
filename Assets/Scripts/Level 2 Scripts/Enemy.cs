using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float damage = 1;
    public float startingHealth = 1;
    public float currentHealth;

    bool playerInRange;

    bool isHit;
    bool isDead;

    protected GameObject player;
    protected PlayerHealth playerHealth;
    protected Player playerMovement;

    protected virtual void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
        playerMovement = player.GetComponent<Player>();

        currentHealth = startingHealth;
        isDead = false;
    }

    protected virtual void Update()
    {
        if (playerInRange)
        {
            if (!playerHealth.invinc)
            {
                Attack();
            }
        }

        isHit = false;
    }

    public void TakeDamage(float amount)
    {
        isHit = true;

        if (isDead)
        {
            return;
        }

        currentHealth -= amount;

        if (currentHealth <= 0 && !isDead)
        {
            Death();
        }
    }

    void Death()
    {
        isDead = true;

        gameObject.SetActive(false);
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 8)
        {
            playerInRange = true;
        }
    }

    protected virtual void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == 8)
        {
            playerInRange = false;
        }
    }

    protected void Attack()
    {
        if (playerHealth.CurrentHealth > 0)
        {
            playerHealth.TakeDamage(damage);
        }

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
