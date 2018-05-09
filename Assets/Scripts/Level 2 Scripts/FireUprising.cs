using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Com.LuisPedroFonseca.ProCamera2D;

public class FireUprising : MonoBehaviour
{
    public float speed;
    public float waitBetweenStart;
    public ProCamera2DCinematics cimematic;
    public Transform cam;
    public GameObject fire;
    public ParticleSystem fireParticle;
    public GameObject fireSection;

    float timer;
    int playerLayer;
    public bool timerStarted = false;

    private void Start()
    {
        playerLayer = LayerMask.NameToLayer("Player");
    }

    private void Update()
    {
        if (timerStarted)
        {
            Debug.Log("Timer Started");
            timer += Time.deltaTime;

            if (timer >= waitBetweenStart)
            {
                cimematic.Play();
                fireParticle.Play();
                timer = 0;
                timerStarted = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == playerLayer && !timerStarted)
        {
            timerStarted = true;
            fireSection.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == playerLayer)
        {
            timerStarted = false;
            cimematic.Stop();
            fireParticle.Stop();
            fireSection.SetActive(false);
        }
    }
}
