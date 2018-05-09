using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TurnOffBullets : MonoBehaviour
{
    public float damage = 1;

    public GameObject bullet;
    public Collider2D bulletCol;

    int enemyLayer;
    int groundLayer;
    int obstacleLayer;
    int bossLayer;

    bool isOnScreen;

    Camera cam;
    Plane[] planes;

    private void Start()
    {
        enemyLayer = LayerMask.NameToLayer("Enemy");
        groundLayer = LayerMask.NameToLayer("Ground");
        obstacleLayer = LayerMask.NameToLayer("Obstacle");
        bossLayer = LayerMask.NameToLayer("Boss");
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
        if (other.gameObject.layer == groundLayer)
        {
            this.gameObject.SetActive(false);
        }
        if (other.gameObject.layer == enemyLayer)
        {
            this.gameObject.SetActive(false);
            Enemy enemy = other.GetComponent<Enemy>();
            enemy.TakeDamage(damage);
        }
        else if (other.gameObject.layer == bossLayer)
        {
            this.gameObject.SetActive(false);
            BossHealth bossHealth = other.gameObject.GetComponent<BossHealth>();
            bossHealth.TakeDamage(damage);
        }
        else if (other.gameObject.layer == obstacleLayer)
        {
            this.gameObject.SetActive(false);
        }
    }

    void OffScreenDisable()
    {
        isOnScreen = false;
        this.gameObject.SetActive(false);
    }
}






