using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Com.LuisPedroFonseca.ProCamera2D;

public class Spikes : MonoBehaviour
{
    public GameObject gameOver;

    float restartTimer;
    float restartDelay = 2f;
    GameObject player;
    LevelManager levelManager;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        levelManager = FindObjectOfType<LevelManager>();
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
                levelManager.StartCoroutine("RespawnPlayer");
            }
        }
        else
        {
            levelManager.StopCoroutine("RespawnPlayer");
            player.gameObject.SetActive(false);
            gameOver.SetActive(true);
        }
    }
}



