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
    public Vector2 tempPos;
    public float verticalSpeed;
    public float amplitude;

    bool damaged;
    bool isDead;

    Player playerMovement;
    Transform target;
    GameObject player;
    PlayerHealth playerHealth;
    float distance;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
        playerMovement = player.GetComponent<Player>();
        target = player.GetComponent<Transform>();
    }

 
    private void OnEnable()
    {
        currentHealth = startingHealth;
    }

    void Update()
    {
        distance = (transform.position - target.position).sqrMagnitude;
        
        if (distance < 100)
        {
            transform.position = Vector3.MoveTowards(transform.position,player.transform.position,moveSpeed * Time.deltaTime);
        }
        else
        {
            transform.Translate(0, Mathf.Sin(Time.realtimeSinceStartup * verticalSpeed) * amplitude, 0);
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject == player)
        {

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



