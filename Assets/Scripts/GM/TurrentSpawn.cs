using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurrentSpawn : EnemySpawn
{
    GameObject enemy;

    public override void SpawnEnemy()
    {
        GameObject obj = TurrentEnemyPool.current.GetPooledObject();
        List<GameObject> pooledObj = new List<GameObject>();
        pooledObj.Add(obj);
        int randomIndex = Random.Range(0, pooledObj.Count);

            
        {
            pooledObj[randomIndex].SetActive(true);
            pooledObj[randomIndex].transform.position = transform.position;
            pooledObj[randomIndex].transform.rotation = transform.rotation;

        }

    }

    public override void TurnEnemyOff()
    {
        enemy = GameObject.FindGameObjectWithTag("Enemy3");

        if (enemy != null)
        {
            enemy.SetActive(false);
        }
    }
}
