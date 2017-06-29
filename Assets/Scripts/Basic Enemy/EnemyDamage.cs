using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Com.LuisPedroFonseca.ProCamera2D;

    public class EnemyDamage : MonoBehaviour
    {
        public float damage = 1;
        public float timebetweenAttack = 0.5f;

        public bool playerInRange;
        public float timer;

        GameObject player;
        Animator anim;
        PlayerHealth playerHealth;
        PlayerMovement playerMovement;
        

        void Awake()
        {
            anim = GetComponent<Animator>();
            player = GameObject.FindGameObjectWithTag("Player");
            playerHealth = player.GetComponent<PlayerHealth>();
            playerMovement = player.GetComponent<PlayerMovement>();
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



