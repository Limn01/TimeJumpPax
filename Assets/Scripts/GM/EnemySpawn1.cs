using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn1 : MonoBehaviour
{
    public Transform[] transforms;

    bool leftArea = false;
    GameObject[] enemies;

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
            leftArea = true;
            TurnEnemyOff();
        }
    }

    void SpawnEnemy()
    {
        for (int i = 0; i < transforms.Length; i++)
        {
            GameObject obj = EnemySpawnPool2.current.GetPooledObject();
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

    void TurnEnemyOff()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy1");

        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i] != null)
            {
                enemies[i].SetActive(false);
            }
        }
    }
}
