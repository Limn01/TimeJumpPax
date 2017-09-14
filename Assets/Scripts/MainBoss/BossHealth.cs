using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour
{
    public float startingHealth = 16f;
    public float currentHealth;
    public float damage = 1;
    public GameObject explosion;

    bool isDamaged;
    public bool isDead;

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
        this.gameObject.SetActive(false);
        Instantiate(explosion, transform.position, Quaternion.identity);
    }
}
