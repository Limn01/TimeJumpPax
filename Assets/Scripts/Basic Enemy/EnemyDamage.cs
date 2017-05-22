using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public int damage = 1;
    public float timebetweenAttack = 0.5f;

    public bool playerInRange;
    public float timer;

    GameObject player;
    Animator anim;

    void Awake()
    {
        PlayerMovement.instance.GetComponent<PlayerMovement>();
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
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

        PlayerMovement.instance.knockbackCount = PlayerMovement.instance.knockbackLength;

        if (player.transform.position.x < transform.position.x)
        {
            PlayerMovement.instance.knockbackFromRight = true;
        }
        else
        {
            PlayerMovement.instance.knockbackFromRight = false;
        }

        if ( PlayerHealth.instance.CurrentHealth > 0)
        {
            PlayerHealth.instance.TakeDamage(damage);
        }
        
       
    }
}
