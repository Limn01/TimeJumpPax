using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeShooterScript : MonoBehaviour
{
    public float playerInRange;
    public float bulletSpeed;
    public Transform shotPoint;
    public float waitBetweenShots;

    PlayerMovement playerMovement;
    GameObject player;

    float shotCounter;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerMovement = player.GetComponent<PlayerMovement>();

        shotCounter = waitBetweenShots;
    }

    void Update()
    {
        //Debug.DrawLine(new Vector3(transform.position.x - playerInRange, transform.position.y, //transform.position.z),
        //    new Vector3(transform.position.x + playerInRange, transform.position.y, //transform.position.z));
        //
        shotCounter -= Time.deltaTime;

        if (playerMovement.transform.position.x > transform.position.x && playerMovement.transform.position.x < transform.position.x + playerInRange && shotCounter < 0)
        {
            GameObject obj = EnemyBulletPool.current.GetPooledObject();
            List<GameObject> pooledObj = new List<GameObject>();
            pooledObj.Add(obj);
            int randomIndex = Random.Range(0, pooledObj.Count);
            if (!pooledObj[randomIndex].activeInHierarchy)
            {
                pooledObj[randomIndex].SetActive(true);
                pooledObj[randomIndex].transform.position = shotPoint.transform.position;
                pooledObj[randomIndex].transform.rotation = transform.rotation;
                pooledObj[randomIndex].GetComponent<Rigidbody2D>().AddForce(shotPoint.right * bulletSpeed, ForceMode2D.Impulse);

                shotCounter = waitBetweenShots;
            }
        }

        if (playerMovement.transform.position.x < transform.position.x && playerMovement.transform.position.x > transform.position.x - playerInRange && shotCounter < 0)
        {
            GameObject obj = EnemyBulletPool.current.GetPooledObject();
            List<GameObject> pooledObj = new List<GameObject>();
            pooledObj.Add(obj);
            int randomIndex = Random.Range(0, pooledObj.Count);
            if (!pooledObj[randomIndex].activeInHierarchy)
            {
                pooledObj[randomIndex].SetActive(true);
                pooledObj[randomIndex].transform.position = shotPoint.transform.position;
                pooledObj[randomIndex].transform.rotation = transform.rotation;
                pooledObj[randomIndex].GetComponent<Rigidbody2D>().AddForce(-shotPoint.right * bulletSpeed, ForceMode2D.Impulse);

                shotCounter = waitBetweenShots;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(new Vector3(transform.position.x - playerInRange, transform.position.y, transform.position.z),
           new Vector3(transform.position.x + playerInRange, transform.position.y, transform.position.z));
    }
}
