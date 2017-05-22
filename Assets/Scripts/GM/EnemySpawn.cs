using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public Transform tramsform;

    GameObject enemy;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            SpawnEnemy();
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            TurnEnemyOff();
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
                pooledObj[randomIndex].transform.position = transform.position;
                pooledObj[randomIndex].transform.rotation = transform.rotation;

            }
        
    }

    void TurnEnemyOff()
    {
        enemy = GameObject.FindGameObjectWithTag("Enemy");

        if (enemy != null)
        {
            enemy.SetActive(false);
        }
    }    
}
