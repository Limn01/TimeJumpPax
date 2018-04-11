using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    public GameObject fireBall;
    public Transform shotPoint;
    public Transform target;
    public GameObject wireSphere;
    public float maxJumpHeight;
    public float timeBetweenFire;
    public float playerRange;
    public LayerMask playerMask;
    public float fireballSpeed;

    bool playerInRange;
    float timer;
    
    GameObject player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
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

        GameObject obj = FireBallPool.current.GetPooledObject();
        List<GameObject> pooledObj = new List<GameObject>();
        pooledObj.Add(obj);
        int randomIndex = Random.Range(0, pooledObj.Count);
        if (!pooledObj[randomIndex].activeInHierarchy)
        {
            pooledObj[randomIndex].SetActive(true);
            pooledObj[randomIndex].transform.position = shotPoint.transform.position;
            pooledObj[randomIndex].transform.rotation = transform.rotation;
            pooledObj[randomIndex].GetComponent<Rigidbody2D>().AddForce(shotPoint.up * fireballSpeed * maxJumpHeight, ForceMode2D.Impulse);
        }
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.white;
    //    Gizmos.DrawLine(new Vector3(transform.position.x - 120, transform.position.y, transform.position.z),
    //        new Vector3(transform.position.x + 120, transform.position.y, transform.position.z));
    //}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 9)
        {
            other.gameObject.SetActive(false);
        }
    }
}
