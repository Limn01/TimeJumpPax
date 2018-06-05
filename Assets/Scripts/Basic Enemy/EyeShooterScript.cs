using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeShooterScript : ObstacleBase
{
    public float playerRange;
    public float bulletSpeed;
    public Transform shotPoint;
    public float waitBetweenShots;
    public float rotateSpeed = 200f;

    int enemyProjectileIndex = 1;
    float shotCounter;
    Transform target;
    Rigidbody2D rb;
    ObjectPooler objectPooler;

    protected override void Awake()
    {
        base.Awake();
        rb = GetComponent<Rigidbody2D>();
        target = player.GetComponent<Transform>();

        shotCounter = waitBetweenShots;
    }

    private void Start()
    {
        objectPooler = ObjectPooler.SharedInstance;
    }

    private void FixedUpdate()
    {
        EnemyTurn();
    }

    protected override void Update()
    {
        base.Update();

        shotCounter -= Time.deltaTime;

        if (playerMovement.transform.position.x > transform.position.x && playerMovement.transform.position.x < transform.position.x + playerRange && shotCounter < 0)
        {
            GameObject enemyProjectile = objectPooler.GetPooledObject(enemyProjectileIndex);
            enemyProjectile.transform.position = shotPoint.position;
            enemyProjectile.transform.rotation = shotPoint.rotation;
            enemyProjectile.SetActive(true);
            enemyProjectile.GetComponent<Rigidbody2D>().AddForce(shotPoint.right * bulletSpeed, ForceMode2D.Impulse);
        }

        if (playerMovement.transform.position.x < transform.position.x && playerMovement.transform.position.x > transform.position.x - playerRange && shotCounter < 0)
        {
            GameObject enemyProjectile = objectPooler.GetPooledObject(enemyProjectileIndex);
            enemyProjectile.transform.position = shotPoint.position;
            enemyProjectile.transform.rotation = shotPoint.rotation;
            enemyProjectile.GetComponent<Rigidbody2D>().AddForce(-shotPoint.right * bulletSpeed, ForceMode2D.Impulse);
        }
    }

    void EnemyTurn()
    {
        Vector2 direction = (Vector2)target.position - rb.position;
        direction.Normalize();
        float rotateAmount = Vector3.Cross(direction, transform.up).z;
        rb.angularVelocity = -rotateAmount * rotateSpeed;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(new Vector3(transform.position.x - playerRange, transform.position.y, transform.position.z),
           new Vector3(transform.position.x + playerRange, transform.position.y, transform.position.z));
    }
}
