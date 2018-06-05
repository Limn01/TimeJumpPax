using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Com.LuisPedroFonseca.ProCamera2D;

[RequireComponent(typeof(Controller2D))]
public class Player : MonoBehaviour
{
    public float maxJumpHeight = 4;
    public float minJumpHeight = 1;
    public float timeToJumpApex = .4f;
    float accelerationTimeAirborne = .2f;
    float accelerationTimeGrounded = .1f;
    public float moveSpeed = 8;
    public float climbSpeed = 8;
    public Transform shotEnd;
    public float turnSmoothing = 15f;
    public Transform playerGraphic;

    public Vector2 wallJumpClimb;
    public Vector2 wallJumpOff;
    public Vector2 wallLeap;
    public float knockBack;
    public bool knockBackFromRight;
    public float knockBackCount;
    public float knockBackLength;
    public bool onLadder;
    public float dashSpeed = 30f;
    public float maxDash;

    public float wallSlideSpeedMax = 3;
    public float wallStickTime = .25f;
    float timeToWallUnstick;
    
    float gravity;
    float maxJumpVelocity;
    float minJumpVelocity;
    public Vector3 velocity;
    float velocityXSmoothing;
    float gravityStore;

    DashState dashState;

    Controller2D controller;
    
    Vector2 directionalInput;
    Vector2 dPadInput;
    bool wallSliding;
    int wallDirX;
    Rigidbody2D rb2;
    Animator anim;
    bool isGrounded;
    PlayerHealth playerHealth;
    PlayerInput playerInput;
    AudioManager audioManager;
    float climbVelocity;
    float xAxis = 90;
    float dpadX;
    float keyInput;
    float dpadInput;
    float dashTimer;
    
    bool doubleJump = false;
    public bool canDoubleJump;
    Quaternion targetRotation = Quaternion.Euler(0, 90, 0);

    private void Awake()
    {
        rb2 = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        playerHealth = FindObjectOfType<PlayerHealth>();
        audioManager = FindObjectOfType<AudioManager>();
        playerInput = GetComponent<PlayerInput>();
    }

    void Start()
    {
        controller = GetComponent<Controller2D>();

        gravity = -(2 * maxJumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        maxJumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
        minJumpVelocity = Mathf.Sqrt(2 * Mathf.Abs(gravity) * minJumpHeight);

        gravityStore = gravity;

        playerGraphic.rotation = Quaternion.Euler(0, 90, 0);
    }

    void Update()
    {
        CalculateVelocity();
        HandleWallSliding();
        Dashing();
        KnockBack();

        anim.SetFloat("Speed", Mathf.Abs(Input.GetAxisRaw("Horizontal")));

        controller.Move(velocity * Time.deltaTime, directionalInput);

        keyInput = Input.GetAxisRaw("Horizontal");
        dpadInput = Input.GetAxisRaw("DpadHorizontal");

        if (controller.collisions.above || controller.collisions.below)
        {
            velocity.y = 0;
        }

        // Caps the joystick input to be positive or negative - FUCKING RAW!
        keyInput = keyInput < 0 ? -1 : keyInput > 0 ? 1 : 0;

        if (keyInput < 0)
        {
            Rotation(keyInput);
            //shotEnd.localPosition = new Vector3(0f, 0f, 0);
            shotEnd.rotation = Quaternion.AngleAxis(180, Vector3.up);
        }

        if (keyInput > 0)
        {
            Rotation(keyInput);
            //shotEnd.localPosition = new Vector3(0.548f, 0.222f, 0);
            shotEnd.transform.rotation = Quaternion.AngleAxis(0, Vector3.up);
        }
        
        if (dpadInput < 0)
        {
            Rotation(dpadInput);
            //shotEnd.localPosition = new Vector3(-0.548f, 0.222f, 0);
            shotEnd.transform.rotation = Quaternion.AngleAxis(180, Vector3.up); 
            anim.SetFloat("Speed", Mathf.Abs(dpadInput));
        }

        if (dpadInput > 0)
        {
            Rotation(dpadInput);
            //shotEnd.localPosition = new Vector3(0.548f, 0.222f, 0);
            shotEnd.rotation = Quaternion.AngleAxis(0, Vector3.up);
            anim.SetFloat("Speed", Mathf.Abs(dpadInput));
        }

        if (controller.collisions.below)
        {
            anim.SetBool("IsGrounded", true);
        }
        else
        {
            anim.SetBool("IsGrounded", false);
        }

        //if (knockBackCount <= 0)
        //{
        //    velocity = new Vector2(velocity.x, velocity.y);
        //}
        //else
        //{
        //    if (knockBackFromRight)
        //    {
        //        velocity = new Vector2(-knockBack, knockBack);
        //    }
        //    if (!knockBackFromRight)
        //    {
        //        velocity = new Vector2(knockBack, knockBack);
        //    }
        //
        //    knockBackCount -= Time.deltaTime;
        //}

        LerpRotation();
    }

    public void SetDirectionalInput(Vector2 input)
    {
        directionalInput = input;
    }

    public void OnJumpInputDown()
    {
        if (wallSliding)
        {
            if (wallDirX == directionalInput.x)
            {
                velocity.x = -wallDirX * wallJumpClimb.x;
                velocity.y = wallJumpClimb.y;
            }
            else if (directionalInput.x == 0)
            {
                velocity.x = -wallDirX * wallJumpOff.x;
                velocity.y = wallJumpOff.y;
            }
            else
            {
                velocity.x = -wallDirX * wallLeap.x;
                velocity.y = wallLeap.y;
            }

            doubleJump = false;
        }
        if (controller.collisions.below)
        {
            velocity.y = maxJumpVelocity;
            audioManager.Play("PlayerJump");
            doubleJump = false;
            
        }
        if (canDoubleJump && !controller.collisions.below && !doubleJump && !wallSliding)
        {
            velocity.y = maxJumpVelocity / 1f;
            doubleJump = true;
            audioManager.Play("DoubleJump");
        }
    }

    public void OnJumpInputUp()
    {
        if (velocity.y > minJumpVelocity)
        {
            velocity.y = minJumpVelocity;
        }
    }

    void HandleWallSliding()
    {
        wallDirX = (controller.collisions.left) ? -1 : 1;
        wallSliding = false;
        if ((controller.collisions.left || controller.collisions.right) && !controller.collisions.below && velocity.y < 0)
        {
            wallSliding = true;

            if (velocity.y < -wallSlideSpeedMax)
            {
                velocity.y = -wallSlideSpeedMax;
            }

            if (timeToWallUnstick > 0)
            {
                velocityXSmoothing = 0;
                velocity.x = 0;

                if (directionalInput.x != wallDirX && directionalInput.x != 0)
                {
                    timeToWallUnstick -= Time.deltaTime;
                }
                else
                {
                    timeToWallUnstick = wallStickTime;
                }
            }
            else
            {
                timeToWallUnstick = wallStickTime;
            }
        }
    }

    void CalculateVelocity()
    {
        float targetVelocityX = directionalInput.x * moveSpeed;
        velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne);
        velocity.y += gravity * Time.deltaTime;
    }

    void Rotation(float input)
    {
        if (velocity != Vector3.zero && input != 0)
        {
            targetRotation = Quaternion.Euler(0, 90 * input, 0);
        }
    }

    void LerpRotation()
    {
        Quaternion newRotation = Quaternion.Lerp(playerGraphic.rotation, targetRotation, turnSmoothing * Time.deltaTime);
        playerGraphic.rotation = newRotation;
    }

    void Dashing()
    {
        switch (dashState)
        {
            case DashState.Ready:

                var isDahingKeyDown = Input.GetAxis("Dash");
    
                if (isDahingKeyDown == 1)
                {
                    if (playerGraphic.rotation.y > 0)
                    {
                        velocity = new Vector3(dashSpeed, velocity.y, velocity.z);
                        dashState = DashState.Dashing;
                    }

                    if (playerGraphic.rotation.y < 0)
                    {
                        velocity = new Vector3(-dashSpeed, velocity.y, velocity.z);
                        dashState = DashState.Dashing;
                    }
                }
                break;

            case DashState.Dashing:

                dashTimer += Time.deltaTime * 3;

                if (dashTimer >= maxDash)
                {
                    dashTimer = maxDash;
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

    void KnockBack()
    {
        if (knockBackCount <= 0)
        {
            velocity = new Vector2(velocity.x, velocity.y);
        }
        else
        {
            if (knockBackFromRight)
            {
                velocity = new Vector2(-knockBack, knockBack);
            }
            if (!knockBackFromRight)
            {
                velocity = new Vector2(knockBack, knockBack);
            }

            knockBackCount -= Time.deltaTime;
        }
    }
}

public enum DashState
{
    Ready,
    Dashing,
    Cooldown
}
