using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turrent : MonoBehaviour,Idamageable,IHealable
{
    public float playerInRange;
    public float bulletSpeed;
    public Transform shotPoint;
    public float shotDuration;
    [SerializeField]
    float startingHealth = 5;
    [SerializeField]
    float currentHealth;

    float gravity;
    float shotCounter;
    bool isDamaged;
    public bool isDead;
    bool restore;
    public LayerMask playerLayer;
    int damage = 2;
    public float maxJumpHeight = 6;
    public float minJumpHeight = 1;
    public float timeToJumpApex = .4f;
    float accerlerationTimeAirborne = .2f;
    float accelerationTimeGrounded = .1f;
    float maxJumpVelocity;
    float minJumpVelocity;

    RaycastHit2D hit;
    GameObject player;
    PlayerHealth playerHealth;
    PlayerMovement playerMovement;
    Rigidbody2D rb;

    public float CurrentHealth
    {
        get
        {
            return currentHealth;
        }

        set
        {
            this.currentHealth = value;
        }
    }

    public float StartingHealth
    {
        get
        {
            return startingHealth;
        }

        set
        {
            this.startingHealth = value;
        }
    }

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
        playerMovement = player.GetComponent<PlayerMovement>();
        rb = GetComponent<Rigidbody2D>();

        Init();

       
    }

    private void OnEnable()
    {
        CurrentHealth = StartingHealth;
    }

    private void Start()
    {
        gravity = -(2 * maxJumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        maxJumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
        minJumpVelocity = Mathf.Sqrt(2 * Mathf.Abs(gravity) * minJumpVelocity);
    }

    private void FixedUpdate()
    {
        rb.AddForce(Vector2.down * -gravity * Time.deltaTime, ForceMode2D.Impulse);
    }


    void Update()
    {
        shotCounter += Time.deltaTime;

        hit = Physics2D.Raycast(transform.position, Vector2.right, playerInRange, playerLayer);

        if (hit.collider != null && hit.collider.tag == "Player")
        {
            if (shotCounter >= shotDuration)
            {
                Shoot();
            }
        }

        //if (isDead)
        //{
        //    FullHealth();
        //    Debug.Log("Restore Health");
        //}
    }

    void Shoot()
    {
        shotCounter = 0;

        GameObject obj = TurrentBulletPool.current.GetPooledObject();
        List<GameObject> pooledObj = new List<GameObject>();
        pooledObj.Add(obj);
        int randomIndex = Random.Range(0, pooledObj.Count);
        if (!pooledObj[randomIndex].activeInHierarchy)
        {
            pooledObj[randomIndex].SetActive(true);
            pooledObj[randomIndex].transform.position = shotPoint.transform.position;
            pooledObj[randomIndex].transform.rotation = transform.rotation;
            pooledObj[randomIndex].GetComponent<Rigidbody2D>().AddForce(shotPoint.right * bulletSpeed, ForceMode2D.Impulse);


        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position +transform.localScale.x*Vector3.right*playerInRange);
    }

    public void TakeDamage(float amount)
    {
        isDamaged = true;

        gameObject.GetComponent<Animation>().Play("TurrentDamage");

        CurrentHealth -= amount;

        if (CurrentHealth <= 0 && !isDead)
        {
            Death();
        }
    }

    void Death()
    {
        isDead = true;

        GameObject obj = ExplosionsPool.current.GetPooledObject();
        List<GameObject> pooledObj = new List<GameObject>();
        pooledObj.Add(obj);
        int randomIndex = Random.Range(0, pooledObj.Count);
        if (!pooledObj[randomIndex].activeInHierarchy)
        {
            pooledObj[randomIndex].SetActive(true);
            pooledObj[randomIndex].transform.position = transform.position;
            pooledObj[randomIndex].transform.rotation = transform.rotation;
        }
        this.gameObject.SetActive(false);
    }

    public void FullHealth()
    {
        restore = true;
        isDead = false;
        CurrentHealth = StartingHealth;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "MovingPlatform")
        {
            transform.parent = other.transform;
        }

        if (other.gameObject.tag == "Player")
        {
            playerMovement.knockbackCount = playerMovement.knockbackLength;

            if (player.transform.position.x < transform.position.x)
            {
                playerMovement.knockbackFromRight = true;
            }
            else
            {
                playerMovement.knockbackFromRight = false;
            }

            if (playerHealth.CurrentHealth > 0)
            {
                playerHealth.TakeDamage(damage);
            }
        }
    }

    public void Init()
    {
        this.StartingHealth = StartingHealth;
        this.CurrentHealth = CurrentHealth;
    }
}


