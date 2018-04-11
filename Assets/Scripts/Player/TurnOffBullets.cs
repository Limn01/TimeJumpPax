using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TurnOffBullets : MonoBehaviour
{
    public float damage = 1;

    public GameObject bullet;
    public Collider2D bulletCol;

    int enemyLayer;

    bool isOnScreen;

    Camera cam;
    Plane[] planes;

    private void Start()
    {
        enemyLayer = LayerMask.NameToLayer("Enemy");

        cam = Camera.main;
    }

    private void Update()
    {
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

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            this.gameObject.SetActive(false);
        }
        if (other.gameObject.layer == enemyLayer)
        {
            this.gameObject.SetActive(false);
            Enemy enemy = other.GetComponent<Enemy>();
            enemy.TakeDamage(damage);
        }
        else if (other.gameObject.CompareTag("Boss"))
        {
            this.gameObject.SetActive(false);
            BossHealth bossHealth = other.gameObject.GetComponent<BossHealth>();
            bossHealth.TakeDamage(damage);
        }
        else if (other.gameObject.CompareTag("Obstacle"))
        {
            this.gameObject.SetActive(false);
            
        }
        else if (other.gameObject.CompareTag("Patrol"))
        {
            this.gameObject.SetActive(false);
            PatrolEnemyScript patrolHealth = other.gameObject.GetComponent<PatrolEnemyScript>();
            patrolHealth.TakeDamage(damage);
        }
    }

    void OffScreenDisable()
    {
        isOnScreen = false;
        this.gameObject.SetActive(false);
    }
}






