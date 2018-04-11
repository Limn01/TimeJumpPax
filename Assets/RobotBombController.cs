using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotBombController : MonoBehaviour
{
    public LayerMask groundLayer;
    public float rayDistance;
    public Transform shootPoint;
    public GameObject bullet;

    public float bulletSpeedLow;
    public float bulletSpeedHigh;
    public float bulletAngle;
    public float bulletTorquAngle;

    Transform target;
    GameObject player;

    Rigidbody2D bulletRb;

    float timer;
    float timeBewteenShots = 2f;

    bool rayIsHitting = false;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        target = player.GetComponent<Transform>();

        
    }

    private void Update()
    {
        Vector3 down = -transform.up;
        Vector3 pos = transform.position;

        Ray ray = new Ray(pos, down);

        Debug.DrawLine(ray.origin, ray.origin + ray.direction * rayDistance, Color.green);

        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, rayDistance, groundLayer);

        if (hit.collider != null)
        {
            rayIsHitting = true;

            Vector3 targetPos = hit.point;

            targetPos += new Vector3(0, transform.localScale.y / 2, 0);

            transform.position = targetPos;
        }

        timer += Time.deltaTime;

        if (timer >= timeBewteenShots)
        {
            Shoot();

            bullet = GameObject.FindGameObjectWithTag("Bullet");

            if (bullet.activeInHierarchy)
            {
                bulletRb = bullet.GetComponent<Rigidbody2D>();

                Vector3 direction = target.position - transform.position;

                direction.Normalize();

                bulletRb.AddForce(new Vector3(Random.Range(-bulletAngle, bulletAngle), Random.Range(bulletSpeedLow, bulletSpeedHigh)) + direction, ForceMode2D.Impulse);
                bulletRb.AddTorque(Random.Range(-bulletTorquAngle, bulletTorquAngle));
                
            }
            //bulletRb = bullet.GetComponent<Rigidbody2D>();
        }
    }

    void Shoot()
    {
        timer = 0;

        Instantiate(bullet, shootPoint.position, Quaternion.identity);

        

        
    }
}
