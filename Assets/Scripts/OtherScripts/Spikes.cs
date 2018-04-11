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
        if (LifeManager.instance.lifeCounter > 0)
        {
            if (other.gameObject.layer == 8)
            {
                audioManager.Play("Death");
                levelManager.StartCoroutine("RespawnPlayer");
            }
        }
        else
        {
            audioManager.Play("Death");
            levelManager.StopCoroutine("RespawnPlayer");
            player.gameObject.SetActive(false);
            gameOver.SetActive(true);
        }
    }
}



