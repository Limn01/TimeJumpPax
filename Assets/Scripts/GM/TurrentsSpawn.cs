using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurrentsSpawn : EnemySpawn1
{
    //public Transform[] SpawnPoints;

    GameObject[] turrentEnemies;


    public override void SpawnEnemy()
    {
        for (int i = 0; i < transforms.Length; i++)
        {
            GameObject obj = TurrentEnemyPool.current.GetPooledObject();
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
        turrentEnemies = GameObject.FindGameObjectsWithTag("Turrent");

        for (int i = 0; i < turrentEnemies.Length; i++)
        {
            if (turrentEnemies[i] != null)
            {
                turrentEnemies[i].SetActive(false);
            }
        }
    }
}
