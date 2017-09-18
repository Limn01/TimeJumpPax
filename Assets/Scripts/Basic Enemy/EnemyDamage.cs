using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Com.LuisPedroFonseca.ProCamera2D;

public class EnemyDamage : MonoBehaviour
{
    public float damage = 1;
    public float timebetweenAttack = 0.5f;
    

    bool playerInRange;
    public float timer;
    GameObject player;
    Animator anim;
    PlayerHealth playerHealth;
    Player playerMovement;
    Controller2D controller;
    
    

    void Awake()
    {
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
        playerMovement = FindObjectOfType<Player>();
        controller = player.GetComponent<Controller2D>();
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject == player)
        {
            playerInRange = true;
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject == player)
        {
            playerInRange = false;
        }
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= timebetweenAttack && playerInRange)
        {
            Attack();
            
            Debug.Log("Attacking");
        }
    }

    void Attack()
    {
        timer = 0f;

        playerMovement.knockBackCount = playerMovement.knockBackLength;

        if (player.transform.position.x < transform.position.x)
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



