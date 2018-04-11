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
    public float colorSpeed;

    float timeBetweenDeath = .5f;
    float timer;

    bool isHit;
    bool isDead;

    SpriteRenderer render;
    GameObject player;
    GravityBody gravityBody;
    AudioManager audio;

    private void OnEnable()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        gravityBody = player.GetComponent<GravityBody>();
        render = GetComponentInChildren<SpriteRenderer>();
        audio = FindObjectOfType<AudioManager>();

        currentHealth = startingHealth;
    }

    private void Update()
    {
        if (isHit)
        {
            render.color = Color.clear;
        }
        else
        {
            render.color = Color.Lerp(render.color, Color.white, colorSpeed * Time.deltaTime);
        }

        isHit = false;
    }

    public void TakeDamage(float amount)
    {
        isHit = true;

        if (isDead)
        {
            return;
        }

        audio.Play("EnemyHurt");

        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            Death();
            Instantiate(portal, portalSpawnPoint.position, Quaternion.identity);
        }
    }

    void Death()
    {
        isDead = true;
        this.gameObject.SetActive(false);
        Instantiate(explosion, transform.position, Quaternion.identity);
    }
}
