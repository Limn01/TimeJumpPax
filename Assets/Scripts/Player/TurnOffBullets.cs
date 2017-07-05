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

        if (other.gameObject.tag == "Obstacle")
        {
            this.gameObject.SetActive(false);
        }
    }

    void OnDisable()
    {
        CancelInvoke();
    }
}






