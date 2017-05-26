using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    [SerializeField]
    GameObject lightingParticle;

    [SerializeField]
    GameObject chargePoint;

    [SerializeField]
    GameObject chargeParticle;

    [SerializeField]
    GameObject lightningPoint;

    [SerializeField]
    float particleDuration;

    [SerializeField]
    float timer;

    bool hasPlayed = false;
    bool particleOn = false;

	void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            LevelManager.instance.currentCheckPoint = gameObject;

            if (!particleOn)
            {
                TurnOnParticle();
                particleOn = true;
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (particleOn)
            {
                TurnOffParticle();
            }
            
        }
    }

    void TurnOnParticle()
    {
        GameObject lightingClone = Instantiate(lightingParticle, lightningPoint.transform.position, Quaternion.Euler(-90,0,0)) as GameObject;

        GameObject chargeClone = Instantiate(chargeParticle, chargePoint.transform.position, Quaternion.Euler(-90, 0, 0)) as GameObject;
    }

    void TurnOffParticle()
    {
        //lightingParticle = GameObject.FindGameObjectWithTag("Lighting");
        lightingParticle.SetActive(false);
    }
}
