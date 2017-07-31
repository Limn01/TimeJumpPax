using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour
{
    public float startingHealth = 16f;
    public float currentHealth;
    public float damage = 1;

    bool isDamaged;
    bool isDead;

    private void OnEnable()
    {
        currentHealth = startingHealth;
        isDead = false;
    }

    private void Update()
    {
        
    }

    public void TakeDamage(float amount)
    {
        isDamaged = true;

        currentHealth -= amount;

        if (currentHealth <= 0 && !isDead)
        {
            Death();
        }
    }

    void Death()
    {
        isDead = true;
    }
}
