using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turrent : MonoBehaviour
{
    public float playerInRange;
    public float bulletSpeed;
    public Transform shotPoint;
    public float shotDuration;
    
    public float rayDistance;
    public LayerMask groundCollision;

    float shotCounter;
    public LayerMask playerLayer;
    int damage = 2;

    RaycastHit2D hit;
    GameObject player;
    PlayerHealth playerHealth;
    PlayerMovement playerMovement;
    Rigidbody2D rb;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
        playerMovement = player.GetComponent<PlayerMovement>();
    }

    void Update()
    {
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
}


