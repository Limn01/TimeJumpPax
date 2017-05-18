using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turrent : MonoBehaviour
{
    public float playerInRange;
    public float bulletSpeed;
    public Transform shotPoint;
    public float shotDuration;
    public float startingHealth = 5;
    public float currentHealth;

    float shotCounter;
    bool isDamaged;
    bool isDead;
    public LayerMask playerLayer;
    int damage = 2;

    RaycastHit2D hit;
    GameObject player;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
   
    void Start()
    {
        currentHealth = startingHealth;
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
        Gizmos.DrawLine(transform.position, transform.position + transform.localScale.x * Vector3.right * playerInRange);
    }

    public void TakeDamage(int amount)
    {
        isDamaged = true;

        gameObject.GetComponent<Animation>().Play("TurrentDamage");

        currentHealth -= amount;
        
        if (currentHealth <= 0 && !isDead)
        {
            Death();
        }
    }

    void Death()
    {
        isDead = true;
        this.gameObject.SetActive(false);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "MovingPlatform")
        {
            transform.parent = other.transform;
        }

        if (other.gameObject.tag == "Player")
        {
            PlayerMovement.instance.knockbackCount = PlayerMovement.instance.knockbackLength;

            if (player.transform.position.x < transform.position.x)
            {
                PlayerMovement.instance.knockbackFromRight = true;
            }
            else
            {
                PlayerMovement.instance.knockbackFromRight = false;
            }

            if (PlayerHealth.instance.currentHealth > 0)
            {
                PlayerHealth.instance.TakeDamage(damage);
            }
        }
    }
}
