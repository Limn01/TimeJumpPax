using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnemySpawn : MonoBehaviour
{
    public bool hasBeenActivated = false;
    public GameObject[] enemiesToModify;
   
    //List<GameObject> enemies = new List<GameObject>();
    GameObject player;
    int playerLayerIndex;

    void Awake()
    {
        //enemy = GameObject.FindGameObjectWithTag("Enemy");
        player = GameObject.FindGameObjectWithTag("Player");
        playerLayerIndex = LayerMask.NameToLayer("Player");

        foreach (GameObject enemy in enemiesToModify)
        {
            enemy.SetActive(false);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == playerLayerIndex)
        {
            SetEnemyActivationState(true);
            Debug.Log("Spawning");
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.layer == playerLayerIndex)
        {
            SetEnemyActivationState(false);
            Debug.Log("Turn off enemy");
        }
    }

    //void SpawnEnemy()
    //{
    //   
    //        GameObject obj = EnemySpawn1Pool.current.GetPooledObject();
    //    enemies.Add(obj);
    //        List<GameObject> pooledObj = new List<GameObject>();
    //        pooledObj.Add(obj);
    //        int randomIndex = Random.Range(0, pooledObj.Count);
    //
    //        if (!pooledObj[randomIndex].activeInHierarchy)
    //        {
    //            pooledObj[randomIndex].SetActive(true);
    //            pooledObj[randomIndex].transform.position = tranform.position;
    //            pooledObj[randomIndex].transform.rotation = tranform.rotation;
    //
    //        }
    //}

    //void FilterList()
    //{
    //    enemies = enemies.Where(item => item != null).ToList();
    //}
    //
    //public void TurnOffEnemy()
    //{
    //    FilterList();
    //
    //    for (int i = 0; i < enemies.Count; i++)
    //    {
    //        enemies[i].SetActive(false);
    //    }
    //}  
    
    void SetEnemyActivationState(bool activate)
    {
        if (activate == true)
        {
            foreach (GameObject enemy in enemiesToModify)
            {
                enemy.SetActive(true);
            }
        }
        else
        {
            foreach (GameObject enemy in enemiesToModify)
            {
                enemy.SetActive(false);
            }
        }

        hasBeenActivated = true;
    }  
}
