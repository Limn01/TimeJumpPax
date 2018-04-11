using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public float damage = 1;
    public float timebetweenAttack = 1f;

    bool playerInRange;

    float timer;
    GameObject player;
    PlayerHealth playerHealth;
    Player playerMovement;
    int enemyLayer;
    int playerLayer;

    EnemyHealth enemyHealth;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
        playerMovement = FindObjectOfType<Player>();
        enemyHealth = GetComponent<EnemyHealth>();
        enemyLayer = LayerMask.NameToLayer("Enemy");
        playerLayer = LayerMask.NameToLayer("Player");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == playerLayer)
        {
            playerInRange = true;
        }
    }
    
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == playerLayer)
        {
            playerInRange = false;
        }
    }

    public virtual void Update()
    {
        timer += Time.deltaTime;

        if (/*timer >= timebetweenAttack*/ playerInRange /*&& enemyHealth.currentHealth > 0*/)
        {
            if (!playerHealth.invinc)
            {
                Attack();
            }
        }
    }

    void Attack()
    {
        timer = 0f;

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



