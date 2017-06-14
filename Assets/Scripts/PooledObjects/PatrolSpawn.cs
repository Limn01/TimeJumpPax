using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Com.LuisPedroFonseca.ProCamera2D;
using System.Linq;

public class PatrolSpawn : MonoBehaviour
{
    //public static PatrolSpawn instance;

    public Transform tranform;
    [SerializeField]
    List<GameObject> enemies = new List<GameObject>();
    GameObject player;
    
    GameObject patrol;
    PatrolEnemy patrolEnemy;

    void Awake()
    {
        //patrol = GameObject.FindGameObjectWithTag("Patrol");
        //patrolEnemy = patrol.GetComponent<PatrolEnemy>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject == player)
        {
            SpawnEnemy();
           // patrolEnemy.StartCoroutine("Patrol");
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

        GameObject obj = PatrolEnemyPool.current.GetPooledObject();
        enemies.Add(obj);
        List<GameObject> pooledObj = new List<GameObject>();
        pooledObj.Add(obj);
        int randomIndex = Random.Range(0, pooledObj.Count);

        if (!pooledObj[randomIndex].activeInHierarchy)
        {
            pooledObj[randomIndex].SetActive(true);
            pooledObj[randomIndex].transform.position = tranform.position;
            pooledObj[randomIndex].transform.rotation = tranform.rotation;
            //patrolEnemy.StartCoroutine("Patrol");

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
