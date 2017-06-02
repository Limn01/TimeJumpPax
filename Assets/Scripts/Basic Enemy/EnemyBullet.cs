using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.LuisPedroFonseca.ProCamera2D
{
    public class EnemyBullet : MonoBehaviour
    {
        public int damage = 3;

        GameObject player;
        PlayerHealth playerHealth;

        void Awake()
        {
            player = GameObject.FindGameObjectWithTag("Player");
            playerHealth = player.GetComponent<PlayerHealth>();


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
                PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();
                playerMovement.knockbackCount = playerMovement.knockbackLength;

                if (other.transform.position.x < transform.position.x)
                {
                    playerMovement.knockbackFromRight = true;
                }

                if (other.transform.position.x > transform.position.x)
                {
                    playerMovement.knockbackFromRight = false;
                }
                playerHealth.TakeDamage(damage);
            }
        }

        void OnDisable()
        {
            CancelInvoke();
        }
    }

}

