using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformDestroy : MonoBehaviour
{
    public DissolveState dissolveState;
    public float waitBetweenDrop;
    public ParticleSystem leaveParticle;
    public float dissolveRate;

    int playerLayer;
    public float timer;
    public float dissolveSpeed;

    bool landedOn;
    public bool dissolveStarted = false;
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
        if (dissolveStarted)
        {
            dissolveRate = Mathf.Lerp(dissolveRate, 1f, dissolveSpeed * Time.deltaTime);
            instaceMat.SetFloat("_DissolveAmount", dissolveRate);
        }
        else if (!dissolveStarted)
        {
            dissolveRate = 0f;
            instaceMat.SetFloat("_DissolveAmount", 0f);
        }

        switch (dissolveState)
        {
            case DissolveState.Ready:

                timerStarted = false;
                dissolveStarted = false;
                
                dissolveState = DissolveState.Dissolve;
                break;

            case DissolveState.Dissolve:

                if (timerStarted && dissolveStarted)
                {
                    timer += Time.deltaTime;

                    if (timer >= waitBetweenDrop)
                    {
                        boxCol.isTrigger = true;
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
                    
                    dissolveState = DissolveState.Ready;
                }
                break;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer == playerLayer && !timerStarted && !dissolveStarted)
        {
            timerStarted = true;
            dissolveStarted = true;
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
