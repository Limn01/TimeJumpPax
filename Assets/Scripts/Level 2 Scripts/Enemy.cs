using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float damage = 1;
    public float startingHealth = 1;
    public float currentHealth;

    int playerLayerIndex;

    bool targetInRange;
    bool isHit;
    bool isDead;
    bool IsOnscreen;

    protected GameObject player;
    protected PlayerHealth playerHealth;
    protected Player playerMovement;

    protected virtual void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
        playerMovement = player.GetComponent<Player>();
        playerLayerIndex = LayerMask.NameToLayer("Player");

        currentHealth = startingHealth;
        isDead = false;
    }

    protected virtual void Update()
    {
        if (targetInRange)
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

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == playerLayerIndex)
        {
            targetInRange = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == playerLayerIndex)
        {
            targetInRange = false;
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
