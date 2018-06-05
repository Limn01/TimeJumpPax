using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    public GameObject fireBall;
    public Transform shotPoint;
    public Transform target;
    public float maxJumpHeight;
    public float timeBetweenFire;
    public float playerRange;
    public LayerMask playerMask;
    public float fireballSpeed;
    public int fireballIndex = 2;

    int obstacleLayerIndex;
    bool playerInRange;
    public float timer;

    ObjectPooler objectPooler;

    private void Start()
    {
        objectPooler = ObjectPooler.SharedInstance;
        obstacleLayerIndex = LayerMask.NameToLayer("Player");
    }

    private void Update()
    {
        timer += Time.deltaTime;

        playerRange = (transform.position - target.position).sqrMagnitude;

        if (playerRange < 120)
        {
            if (timer >= timeBetweenFire)
            {
                ShootFireball();
            }
        }
    }

    void ShootFireball()
    {
        timer = 0;
        fireBall = objectPooler.GetPooledObject(fireballIndex);
        fireBall.SetActive(true);
        fireBall.transform.rotation = shotPoint.transform.rotation;
        fireBall.transform.position = shotPoint.transform.position;
        fireBall.GetComponent<Rigidbody2D>().AddForce(shotPoint.up * fireballSpeed * maxJumpHeight, ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == obstacleLayerIndex)
        {
            other.gameObject.SetActive(false);
        }
    }
}
