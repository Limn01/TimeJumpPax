﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class HoverEnemySpawn : MonoBehaviour
{
    public Transform[] transforms;
 
    List<GameObject> enemies = new List<GameObject>();

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
           
        }
    }

    void SpawnEnemy()
    {
        for (int i = 0; i < transforms.Length; i++)
        {
            GameObject obj = HoverEnemyPool.current.GetPooledObject();
            enemies.Add(obj);
            List<GameObject> pooledObj = new List<GameObject>();
            pooledObj.Add(obj);
            int randomIndex = Random.Range(0, pooledObj.Count);

            if (!pooledObj[randomIndex].activeInHierarchy)
            {
                pooledObj[randomIndex].SetActive(true);
                pooledObj[randomIndex].transform.position = transforms[i].position;
                pooledObj[randomIndex].transform.rotation = transforms[i].rotation;

            }
        }
    }

    void FilterList()
    {
        enemies = enemies.Where(item => item != null).ToList();
    }

    void TurnEnemyOff()
    {
        FilterList();

        for (int i = 0; i < enemies.Count; i++)
        {
            if (enemies[i] != null)
            {
                enemies[i].SetActive(false);
            }
        }
    }
}
