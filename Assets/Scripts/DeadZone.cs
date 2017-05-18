﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone : MonoBehaviour
{
    
    int damage = 16;

    
   void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            LevelManager.instance.RespawnPlayer();
            LifeManager.instance.TakeLife();
            PlayerHealth.instance.FullHealth();
           
        }
    }
}
