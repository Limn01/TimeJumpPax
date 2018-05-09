using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    public DashState dashState;
    public float dashTimer;
    public float maxDash = 20f;
    public float dashSpeed;
    public float dashLength;
    public GameObject dashParticle;
    public Transform playerGraphic;

    public Vector2 savedVelocity;
    
    Rigidbody2D rb2D;
    Player playerMovement;

    private void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
        playerMovement = GetComponent<Player>();
    }

    private void Update()
    {
        switch (dashState)
        {
            case DashState.Ready:
                var isDashingKeyDown = Input.GetAxis("Dash");
                if (isDashingKeyDown == 1)
                {
                    if (playerMovement.velocity.x >= 1)
                    {
                        playerMovement.velocity = new Vector3(dashSpeed /*+ dashLength*/, playerMovement.velocity.y);
                        dashParticle.SetActive(true);
                        //transform.position = transform.Translate(Vector3.right * Time.deltaTime, 0, 0);
                        dashState = DashState.Dashing;
                    }

                    if (playerMovement.velocity.x < 1)
                    {
                        //savedVelocity = rb2D.velocity;
                        //savedVelocity = playerMovement.velocity;
                        //rb2D.AddForce(Vector2.left * 7, ForceMode2D.Impulse);
                        playerMovement.velocity = new Vector3(-dashSpeed /*+ dashLength*/, playerMovement.velocity.y);
                        dashParticle.SetActive(true);
                        dashState = DashState.Dashing;
                    }
                    
                }
                break;

            case DashState.Dashing:
                dashTimer += Time.deltaTime * 3;
                if (dashTimer >= maxDash)
                {
                    dashTimer = maxDash;
                    dashParticle.SetActive(false);
                    //rb2D.velocity = savedVelocity;
                    //playerMovement.velocity = savedVelocity;
                    dashState = DashState.Cooldown;
                }
                break;

            case DashState.Cooldown:
                dashTimer -= Time.deltaTime;
                if (dashTimer <= 0)
                {
                    dashTimer = 0;
                    
                    dashState = DashState.Ready;
                }
                break;
        }
    }
}

//public enum DashState
//{
//    Ready,
//    Dashing,
//    Cooldown
//}
