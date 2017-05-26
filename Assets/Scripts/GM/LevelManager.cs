﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    public GameObject currentCheckPoint;

    //PlayerHealth playerHealth;
    GameObject player;
	// Use this for initialization
    void Awake()
    {
        instance = this;
    }

	void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        //playerHealth = player.GetComponent<PlayerHealth>();
	}

    public void RespawnPlayer()
    {
        StartCoroutine("RestartGame");
    }

    public IEnumerator RestartGame()
    {
        player.gameObject.SetActive(false);
        yield return new WaitForSeconds(1f);
        player.transform.position = currentCheckPoint.transform.position;
        PlayerHealth.instance.FullHealth();
        LifeManager.instance.TakeLife();
        player.gameObject.SetActive(true);
    }
}
