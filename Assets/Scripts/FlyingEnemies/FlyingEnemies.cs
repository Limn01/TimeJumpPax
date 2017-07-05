using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemies : MonoBehaviour, IHealable,  Idamageable
{
    public float moveSpeed;
    public float playerRange;
    public LayerMask playerLayer;
    public float damage;
    [SerializeField]
    float startingHealth = 2;
    [SerializeField]
    float currentHealth;

    bool damaged;
    bool isDead;

    PlayerMovement playerMovement;
    Transform target;
    GameObject player;
    PlayerHealth playerHealth;
    float distance;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
        playerMovement = player.GetComponent<PlayerMovement>();
        target = player.GetComponent<Transform>();

        currentHealth = startingHealth;
    }

    void Update()
    {
        distance = (transform.position - target.position).sqrMagnitude;
       // distance = Vector3.Distance(transform.position, target.transform.position);
        
        if (distance < 100)
        {
            transform.position = Vector3.MoveTowards(transform.position,player.transform.position,moveSpeed * Time.deltaTime);
        }

        if (isDead)
        {
            FullHealth();
            Debug.Log("Full health");
        }

    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject == player)
        {

            playerMovement.knockbackCount = playerMovement.knockbackLength;

            if (player.transform.position.x < transform.position.x)
            {
                playerMovement.knockbackFromRight = true;
            }
            else
            {
                playerMovement.knockbackFromRight = false;
            }

            if (playerHealth.CurrentHealth > 0)
            {
                playerHealth.TakeDamage(damage);
            }
        }
    }

    public void TakeDamage(float amount)
    {
        damaged = true;

        currentHealth -= amount;

        gameObject.GetComponentInChildren<Animator>().Play("FlyingDamage");

        if (currentHealth <= 0 && !isDead)
        {
            Death();
        }
    }

    void Death()
    {
        isDead = true;
        gameObject.SetActive(false);
    }

    public void FullHealth()
    {
        isDead = false;
        currentHealth = startingHealth;
    }
}



