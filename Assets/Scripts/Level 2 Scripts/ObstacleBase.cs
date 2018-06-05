using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleBase : MonoBehaviour
{
    public float damage = 1;

    protected GameObject player;
    protected PlayerHealth playerHealth;
    protected Player playerMovement;

    int playerLayerIndex;
    bool playerInRange;

    protected virtual void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
        playerMovement = player.GetComponent<Player>();
        playerLayerIndex = LayerMask.NameToLayer("Player");
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == playerLayerIndex)
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == playerLayerIndex)
        {
            playerInRange = false;
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
