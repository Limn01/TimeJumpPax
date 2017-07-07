using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeShooterDifferentBehaviour : MonoBehaviour
{
    [SerializeField]
    Transform shotPoint;
    [SerializeField]
    float waitBetweenShots;
    [SerializeField]
    float bulletSpeed;
    
    Transform target;
    GameObject player;
    float dist;
    float shotCounter;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        target = player.GetComponent<Transform>();
    }

    private void Start()
    {
        shotCounter = waitBetweenShots;
    }

    private void Update()
    {
        dist = Vector2.Distance(transform.position, target.position);

        if (dist < 1000)
        {
            Shoot();
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
}
