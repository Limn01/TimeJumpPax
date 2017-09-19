using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingTriggers : MonoBehaviour
{
    GameObject player;
    Controller2D controller;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        controller = player.GetComponent<Controller2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == player)
        {
            Debug.Log("shit");
        }
    }
}
