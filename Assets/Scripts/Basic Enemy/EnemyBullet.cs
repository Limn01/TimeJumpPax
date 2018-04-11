using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public int damage = 3;

    GameObject player;
    PlayerHealth playerHealth;
    Player playerMovement;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
        playerMovement = player.GetComponent<Player>();
        
    }

   void OnEnable()
    {
        Invoke("Destroy", 5f);
    }

    void Destroy()
    {
        gameObject.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 10)
        {
            this.gameObject.SetActive(false);
        }

        if (other.gameObject.layer == 8)
        {
            if (!playerHealth.invinc)
            {
                Attack();
                playerHealth.TakeDamage(damage);
                this.gameObject.SetActive(false);
            }
        }
    }

    void Attack()
    {
        playerMovement.knockBackCount = playerMovement.knockBackLength;

        if (transform.position.x < transform.position.x)
        {
            playerMovement.knockBackFromRight = true;
        }

        if (transform.position.x > transform.position.x)
        {
            playerMovement.knockBackFromRight = false;
        }
    }

    void OnDisable()
    {
        CancelInvoke();
    }
}


