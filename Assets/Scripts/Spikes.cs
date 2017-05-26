using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    GameObject player;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

   void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == player )
        {
            LevelManager.instance.RespawnPlayer();
        }
    }
}
