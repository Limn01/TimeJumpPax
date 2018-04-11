using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolEnemyScript : MonoBehaviour
{
    [SerializeField]
    float startingHealth = 5;
    [SerializeField]
    float currentHealth;

    bool damaged;
    bool isDead;

    public float StartingHealth
    {
        get
        {
            return startingHealth;
        }

        set
        {
            startingHealth = value;
        }
    }

    public float CurrentHealth
    {
        get
        {
            return currentHealth;
        }

        set
        {
            currentHealth = value;
        }
    }

    private void Awake()
    {
        CurrentHealth = StartingHealth;
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

        CurrentHealth -= amount;

        if (CurrentHealth <= 0 && !isDead)
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
        CurrentHealth = StartingHealth;
    }
}
