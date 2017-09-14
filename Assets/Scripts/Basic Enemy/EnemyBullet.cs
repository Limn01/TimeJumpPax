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
        if (other.gameObject.tag == "Ground")
        {
            this.gameObject.SetActive(false);
        }

        if (other.gameObject.tag == "Player")
        {
            this.gameObject.SetActive(false);

            playerMovement.knockBackCount = playerMovement.knockBackLength;

            if (other.transform.position.x < transform.position.x)
            {
                playerMovement.knockBackFromRight = true;
            }

            if (other.transform.position.x > transform.position.x)
            {
                playerMovement.knockBackFromRight = false;
            }
            playerHealth.TakeDamage(damage);
        }
    }

    void OnDisable()
    {
        CancelInvoke();
    }
}


