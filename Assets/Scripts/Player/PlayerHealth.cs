using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Com.LuisPedroFonseca.ProCamera2D;

public class PlayerHealth : MonoBehaviour
{
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
    public bool invinc = false;
   
    public float hurtTime;
    public float timer;
    public GameObject playerModel;
    public float flashDelay = 1f;
   
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
        levelManager = FindObjectOfType<LevelManager>();
        camera = GameObject.FindGameObjectWithTag("MainCamera");
        proShake = camera.GetComponent<ProCamera2DShake>();
        anim = GetComponentInChildren<Animator>();
        audioManager = FindObjectOfType<AudioManager>();
        //render = GetComponentInChildren<SpriteRenderer>();
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
            damaged = false;
        }
        else
        {
            invinc = true;
            timer -= Time.deltaTime;

        }
    }
    public void TakeDamage(float amount)
    {
        damaged = true;

        StartCoroutine(HurtBlinker());

        timer += 2f;

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

    IEnumerator HurtBlinker()
    {
        if (playerModel != null)
        {
            while (damaged)
            {
                playerModel.SetActive(false);
                yield return new WaitForSeconds(flashDelay);
                playerModel.SetActive(true);
                yield return new WaitForSeconds(flashDelay);
            }
        }
    }
}

