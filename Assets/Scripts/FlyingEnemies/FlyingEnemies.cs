using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Com.LuisPedroFonseca.ProCamera2D
{
    public class FlyingEnemies : MonoBehaviour, IHealable
    {
        public float moveSpeed;
        public float playerRange;
        public LayerMask playerLayer;
        public float damage;
        [SerializeField]
        float startingHealth = 2;
        [SerializeField]
        float currentHealth;


        public bool playerInRange;
        bool damaged;
        bool isDead;

        PlayerMovement playerMovement;
        GameObject player;
        PlayerHealth playerHealth;

        void Awake()
        {
            player = GameObject.FindGameObjectWithTag("Player");
            playerHealth = player.GetComponent<PlayerHealth>();
            playerMovement = player.GetComponent<PlayerMovement>();

            currentHealth = startingHealth;
        }

        void Update()
        {
            playerInRange = Physics2D.OverlapCircle(transform.position, playerRange, playerLayer);

            if (playerInRange)
            {
                transform.position = Vector3.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);
            }

            if (isDead)
            {
                FullHealth();
                Debug.Log("Full health");
            }

        }

        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(transform.position, playerRange);
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
}


