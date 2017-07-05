using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyHealth : MonoBehaviour,Idamageable, IHealable
{
    public float damage = 1;
   
    [SerializeField]
    float startingHealth;
    [SerializeField]
    float currentHealth;

    bool damaged;
    bool isDead;

    private void Awake()
    {
        currentHealth = startingHealth;
    }

    private void Update()
    {
        if (isDead)
        {
            FullHealth();
        }
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

    public void FullHealth()
    {
        isDead = false;
        currentHealth = startingHealth;
    }
}



