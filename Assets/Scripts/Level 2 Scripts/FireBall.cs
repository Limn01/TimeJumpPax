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
    public int fireballIndex = 1;

    bool playerInRange;
    public float timer;

    GenericObjectPooler oP;
    GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        oP = GenericObjectPooler.SharedInstance;
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
        fireBall = oP.GetPooledObject(fireballIndex);
        fireBall.SetActive(true);
        fireBall.transform.rotation = shotPoint.transform.rotation;
        fireBall.transform.position = shotPoint.transform.position;
        fireBall.GetComponent<Rigidbody2D>().AddForce(shotPoint.up * fireballSpeed * maxJumpHeight, ForceMode2D.Impulse);
        
        //objectPooler.SpawnFromPool("Fireball", shotPoint.position, shotPoint.rotation);
        //GameObject obj = FireBallPool.current.GetPooledObject();
        //List<GameObject> pooledObj = new List<GameObject>();
        //pooledObj.Add(obj);
        //int randomIndex = Random.Range(0, pooledObj.Count);
        //if (!pooledObj[randomIndex].activeInHierarchy)
        //{
        //    pooledObj[randomIndex].SetActive(true);
        //    pooledObj[randomIndex].transform.position = shotPoint.transform.position;
        //    pooledObj[randomIndex].transform.rotation = transform.rotation;
        //    pooledObj[randomIndex].GetComponent<Rigidbody2D>().AddForce(shotPoint.up * fireballSpeed * maxJumpHeight, ForceMode2D.Impulse);
        //}
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
