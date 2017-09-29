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
    public GameObject portal;
    public Transform portalSpawnPoint;

    float timeBetweenDeath = .5f;
    float timer;

    bool isDamaged;
    bool isDead;
    
    GameObject player;
    GravityBody gravityBody;

    private void OnEnable()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        gravityBody = player.GetComponent<GravityBody>();

        currentHealth = startingHealth;
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
        Instantiate(portal, portalSpawnPoint.position, Quaternion.identity);
    }
}
