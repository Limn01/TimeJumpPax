using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Com.LuisPedroFonseca.ProCamera2D;

public class PlayerHealth : MonoBehaviour
{
    //public CamaraShake cameraShake;

    [SerializeField]
    private float maxValue;
    [SerializeField]
    private float currentHealth;

    public float timeBetweenInvis;
    public GameObject gameOver;
    public float flashSpeed;
    int enemyLayer;
    int playerLayer;

    public bool damaged = false;
    public bool isDead;
    public bool fullyDead = false;
    public bool invinc;
   
    public float hurtTime;
    public float timer;
   
    [SerializeField]
    HealthBar healthBar;
    ProCamera2DShake proShake;
    GameObject camera;
    Animator anim;
    GameObject gameManager;
    GameObject player;
    Player playerController;
    LevelManager levelManager;
    AudioManager audioManager;
    SpriteRenderer render;

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
        render = GetComponentInChildren<SpriteRenderer>();
        enemyLayer = LayerMask.NameToLayer("Enemy");
        playerLayer = LayerMask.NameToLayer("Player");

        Initialize();

        CurrentHealth = MaxValue;
    }

    private void Update()
    {

        if (timer <= 0)
        {
            invinc = false;
            render.color = Color.white;
            gameObject.GetComponentInChildren<Animation>().Stop("PlayerDamage");
        }
        else
        {
            invinc = true;
            timer -= Time.deltaTime;
            gameObject.GetComponentInChildren<Animation>().Play("PlayerDamage");

        }

        damaged = false;
    }
    public void TakeDamage(float amount)
    {
        damaged = true;

        //StartCoroutine(HurtBlinker());
        timer += 1.5f;

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
        audioManager.Play("Death");
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

