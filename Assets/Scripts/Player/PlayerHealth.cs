using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Com.LuisPedroFonseca.ProCamera2D;

public class PlayerHealth : MonoBehaviour, IHealable, Idamageable
{
    public CamaraShake cameraShake;

    [SerializeField]
    private float maxValue;
    [SerializeField]
    private float currentHealth;
    
    public bool damaged = false;
    public GameObject gameOver;

    public bool isDead;
    public bool fullyDead = false;
    //public AudioSource sound;
    
    [SerializeField]
    HealthBar healthBar;
    ProCamera2DShake proShake;
    GameObject camera;

    //GameObject player;
    Animator anim;
    GameObject gameManager;
    GameObject player;
    Player playerController;
    LevelManager levelManager;
    AudioManager audioManager;

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

    void OnDestroy()
    {

    }   

    void Awake()
    {
        PlayerHealth health = FindObjectOfType<PlayerHealth>();
        levelManager = FindObjectOfType<LevelManager>();
        camera = GameObject.FindGameObjectWithTag("MainCamera");
        proShake = camera.GetComponent<ProCamera2DShake>();
        anim = GetComponentInChildren<Animator>();
        audioManager = FindObjectOfType<AudioManager>();

        Initialize();

        CurrentHealth = MaxValue;
    }

    public void TakeDamage(float amount)
    {
        damaged = true;

        gameObject.GetComponentInChildren<Animation>().Play("PlayerDamage");

        CurrentHealth -= amount;

        proShake.Shake("PlayerHit");

        audioManager.Play("PlayerHurt");

        if (CurrentHealth <= 0 && !isDead)
        {
            Death();
        }
    }

    void Death()
    {
        isDead = true;
        gameObject.SetActive(false);
        levelManager.StartCoroutine("RespawnPlayer");
        FullHealth();

        if (LifeManager.instance.lifeCounter == 0)
        {
            gameOver.SetActive(true);
        }
    }

    public void FullHealth()
    {
        damaged = false;
        isDead = false;
        CurrentHealth = MaxValue;
    }

    public void Initialize()
    {
        this.MaxValue = MaxValue;
        this.CurrentHealth = currentHealth;
    }
}

