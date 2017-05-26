using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone : MonoBehaviour
{
    int damage = 16;
    GameObject player;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    
   void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == player)
        {
            player.SetActive(false);
            LevelManager.instance.RespawnPlayer();
           
        }
    }
}
