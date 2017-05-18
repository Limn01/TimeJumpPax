using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth instance;
    public int startingHealth = 16;
    public int currentHealth;
    public bool damaged;
    public bool isDead;
    //public AudioSource sound;
    public Slider healthSlider;
    public AudioSource damageSound;

    //GameObject player;
    Animator anim;
    GameObject gameManager;
    
   
    void Awake()
    {
        instance = this;
        //player = GameObject.FindGameObjectWithTag("Player");
        gameManager = GameObject.FindGameObjectWithTag("_GM");
        
        anim = GetComponentInChildren<Animator>();

        currentHealth = startingHealth;
    }

   public void TakeDamage(int amount)
    {
        damaged = true;

        gameObject.GetComponentInChildren<Animation>().Play("PlayerDamage");

        currentHealth -= amount;

        healthSlider.value = currentHealth;

        damageSound.Play();

        if (currentHealth <= 0 && !isDead)
        {
            Death();
            
        }
    }

    void Death()
    {
        isDead = true;
        //this.gameObject.SetActive(false);
        LifeManager.instance.TakeLife();
        LevelManager.instance.RespawnPlayer();
        FullHealth();

    }

    public void FullHealth()
    {
        isDead = false;
        currentHealth = startingHealth;
        healthSlider.value = currentHealth;
    }
}
