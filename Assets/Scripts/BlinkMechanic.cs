using System.Collections;
using UnityEngine;

public class BlinkMechanic : MonoBehaviour
{
    [SerializeField]
    float blinkDistance;
    [SerializeField]
    float blinkTime = 1f;
    [SerializeField]
    ParticleSystem blinkPartick;

    float blinkTimer;
    bool facingRight;
    bool canBlink = true;
    float blinkSpeed;
   
    private void Update()
    {
        if (Input.GetButtonDown("Blink") && canBlink)
        {
            canBlink = false;
            Blink();
            blinkPartick.Play();
        }
        
        if (!canBlink)
        {
            blinkTimer += Time.deltaTime;

            if (blinkTimer > blinkTime)
            {
                canBlink = true;
                blinkTimer = 0;
            }
        }

        if (transform.localScale.x == 1)
        {
            facingRight = true;
        }
        else
        {
            facingRight = false;
        }
    }

    void Blink()
    {
        Vector3 blink;
        if (facingRight)
        {
            blink = new Vector3(blinkDistance, 0, 0);
        }
            
        else
            blink = new Vector3(-blinkDistance, 0, 0);

        transform.position += blink;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + transform.localScale.x * Vector3.right * blinkDistance);
    }

}
