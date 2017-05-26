using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour
{
    [SerializeField]
    AudioSource source;

    bool isPaused;

	// Update is called once per frame
	void Update ()
    {
        if (isPaused)
        {
            Time.timeScale = 0f;
            
        }

        else
        {
            Time.timeScale = 1f;
           
        }

        if (Input.GetButtonDown("Pause"))
        {
            isPaused = !isPaused;
            source.Play();
        }
	}
}
