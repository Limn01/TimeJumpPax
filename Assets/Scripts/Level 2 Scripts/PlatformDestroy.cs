using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformDestroy : MonoBehaviour
{
    public DissolveState dissolveState;
    public float waitBetweenDrop;
    public ParticleSystem leaveParticle;

    int playerLayer;
    public float timer;
    float dissolveSpeed = 1;

    bool landedOn;
    bool timerStarted = false;

    BoxCollider2D boxCol;
    Renderer dissolveMat;
    Material instaceMat;

    private void Awake()
    {
        playerLayer = LayerMask.NameToLayer("Player");
        boxCol = GetComponent<BoxCollider2D>();
        dissolveMat = gameObject.GetComponent<Renderer>();
        instaceMat = dissolveMat.material;
    }

    private void Update()
    {
        switch (dissolveState)
        {
            case DissolveState.Ready:

                timerStarted = false;
                dissolveState = DissolveState.Dissolve;
                break;

            case DissolveState.Dissolve:

                if (timerStarted)
                {
                    timer += Time.deltaTime;

                    if (timer >= waitBetweenDrop)
                    {
                        boxCol.isTrigger = true;
                        //dissolveMat.enabled = false;
                        dissolveMat.material.
                        
                        dissolveState = DissolveState.Cooldown;
                    }
                    
                }
                break;

            case DissolveState.Cooldown:

                timer -= Time.deltaTime;

                if (timer <= 0)
                {
                    timer = 0;
                    boxCol.isTrigger = false;
                    dissolveMat.enabled = true;
                    dissolveState = DissolveState.Ready;
                }
                break;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer == playerLayer && !timerStarted)
        {
            timerStarted = true;
            leaveParticle.Play();
        }
    }
}

public enum DissolveState
{
    Ready,
    Dissolve,
    Cooldown
}
