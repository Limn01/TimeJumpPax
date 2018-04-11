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

    public float wallSlideSpeedMax = 3;
    public float wallStickTime = .25f;
    float timeToWallUnstick;
    
    float gravity;
    float maxJumpVelocity;
    float minJumpVelocity;
    public Vector3 velocity;
    float velocityXSmoothing;
    float gravityStore;

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
    float joystickAngle = 90;
    
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

        anim.SetFloat("Speed", Mathf.Abs(Input.GetAxisRaw("Horizontal")));

        controller.Move(velocity * Time.deltaTime, directionalInput);

        float keyInput = Input.GetAxisRaw("Horizontal");
        float dpadInput = Input.GetAxisRaw("DpadHorizontal");

        //Vector2 keyInput = new Vector2(Input.GetAxisRaw("Horizonatl"), 0);

        if (controller.collisions.above || controller.collisions.below)
        {
            velocity.y = 0;
        }

        if (Mathf.Abs(keyInput) < .1)
        {
            //Rotation(keyInput);
            //shotEnd.localPosition = new Vector3(0.548f, 0.222f, 0);
            shotEnd.rotation = Quaternion.AngleAxis(180, Vector3.up);
        }

        if (Mathf.Abs(keyInput) >= .1)
        {
            xAxis = Input.GetAxisRaw("Horizontal");

            Rotation(xAxis);
            shotEnd.localPosition = new Vector3(0.548f, 0.222f, 0);
            shotEnd.transform.rotation = Quaternion.AngleAxis(0, Vector3.up);
        }
        
        if (dpadInput < -0.01f)
        {
            Rotation(dpadInput);
            shotEnd.transform.rotation = Quaternion.AngleAxis(180, Vector3.up); 
            anim.SetFloat("Speed", Mathf.Abs(dpadInput));
        }

        if (Mathf.Abs(dpadInput) >= 0.1)
        {
            dpadX = Input.GetAxisRaw("Horizontal");
            Rotation(dpadX);
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
            velocity.y = maxJumpVelocity / 1.3f;
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
        joystickAngle = Mathf.Atan2(xAxis,0) * Mathf.Rad2Deg;
        
        Quaternion newRotation = Quaternion.Lerp(playerGraphic.rotation, Quaternion.Euler(/*targetRotation*/0,joystickAngle ,0), turnSmoothing * Time.deltaTime);
        playerGraphic.rotation = newRotation;
    }
}
