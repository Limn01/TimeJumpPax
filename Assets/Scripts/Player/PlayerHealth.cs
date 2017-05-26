using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth instance;
    [SerializeField]
    private float maxValue;
    [SerializeField]
    private float currentHealth;
    public bool damaged;
    public bool isDead;
    //public AudioSource sound;
    public AudioSource damageSound;

    //GameObject player;
    Animator anim;
    GameObject gameManager;
    [SerializeField]
    HealthBar healthBar;
    
    public float CurrentHealth
    {
        get
        {
            return currentHealth;
        }

        set
        {
            this.currentHealth = value;
            healthBar.Value = currentHealth;
        }
    }

    public float MaxValue
    {
        get
        {
            return maxValue;
        }

        set
        {
            this.maxValue = value;
            healthBar.MaxValue = maxValue;
        }
    }

    void Awake()
    {
        instance = this;
        //player = GameObject.FindGameObjectWithTag("Player");
        gameManager = GameObject.FindGameObjectWithTag("_GM");
        
        anim = GetComponentInChildren<Animator>();

        Initialize();

        CurrentHealth = MaxValue;
    }

   public void TakeDamage(float amount)
    {
        damaged = true;

        gameObject.GetComponentInChildren<Animation>().Play("PlayerDamage");

        CurrentHealth -= amount;

        CamaraShake.instance.ShakeCamera(3f, .3f);

        //cameraShake.ShakeCamera(5f, .3f);

        damageSound.Play();

        if (CurrentHealth <= 0 && !isDead)
        {
            Death();
            
        }
    }

    void Death()
    {
        isDead = true;
        gameObject.SetActive(false);
        LevelManager.instance.Invoke("RespawnPlayer", 1);
        FullHealth();

       
    }

    public void FullHealth()
    {
        isDead = false;
        CurrentHealth = MaxValue;
    }

    public void Initialize()
    {
        this.MaxValue = MaxValue;
        this.CurrentHealth = currentHealth;
    }
}
