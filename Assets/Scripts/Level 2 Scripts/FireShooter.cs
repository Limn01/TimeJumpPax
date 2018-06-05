using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class FireShooter : Enemy
{
    public LayerMask PlayerLayer;
    public float bulletSpeed;
    public float waitBetweenShots;
    //public Transform shotPoint;
    public float verticalSpeed;
    public float amplitude;
    public float playerRange;
    public GameObject fireBall;

    bool playerInShootingRange;
    float timer;
    public float distance;
    int enemyProjectileIndex = 3;

    Transform target;
    AIPath pathFinding;
    ObjectPooler objectPooler;
    Rigidbody2D rb;

    protected override void Awake()
    {
        base.Awake();

        pathFinding = GetComponent<AIPath>();
        target = player.GetComponent<Transform>();
        
    }

    private void Start()
    {
        timer = waitBetweenShots;
        objectPooler = ObjectPooler.SharedInstance;
    }

    protected override void Update()
    {

        base.Update();

        transform.Translate(0, Mathf.Sin(Time.realtimeSinceStartup * verticalSpeed) * amplitude, 0);

        playerInShootingRange = Physics2D.OverlapCircle(transform.position, playerRange, PlayerLayer);

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

            fireBall = objectPooler.GetPooledObject(enemyProjectileIndex);
            fireBall.transform.position = transform.position;
            fireBall.transform.rotation = transform.rotation;
            fireBall.SetActive(true);
            fireBall.GetComponent<Rigidbody2D>().AddForce(direction * bulletSpeed, ForceMode2D.Impulse);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, playerRange);
    }
}
