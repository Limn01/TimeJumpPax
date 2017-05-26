    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public static EnemySpawn instance;

    public Transform tranform;

    GameObject enemy;
    GameObject player;

    void Awake()
    {
        instance = this;
        //enemy = GameObject.FindGameObjectWithTag("Enemy");
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject == player)
        {
            SpawnEnemy();
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject == player)
        {
            OnDisable();
            Debug.Log("Disable Enemy");
            
            
        }
    }

    void SpawnEnemy()
    {
       
            GameObject obj = EnemySpawn1Pool.current.GetPooledObject();
            List<GameObject> pooledObj = new List<GameObject>();
            pooledObj.Add(obj);
            int randomIndex = Random.Range(0, pooledObj.Count);

            if (!pooledObj[randomIndex].activeInHierarchy)
            {
                pooledObj[randomIndex].SetActive(true);
                pooledObj[randomIndex].transform.position = tranform.position;
                pooledObj[randomIndex].transform.rotation = tranform.rotation;

            }
        
    }

    public void OnDisable()
    {
        enemy = GameObject.FindGameObjectWithTag("Enemy");

        if (enemy != null)
        {
            enemy.SetActive(false);
        }
    }    
}
