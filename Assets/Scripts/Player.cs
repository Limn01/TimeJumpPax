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
    public Transform shotEnd;

    public Vector2 wallJumpClimb;
    public Vector2 wallJumpOff;
    public Vector2 wallLeap;
    public float knockBack;
    public bool knockBackFromRight;
    public float knockBackCount;
    public float knockBackLength;

    public float wallSlideSpeedMax = 3;
    public float wallStickTime = .25f;
    float timeToWallUnstick;

    float gravity;
    float maxJumpVelocity;
    float minJumpVelocity;
    Vector3 velocity;
    float velocityXSmoothing;

    Controller2D controller;

    Vector2 directionalInput;
    bool wallSliding;
    int wallDirX;
    Rigidbody2D rb;
    //Animator anim;
    bool isGrounded;
    PlayerHealth playerHealth;
    AudioManager audioManager;
    
    bool doubleJump = false;
    public bool canDoubleJump;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        //anim = GetComponentInChildren<Animator>();
        playerHealth = FindObjectOfType<PlayerHealth>();
        audioManager = FindObjectOfType<AudioManager>();
    }

    void Start()
    {
        controller = GetComponent<Controller2D>();

        gravity = -(2 * maxJumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        maxJumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
        minJumpVelocity = Mathf.Sqrt(2 * Mathf.Abs(gravity) * minJumpHeight);
    }

    void Update()
    {
        CalculateVelocity();
        HandleWallSliding();

        //anim.SetFloat("Speed", Mathf.Abs(Input.GetAxisRaw("Horizontal")));

        controller.Move(velocity * Time.deltaTime, directionalInput);

        if (controller.collisions.above || controller.collisions.below)
        {
            velocity.y = 0;
        }

        if (Input.GetAxisRaw("Horizontal") < -0.1f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            shotEnd.rotation = Quaternion.AngleAxis(180, Vector3.up);
        }

        if (Input.GetAxisRaw("Horizontal") > 0.1)
        {
            transform.localScale = new Vector3(1, 1, 1);
            shotEnd.rotation = Quaternion.AngleAxis(0, Vector3.up);
        }

        if (controller.collisions.below)
        {
            //anim.SetBool("IsGrounded", true);
            
        }
        else
        {
            //anim.SetBool("IsGrounded", false);
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

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.tag == "MovingPlatform")
    //    {
    //        transform.parent = collision.transform;
    //    }
    //}
    //
    //private void OnCollisionExit2D(Collision2D collision)
    //{
    //    if (collision.gameObject.tag == "MovingPlatform")
    //    {
    //        transform.parent = null;
    //    }
    //}
}
