using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnemySpawn : MonoBehaviour
{
    public static EnemySpawn instance;

    public Transform tranform;
    [SerializeField]
    List<GameObject> enemies = new List<GameObject>();
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
           TurnOffEnemy();
            Debug.Log("Disable Enemy");
            
            
        }
    }

    void SpawnEnemy()
    {
       
            GameObject obj = EnemySpawn1Pool.current.GetPooledObject();
        enemies.Add(obj);
            List<GameObject> pooledObj = new List<GameObject>();
            pooledObj.Add(obj);
            int randomIndex = Random.Range(0, pooledObj.Count);

            if (!pooledObj[randomIndex].activeInHierarchy)
            {
                pooledObj[randomIndex].SetActive(true);
                pooledObj[randomIndex].transform.position = tranform.position;
                pooledObj[randomIndex].transform.rotation = tranform.rotation;

            }

        GameObject ob = PatrolEnemyPool.current.GetPooledObject();
        enemies.Add(ob);
        List<GameObject> patrolObj = new List<GameObject>();
        patrolObj.Add(ob);
        int patrolIndex = Random.Range(0, patrolObj.Count);

        if (!patrolObj[patrolIndex].activeInHierarchy)
        {
            patrolObj[patrolIndex].SetActive(true);
            patrolObj[patrolIndex].transform.position = transform.position;
            patrolObj[patrolIndex].transform.rotation = transform.rotation;
        }

    }

    void FilterList()
    {
        enemies = enemies.Where(item => item != null).ToList();
    }

    public void TurnOffEnemy()
    {
        FilterList();

        for (int i = 0; i < enemies.Count; i++)
        {
            enemies[i].SetActive(false);
        }
    }    
}
