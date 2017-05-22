using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{`
    public static PlayerHealth instance;
    [SerializeField]
    private int maxVal = 16;
    [SerializeField]
    private int currentHealth;
    public bool damaged;
    public bool isDead;
    //public AudioSource sound;
    public Slider healthSlider;
    public AudioSource damageSound;

    //GameObject player;
    Animator anim;
    GameObject gameManager;
    HealthBar healthBar;

    public int CurrentHealth
    {
        get
        {
            return currentHealth;
        }

        set
        {
            currentHealth = value;
            healthBar.Value = currentHealth;
        }
    }

    public int StartingHealth
    {
        get
        {
            return maxVal;
        }

        set
        {
            maxVal = value;
            healthBar.MaxValue = maxVal;
        }
    }

    void Awake()
    {
        instance = this;
        //player = GameObject.FindGameObjectWithTag("Player");
        gameManager = GameObject.FindGameObjectWithTag("_GM");
        
        anim = GetComponentInChildren<Animator>();

        //CurrentHealth = StartingHealth;
    }

   public void TakeDamage(int amount)
    {
        damaged = true;

        gameObject.GetComponentInChildren<Animation>().Play("PlayerDamage");

        CurrentHealth -= amount;

        //healthSlider.value = CurrentHealth;

        damageSound.Play();

        if (CurrentHealth <= 0 && !isDead)
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
        CurrentHealth = StartingHealth;
        healthSlider.value = CurrentHealth;
    }
}
