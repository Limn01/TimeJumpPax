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
    int enemyLayer;
    int playerLayer;

    public bool playerInRange;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
        playerMovement = player.GetComponent<Player>();
        enemyLayer = LayerMask.NameToLayer("Enemy");
        playerLayer = LayerMask.NameToLayer("Player");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 8)
        {
            playerInRange = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == 8)
        {
            playerInRange = false;
        }
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= timeBetweenAttack && playerInRange)
        {
            if (!playerHealth.invinc)
            {
                Attack();
            }
        }
    }

    void Attack()
    {
        timer = 0;
       
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


