using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.LuisPedroFonseca.ProCamera2D
{
    public class CheckPoint : MonoBehaviour
    {
        LevelManager levelManager;

        private void Awake()
        {
            levelManager = FindObjectOfType<LevelManager>();
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.tag == "Player")
            {
                levelManager.currentCheckPoint = gameObject;
                //LevelManager.instance.currentCheckPoint = gameObject;
            }
        }
    }
}

