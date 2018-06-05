using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turrent : Enemy
{
    public float playerInRange;
    public float bulletSpeed;
    public Transform shotPoint;
    public float shotDuration;
    
    public float rayDistance;
    public LayerMask groundCollision;

    float shotCounter;
    int enemyProjectileIndex = 1;

    public LayerMask playerLayer;

    RaycastHit2D hit;
    Rigidbody2D rb;
    ObjectPooler objectPooler;

    private void Start()
    {
        objectPooler = ObjectPooler.SharedInstance;
    }

    protected override void Update()
    {
        base.Update();

        Vector3 pos = transform.position;
        Vector3 down = -transform.up;
        Vector3 size = transform.localScale;
        Vector3 halfSize = size * 0.37f;

        Vector3 rayFromScale = pos + down * halfSize.y;

        Ray ray = new Ray(rayFromScale, down);

        Debug.DrawLine(ray.origin, ray.origin + ray.direction * rayDistance, Color.magenta);

        RaycastHit2D groundHit = Physics2D.Raycast(ray.origin, ray.direction, rayDistance, groundCollision);

        if (groundHit)
        {
            Vector2 targetLocation = groundHit.point;
            targetLocation += new Vector2(0, size.y / 3.1f);
            transform.position = targetLocation;
        }

        shotCounter += Time.deltaTime;

        hit = Physics2D.Raycast(transform.position, Vector3.right, playerInRange, playerLayer);

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

        GameObject enemyProjectile = objectPooler.GetPooledObject(enemyProjectileIndex);
        enemyProjectile.SetActive(true);
        enemyProjectile.transform.position = shotPoint.position;
        enemyProjectile.transform.rotation = shotPoint.rotation;
        enemyProjectile.GetComponent<Rigidbody2D>().AddForce(shotPoint.right * bulletSpeed, ForceMode2D.Impulse);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + transform.localScale.x * Vector3.right * playerInRange);
    }
}


