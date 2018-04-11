using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class FireShooter : Enemy
{
    public LayerMask playerLayer;
    public float bulletSpeed;
    public float waitBetweenShots;
    public Transform target;
    //public Transform shotPoint;
    public float verticalSpeed;
    public float amplitude;
    public float playerRange;

    bool playerInShootingRange;
    float timer;
    float distance;

    AIPath pathFinding;

    protected override void Awake()
    {
        base.Awake();

        pathFinding = GetComponent<AIPath>();
    }

    private void Start()
    {
        timer = waitBetweenShots;
    }

    protected override void Update()
    {
        base.Update();

        transform.Translate(0, Mathf.Sin(Time.realtimeSinceStartup * verticalSpeed) * amplitude, 0);

        playerInShootingRange = Physics2D.OverlapCircle(transform.position, playerRange, playerLayer);

        distance = (transform.position - target.position).sqrMagnitude;

        if (distance < 150)
        {
            pathFinding.canMove = true;
        }

        if (playerInShootingRange)
        {
            Debug.Log("Shooting");
            Shooting();
        }
    }

    void Shooting()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            Vector2 direction = target.position - transform.position;

            direction.Normalize();

            GameObject obj = EnemyBulletPool.current.GetPooledObject();
            List<GameObject> pooledObj = new List<GameObject>();
            pooledObj.Add(obj);
            int randomIndex = Random.Range(0, pooledObj.Count);
            if (!pooledObj[randomIndex].activeInHierarchy)
            {
                pooledObj[randomIndex].SetActive(true);
                pooledObj[randomIndex].transform.position = /*shotPoint.*/transform.position;
                pooledObj[randomIndex].transform.rotation = /*shotPoint.*/transform.rotation;
                pooledObj[randomIndex].GetComponent<Rigidbody2D>().AddForce(direction * bulletSpeed, ForceMode2D.Impulse);

                timer = waitBetweenShots;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, playerRange);
    }
}
