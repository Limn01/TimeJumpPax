    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotWormDamage : MonoBehaviour
{
    public float damage;
    public float timeBetweenAttack;

    bool playerInRange = false;

    float timer;

    GameObject player;
    Player playerMovement;
    PlayerHealth playerHealth;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerMovement = player.GetComponent<Player>();
        playerHealth = player.GetComponent<PlayerHealth>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 8)
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == 8)
        {
            playerInRange = false;
        }
    }

    private void Update()
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

        playerMovement.knockBackCount = playerMovement.knockBackLength;

        if (player.transform.position.x < this.transform.position.x)
        {
            playerMovement.knockBackFromRight = true;
        }
        else
        {
            playerMovement.knockBackFromRight = false;
        }

        if (playerHealth.CurrentHealth > 0)
        {
            playerHealth.TakeDamage(damage);
        }
    }
}
