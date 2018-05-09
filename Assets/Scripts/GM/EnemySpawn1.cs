using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnemySpawn1 : MonoBehaviour
{
    [System.Serializable]
    public class Room
    {
        public GameObject[] enemyType;
        public Transform[] points;
        public int enemyAmount;
    }

    public Room[] rooms;
    public Transform[] transforms;
    public List<GameObject> enemies = new List<GameObject>();
    public int enemyIndex;
    public GameObject enemy;

    int playerLayer;

    GenericObjectPooler objectPooler;

    private void Start()
    {
        playerLayer = LayerMask.NameToLayer("Player");
        objectPooler = GenericObjectPooler.SharedInstance;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == playerLayer)
        {
            SpawnEnemy();
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.layer == playerLayer)
        {
            TurnEnemyOff();
        }
    }

    void SpawnEnemy(Room _rooms)
    {
        for (int i = 0; i < _rooms.enemyType.Length; i++)
        {
            enemy= objectPooler.GetPooledObject(enemyIndex);
            enemies.Add(enemy);
            enemy.transform.position = transforms[i].transform.position;
            enemy.transform.rotation = transforms[i].rotation;
            enemy.SetActive(true);  
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
