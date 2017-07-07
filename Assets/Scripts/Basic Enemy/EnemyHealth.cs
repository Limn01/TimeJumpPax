using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour,Idamageable
{
    public float damage = 1;
   
    [SerializeField]
    float startingHealth;
    [SerializeField]
    float currentHealth;

    bool damaged;
    bool isDead;

    private void OnEnable()
    {
        currentHealth = startingHealth;
        isDead = false;
    }

    public void TakeDamage(float amount)
    {
        damaged = true;

        currentHealth -= amount;

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

}



