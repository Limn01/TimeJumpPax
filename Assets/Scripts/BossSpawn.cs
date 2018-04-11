using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawn : MonoBehaviour
{
    public Transform spawnPoint;
    public GameObject boss;
    public GameObject wallBlock;

    AudioManager audio;

    private void Awake()
    {
        audio = FindObjectOfType<AudioManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            boss.SetActive(true);
            wallBlock.SetActive(true);
            audio.Play("BossMusic");
            audio.Stop("LevelMusic");
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
