using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    GameObject player;
    Player playerMovement;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerMovement = player.GetComponent<Player>();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.layer == 8)
        {
            playerMovement.onLadder = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == 8)
        {
            playerMovement.onLadder = false;
        }
    }
}
