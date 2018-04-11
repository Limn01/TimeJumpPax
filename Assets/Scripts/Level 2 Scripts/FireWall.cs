using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWall : MonoBehaviour
{
    public FireWallState fireWallState;

    public float timeBetweenFire;
    public float damage;
    public GameObject fire;

    GameObject player;
    PlayerHealth playerHealth;
    Player playerMovement;
    BoxCollider2D boxCol;

    public float timer;

    bool playerInRange;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
        playerMovement = player.GetComponent<Player>();
        boxCol = GetComponent<BoxCollider2D>();

        boxCol.enabled = false;
    }

    private void Update()
    {
        switch (fireWallState)
        {
            case FireWallState.Off:
                timer += Time.deltaTime;

                boxCol.enabled = false;

                if (timer >= timeBetweenFire)
                {
                    fire.SetActive(true);
                    fireWallState = FireWallState.On;
                }

                break;

            case FireWallState.On:

                timer -= Time.deltaTime;
                boxCol.enabled = true;

                if (timer <= 0)
                {
                    fire.SetActive(false);
                    fireWallState = FireWallState.Off;
                }

                break;
        }

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
}

public enum FireWallState
{
    Off,
    On
}
