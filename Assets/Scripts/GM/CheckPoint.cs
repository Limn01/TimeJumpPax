using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public AudioSource activate;

    bool hasPlayed = false;

	void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            LevelManager.instance.currentCheckPoint = gameObject;

            if (!hasPlayed)
            {
                activate.Play();
                hasPlayed = true;
            }
        }
    }
}
