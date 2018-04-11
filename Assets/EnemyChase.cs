using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChase : MonoBehaviour
{
    public float moveSpeed;

    public Transform[] enemies;

    GameObject player;
    public bool atPoint;
    Animation clip;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        clip = player.GetComponent<Animation>();
    }

    private void Update()
    {
        if (atPoint)
        {
            for (int i = 0; i < enemies.Length; i++)
            {
                enemies[i].position = Vector3.MoveTowards(enemies[i].transform.position, player.transform.position, moveSpeed * Time.deltaTime);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            atPoint = true;
            clip.Play("PlayerShake");
        }
    }
}
