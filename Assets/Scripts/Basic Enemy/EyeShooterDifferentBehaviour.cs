using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeShooterDifferentBehaviour : MonoBehaviour
{
    public GameObject enemyShooterModel;

    [SerializeField]
    Transform shotPoint;
    [SerializeField]
    float waitBetweenShots;
    [SerializeField]
    float bulletSpeed;
    [SerializeField]
    float rotateSpeed = 200f;

    Transform target;
    GameObject player;
    float dist;
    float shotCounter;
    Rigidbody2D rb;

    ObjectPooler objectPooler;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        target = player.GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        objectPooler = ObjectPooler.SharedInstance;
        shotCounter = waitBetweenShots;

        enemyShooterModel.transform.rotation = Quaternion.Euler(0, 0, -90);
    }

    private void FixedUpdate()
    {
        LookAtTarget();
    }

    private void Update()
    {
        dist = (transform.position - target.position).sqrMagnitude;

        if (dist < 1000)
        {
            //Shoot();
        }
    }

    void Shoot()
    {
        shotCounter -= Time.deltaTime;

        if (shotCounter <= 0)
        {
            Vector2 direction = target.transform.position - transform.position;

            direction.Normalize();

            GameObject obj = EnemyBulletPool.current.GetPooledObject();
            List<GameObject> pooledObj = new List<GameObject>();
            pooledObj.Add(obj);
            int randomIndex = Random.Range(0, pooledObj.Count);
            if (!pooledObj[randomIndex].activeInHierarchy)
            {
                pooledObj[randomIndex].SetActive(true);
                pooledObj[randomIndex].transform.position = shotPoint.transform.position;
                pooledObj[randomIndex].transform.rotation = transform.rotation;
                pooledObj[randomIndex].GetComponent<Rigidbody2D>().AddForce(direction * bulletSpeed, ForceMode2D.Impulse);

                shotCounter = waitBetweenShots;
            }
        }
    }

    void LookAtTarget()
    {
        Vector2 direction = (Vector2)target.position - rb.position;
        direction.Normalize();
        float rotateAmount = Vector3.Cross(direction, transform.up).z;
        rb.angularVelocity = -rotateAmount * rotateSpeed;
    }
}
