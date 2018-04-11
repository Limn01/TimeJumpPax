using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    public float jumpforce = 400f;
    public float jumpTimer;
    public float maxJumpTimer;
    public float knockback;
    public float knockbackCount;
    public float knockbackLength;
    public bool knockbackFromRight;
    [SerializeField]
    float rayLength;

    public bool wallCheck;
    public Transform groundCheck;
    public const float groundCheckRadius = .02f;
    public Transform shotEnd;
    public float climbSpeed;
    public float climbVelocity;
    public float gravityStore;
    public AudioSource jumpSound;
    public AudioSource doubleJumpSound;

    public bool grounded;
    public bool canMove;
    bool canJump;
    bool doubleJump;
    public bool onLadder;
    float jumpVelocity;

    float gravity;
    public float maxJumpHeight = 6;
    public float minJumpHeight = 1;
    public float timeToJumpApex = .4f;
    float accerlerationTimeAirborne = .2f;
    float accelerationTimeGrounded = .1f;
    float maxJumpVelocity;
    float minJumpVelocity;

    public bool facingRight = true;
    [SerializeField]
    bool airControl = false;
    Rigidbody2D rb2d;
    public LayerMask whatIsGround;
    Animator anim;
    float moveSpeedStore;
    Vector3 velocity;
 
    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        jumpSound = GetComponent<AudioSource>();
    }

    private void Start()
    {
        gravity = -(2 * maxJumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        maxJumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
        minJumpVelocity = Mathf.Sqrt(2 * Mathf.Abs(gravity) * minJumpVelocity);

        gravityStore = gravity;
        moveSpeedStore = moveSpeed;
        canMove = true;
    }

    void FixedUpdate()
    {
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
        
        rb2d.AddForce(Vector2.down * -gravity * Time.deltaTime, ForceMode2D.Impulse);
    }

    void Update()
    {
        Move();

        if (!canMove)
        {
            moveSpeed = 0;
            return;
        }
        else
        {
            moveSpeed = moveSpeedStore;
        }
        
        if (grounded)
        {
            doubleJump = false;
            airControl = true;
            anim.SetBool("Grounded", true);
        }

        if (grounded && Input.GetButtonDown("Jump"))
        {
            jumpTimer = 0;
            canJump = true;
            rb2d.velocity = new Vector2(rb2d.velocity.x, maxJumpVelocity);
            rb2d.velocity = new Vector2(rb2d.velocity.x, 0);
            jumpSound.Play();
            anim.SetBool("Grounded", false);
        }
        else if (Input.GetButton("Jump") && canJump && jumpTimer < maxJumpTimer)
        {
            jumpTimer += Time.deltaTime;
            rb2d.velocity = new Vector2(rb2d.velocity.x, jumpforce);
        }
        else
        {
            canJump = false;
        }

        if (Input.GetButtonDown("Jump") && !doubleJump && !grounded)
        {
            doubleJump = true;
            
            rb2d.gravityScale = gravityStore;
            rb2d.velocity = new Vector2(rb2d.velocity.x, jumpforce * 1.2f);
            doubleJumpSound.Play();
        }

        if (onLadder)
        {
            gravity = 0;
            climbVelocity = climbSpeed * Input.GetAxis("Vertical");
            rb2d.velocity = new Vector2(rb2d.velocity.x, climbVelocity);
        }

        if (!onLadder)
        {
            gravity = gravityStore;
        }
    }

    void Move()
    {

        float h = Input.GetAxisRaw("Horizontal");

        if (grounded || airControl)
        {
            anim.SetFloat("Speed", Mathf.Abs(h));
            
            if (h > 0)
            {
                transform.position += Vector3.right * moveSpeed * Time.deltaTime;
                transform.localScale = new Vector3(1, 1, 1);
                shotEnd.rotation = Quaternion.AngleAxis(0, Vector3.up);
            }

            else if (h < 0)
            {
                transform.position += Vector3.left * moveSpeed * Time.deltaTime;
                transform.localScale = new Vector3(-1, 1, 1);
                shotEnd.rotation = Quaternion.AngleAxis(180, Vector3.up);
            }
           
        }

        if (knockbackCount <= 0)
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, rb2d.velocity.y);
        }
        else
        {
            if (knockbackFromRight)
            {
                rb2d.velocity = new Vector2(-knockback, knockback);
                airControl = false;
            }

            if (!knockbackFromRight)
            {
                rb2d.velocity = new Vector2(knockback, knockback);
                airControl = false;
            }

            knockbackCount -= Time.deltaTime;
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "MovingPlatform")
        {
            transform.parent = other.transform;
        }

        if (other.gameObject.tag == "FlipPlatform")
        {
            transform.parent = other.transform;
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "MovingPlatform")
        {
            transform.parent = null;
        }

        if (other.gameObject.tag == "FlipPlatform")
        {
            transform.parent = null;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawSphere(groundCheck.position, groundCheckRadius);
    }
}

