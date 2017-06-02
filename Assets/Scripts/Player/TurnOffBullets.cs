using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.LuisPedroFonseca.ProCamera2D
{
    public class TurnOffBullets : MonoBehaviour
    {
        public int damage = 1;

        void OnEnable()
        {
            Invoke("Destroy", 2f);
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

            if (other.gameObject.tag == "Enemy")
            {
                this.gameObject.SetActive(false);
                other.gameObject.SetActive(false);
            }

            if (other.gameObject.tag == "Enemy1")
            {
                this.gameObject.SetActive(false);
                other.gameObject.SetActive(false);
            }

            if (other.gameObject.tag == "Obstacle")
            {
                this.gameObject.SetActive(false);
            }

            if (other.gameObject.tag == "Enemy2")
            {
                this.gameObject.SetActive(false);
                other.gameObject.SetActive(false);
            }

            if (other.gameObject.tag == "Turrent")
            {
                Turrent turrentScirpt = other.gameObject.GetComponent<Turrent>();
                turrentScirpt.TakeDamage(damage);
                this.gameObject.SetActive(false);
            }

            if (other.gameObject.tag == "Enemy3")
            {
                this.gameObject.SetActive(false);
                FlyingEnemies flyingEnemies = other.gameObject.GetComponent<FlyingEnemies>();
                flyingEnemies.TakeDamage(damage);
            }
        }

        void OnDisable()
        {
            CancelInvoke();
        }
    }
}


