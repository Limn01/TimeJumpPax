using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Com.LuisPedroFonseca.ProCamera2D
{
    public class DeadZone : MonoBehaviour
    {
        public GameObject gameOver;

        float restartTimer;
        float restartDelay = 2f;
        int damage = 16;
        GameObject player;
        LevelManager levelManager;
        AudioManager audioManager;

        void Awake()
        {
            player = GameObject.FindGameObjectWithTag("Player");
            levelManager = FindObjectOfType<LevelManager>();
            audioManager = FindObjectOfType<AudioManager>();
        }

        private void Update()
        {
            if (gameOver.activeInHierarchy)
            {
                restartTimer += Time.deltaTime;

                if (restartTimer >= restartDelay)
                {
                    SceneManager.LoadScene("MainMenu");
                }
            }
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (LifeManager.instance.lifeCounter != 0)
            {
                if (other.gameObject == player)
                {
                    audioManager.Play("Death");
                    //LevelManager.instance.StartCoroutine("RespawnPlayer");
                    levelManager.StartCoroutine("RespawnPlayer");
                }
            }
            else
            {
                audioManager.Play("Death");
                Debug.Log("Stopped respawn");
                levelManager.StopCoroutine("RespawnPlayer");
                gameOver.SetActive(true);
                player.SetActive(false);
            }
        }
    }
}


