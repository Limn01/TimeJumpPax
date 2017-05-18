using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemySpawn : EnemySpawn1
{
    //public Transform[] spawnPoints;

    GameObject[] enemies;

    public override void SpawnEnemy()
    {
        for (int i = 0; i < transforms.Length; i++)
        {
            GameObject obj = FlyingEnemyPool.current.GetPooledObject();
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

    public override void TurnEnemyOff()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy3");

        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i] != null)
            {
                enemies[i].SetActive(false);
            }
        }
    }
}
