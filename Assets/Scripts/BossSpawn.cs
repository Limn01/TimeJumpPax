using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawn : MonoBehaviour
{
    public Transform spawnPoint;
    public GameObject boss;
    public GameObject wallBlock;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            boss.SetActive(true);
            wallBlock.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            boss.SetActive(false);
            wallBlock.SetActive(false);
        }
    }
}
