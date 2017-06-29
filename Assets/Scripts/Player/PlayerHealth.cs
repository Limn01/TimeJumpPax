using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Com.LuisPedroFonseca.ProCamera2D;

public class PlayerHealth : MonoBehaviour, IHealable, Idamageable
{

    [SerializeField]
    private float maxValue;
    [SerializeField]
    private float currentHealth;

    public bool damaged = false;

    public bool isDead;
    //public AudioSource sound;
    [SerializeField]
    AudioSource damageSound;
    [SerializeField]
    HealthBar healthBar;

    //GameObject player;
    Animator anim;
    GameObject gameManager;
    GameObject player;
    [SerializeField]
    CamaraShake cameraShake;


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

        anim = GetComponentInChildren<Animator>();

        Initialize();

        CurrentHealth = MaxValue;
    }

    public void TakeDamage(float amount)
    {
        damaged = true;

        gameObject.GetComponentInChildren<Animation>().Play("PlayerDamage");

        CurrentHealth -= amount;

        cameraShake.ShakeCamera(3f, .3f);

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
        LevelManager.instance.StartCoroutine("RespawnPlayer");
        FullHealth();
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

