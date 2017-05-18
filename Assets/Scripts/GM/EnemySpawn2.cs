using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn2 : EnemySpawn1
{
    //public Transform[] spawnPoints;

    GameObject[] commonEnemies;

    public override void SpawnEnemy()
    {
        for (int i = 0; i < transforms.Length; i++)
        {
            GameObject obj = EnemySpawn3Pool.current.GetPooledObject();
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
        commonEnemies = GameObject.FindGameObjectsWithTag("Enemy2");

        for (int i = 0; i < commonEnemies.Length; i++)
        {
            if (commonEnemies[i] != null)
            {
                commonEnemies[i].SetActive(false);
            }
        }
    }
}
