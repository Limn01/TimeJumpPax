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
        if (controller.collider.tag == "Player")
        {
            Debug.Log("pie");
        }
    }

    private void Update()
    {
        if (controller.collider.tag == "Finish")
        {
            Debug.Log("Poo");
        }
    }
}
