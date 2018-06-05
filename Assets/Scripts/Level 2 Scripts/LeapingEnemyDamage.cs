using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeapingEnemyDamage : MonoBehaviour
{
    public float damage = 1;
    public float startingHealth;
    public float currentHealth;

    int playerLayerIndex;

    GameObject player;
    Player playerMovement;
    PlayerHealth playerHealth;

    bool playerInRange;
    bool isHit;
    bool isDead;

    protected virtual void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
        playerMovement = player.GetComponent<Player>();
        playerLayerIndex = LayerMask.NameToLayer("Player");

        currentHealth = startingHealth;
        isDead = false;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer == playerLayerIndex)
        {
            playerInRange = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.layer == playerLayerIndex)
        {
            playerInRange = false;
        }
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
    }

    void Attack()
    {
        if (playerHealth.CurrentHealth > 0)
        {
            playerHealth.TakeDamage(damage);
        }

        playerMovement.knockBackCount = playerMovement.knockBackLength;

        if (transform.position.x < transform.position.x)
        {
            playerMovement.knockBackFromRight = true;
        }
        else
        {
            playerMovement.knockBackFromRight = false;
        }
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
}
