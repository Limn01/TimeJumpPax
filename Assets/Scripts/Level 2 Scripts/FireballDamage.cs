using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballDamage : MonoBehaviour
{
    public int damage = 1;
    public Collider2D fireCol;

    protected GameObject player;
    protected PlayerHealth playerHealth;
    protected Player playerMovement;
    Camera cam;
    Plane[] planes;

    bool isOnScreen;


    protected virtual void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
        playerMovement = player.GetComponent<Player>();
    }

    private void Start()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        planes = GeometryUtility.CalculateFrustumPlanes(cam);

        if (GeometryUtility.TestPlanesAABB(planes, fireCol.bounds))
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

    protected void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 8)
        {
            if (!playerHealth.invinc)
            {
                Attack();
                playerHealth.TakeDamage(damage);
                this.gameObject.SetActive(false);
            }
        }

        if (other.gameObject.layer == 10)
        {
            this.gameObject.SetActive(false);
        }
    }

    protected void Attack()
    {
        playerMovement.knockBackCount = playerMovement.knockBackLength;

        if (transform.position.x < transform.position.x)
        {
            playerMovement.knockBackFromRight = true;
        }

        if (transform.position.x > transform.position.x)
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
