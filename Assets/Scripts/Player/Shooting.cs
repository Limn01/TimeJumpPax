using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public float bulletSpeed;
    public Transform shotEnd;
    public float timeBetweenShots;
  
    float cooldownTimer = 100f;
    float timer;
    public int playerBulletIndex = 0;

    ObjectPooler objectPooler;
    AudioManager audioManager;

    private void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }

    private void Start()
    {
        objectPooler = ObjectPooler.SharedInstance;
    }

    void Update()
    { 
        timer += Time.deltaTime;

        if (Input.GetButtonDown("Fire1"))
        {
            if (timer > timeBetweenShots)
            {
                Shoot();
                audioManager.Play("PlayerShoot");
                timer = 0;
            }
        }
    }

    void Shoot()
    {
        GameObject playerBullet = objectPooler.GetPooledObject(playerBulletIndex);
        playerBullet.SetActive(true);
        playerBullet.transform.position = shotEnd.transform.position;
        playerBullet.transform.rotation = transform.rotation;
        playerBullet.GetComponent<Rigidbody2D>().AddForce(shotEnd.right * bulletSpeed, ForceMode2D.Impulse);

       
        //GameObject obj = PlayerBulletPooling.current.GetPooledObject(); 
        //List<GameObject> pooledObj = new List<GameObject>();
        //pooledObj.Add(obj);
        //int randomIndex = Random.Range(0, pooledObj.Count);
        //if (!pooledObj[randomIndex].activeInHierarchy)
        //{
        //    pooledObj[randomIndex].SetActive(true);
        //    pooledObj[randomIndex].transform.position = shotEnd.transform.position;
        //    pooledObj[randomIndex].transform.rotation = transform.rotation;
        //    pooledObj[randomIndex].GetComponent<Rigidbody2D>().AddForce(shotEnd.right * bulletSpeed, ForceMode2D.Impulse);
        //}
    }
}
