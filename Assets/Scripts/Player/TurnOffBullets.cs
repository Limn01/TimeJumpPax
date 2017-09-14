using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TurnOffBullets : MonoBehaviour
{
    public float damage = 1;

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
            EnemyHealth enemyHealth = other.gameObject.GetComponent<EnemyHealth>();
            enemyHealth.TakeDamage(damage);
        }

        else if (other.gameObject.tag == "Boss")
        {
            this.gameObject.SetActive(false);
            BossHealth bossHealth = other.gameObject.GetComponent<BossHealth>();
            bossHealth.TakeDamage(damage);
        }
        else if (other.gameObject.tag == "Turrent")
        {
            this.gameObject.SetActive(false);
            Turrent turrent = other.gameObject.GetComponent<Turrent>();
            turrent.TakeDamage(damage);
        }

        else if (other.gameObject.tag == "Obstacle")
        {
            this.gameObject.SetActive(false);
        }
        else if (other.gameObject.tag == "Patrol")
        {
            this.gameObject.SetActive(false);
            PatrolEnemyScript patrolHealth = other.gameObject.GetComponent<PatrolEnemyScript>();
            patrolHealth.TakeDamage(damage);
        }
    }

    void OnDisable()
    {
        CancelInvoke();
    }
}






