using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyProjectile : MonoBehaviour
{
    public float damage;
    public GameObject bullet;
    public Collider2D bulletCol;
    public float speed;

    bool isOnScreen;

    Camera cam;
    Plane[] planes;
    Rigidbody2D r;
    GameObject player;
    Player playerMovement;
    PlayerHealth playerHealth;
    

    private void Awake()
    {
        r = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerMovement = player.GetComponent<Player>();
        playerHealth = player.GetComponent<PlayerHealth>();

        cam = Camera.main;
    }

    private void Update()
    {
        r.AddForce(transform.right * speed, ForceMode2D.Force);

        planes = GeometryUtility.CalculateFrustumPlanes(cam);

        if (GeometryUtility.TestPlanesAABB(planes, bulletCol.bounds))
        {
            isOnScreen = true;
        }
        else
        {
            isOnScreen = false;
        }

        if (!isOnScreen)
        {
            OffScreenDisable();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 10)
        {
            this.gameObject.SetActive(false);
        }

        if (other.gameObject.layer == 8)
        {
            if (!playerHealth.invinc)
            {
                Attack();
                playerHealth.TakeDamage(damage);
                this.gameObject.SetActive(false);
            }
        }
    }

    void Attack()
    {
        playerMovement.knockBackCount = playerMovement.knockBackLength;

        if (transform.position.x < transform.position.x)
        {
            playerMovement.knockBackFromRight = true;
        }
        else
        {
            playerMovement.knockBackFromRight = false;
        }
    }

    void OffScreenDisable()
    {
        isOnScreen = false;
        this.gameObject.SetActive(false);
    }
}
