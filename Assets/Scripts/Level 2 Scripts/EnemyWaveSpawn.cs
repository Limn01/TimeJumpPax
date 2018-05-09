using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWaveSpawn : MonoBehaviour
{
    public Transform[] points;
    public float waitBetweenSpawn;

    int playerLayer;
    int enemyLayer;
    bool timerStarted = false;
    float timer;
    int fireflyIndex = 2;

    GenericObjectPooler objectPooler;

    private void Start()
    {
        playerLayer = LayerMask.NameToLayer("Player");
        enemyLayer = LayerMask.NameToLayer("Enemy");
        objectPooler = GenericObjectPooler.SharedInstance;
    }

    private void Update()
    {
        if (timerStarted)
        {
            timer += Time.deltaTime;

            if (timer >= waitBetweenSpawn)
            {
                Spawn();
                timer = 0;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == playerLayer && !timerStarted)
        {
            timerStarted = true;
        }

        if (other.gameObject.layer == enemyLayer)
        {
            other.gameObject.SetActive(false);
        }
    }

    void Spawn()
    {
        for (int i = 0; i < points.Length; i++)
        {
            GameObject firefly = objectPooler.GetPooledObject(fireflyIndex);
            firefly.SetActive(true);
            firefly.transform.position = points[i].transform.position;
            firefly.transform.rotation = points[i].transform.rotation;
        }
    }
}
