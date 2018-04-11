using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDamage : MonoBehaviour
{
    public float damage = 1;

    bool playerInRange;

    GameObject player;
    PlayerHealth playerHealth;
    Player playerMovement;
    BossHealth bossHealth;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
        playerMovement = player.GetComponent<Player>();
        bossHealth = GetComponent<BossHealth>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer == 8)
        {
            playerInRange = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.layer == 8)
        {
            playerInRange = false;
        }
    }

    private void Update()
    {
        if (playerInRange && bossHealth.currentHealth > 0)
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
