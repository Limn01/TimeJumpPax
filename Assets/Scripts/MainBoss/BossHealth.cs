using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossHealth : MonoBehaviour
{
    public float startingHealth = 16f;
    public float currentHealth;
    public float damage = 1;
    public GameObject explosion;
    public TimeManager timeManager;

    float timeBetweenDeath = .5f;
    float timer;

    bool isDamaged;
    public bool isDead;

    

    private void OnEnable()
    {
        currentHealth = startingHealth;
        //isDead = false;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        
        if (isDead)
        {
            if (timer >= timeBetweenDeath)
            {
                //timer = 0;
                SceneManager.LoadScene("MainMenu");
                //timeManager.DoSlowmotion();
            }
        }
    }

    public void TakeDamage(float amount)
    {
        isDamaged = true;

        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            Death();
        }
    }

    void Death()
    {
        isDead = true;
        //timeManager.DoSlowmotion();
        //Debug.Log("time slow should work");
        this.gameObject.SetActive(false);
        Instantiate(explosion, transform.position, Quaternion.identity);
        //SceneManager.LoadScene("MainMenu");
    }
}
