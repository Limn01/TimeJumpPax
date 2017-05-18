using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement instance;
    public float moveSpeed;
    public float jumpforce = 400f;
    public float jumpTimer;
    public float maxJumpTimer;
    public float knockback;
    public float knockbackCount;
    public float knockbackLength;
    public bool knockbackFromRight;
    

    public bool wallCheck;
    public Transform groundCheck;
    public const float groundCheckRadius = .2f;
    public Transform shotEnd;
    public float climbSpeed;
    public float climbVelocity;
    public float gravityStore;
    public AudioSource jumpSound;
    public AudioSource doubleJumpSound;

    public bool grounded;
    bool canJump;
    bool doubleJump;
    public bool onLadder;

    public bool facingRight = true;
    [SerializeField] bool airControl = false;
    Rigidbody2D rb2d;
    public LayerMask whatIsGround;
    Animator anim;

    void Awake()
    {
        instance = this;
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        jumpSound = GetComponent<AudioSource>();
        gravityStore = rb2d.gravityScale;
        
    }

    void FixedUpdate()
    {
        grounded = false;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, groundCheckRadius, whatIsGround);

        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                grounded = true;
            }
        }

        Move();
    }

    void Update()
    {
        anim.SetFloat("Speed", Mathf.Abs(rb2d.velocity.x));
        anim.SetBool("Grounded", grounded);

        if (grounded)
        {
            doubleJump = false;
            airControl = true;
        }

        if (grounded && Input.GetButtonDown("Jump"))
        {
            jumpTimer = 0;
            canJump = true;
            rb2d.velocity = new Vector2(rb2d.velocity.x, jumpforce * 3);
            
            jumpSound.Play();
            
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
            rb2d.velocity = new Vector2(rb2d.velocity.x, jumpforce * 1.2f);
            doubleJumpSound.Play();
        }

        if (onLadder)
        {
            rb2d.gravityScale = 0f;
            climbVelocity = climbSpeed * Input.GetAxis("Vertical");
            rb2d.velocity = new Vector2(rb2d.velocity.x, climbVelocity);
        }

        if (!onLadder)
        {
            rb2d.gravityScale = gravityStore;
            
        }
    }

    void Move()
    {
        float h = Input.GetAxis("Horizontal");

        if (grounded || airControl)
        {
            rb2d.velocity = new Vector2(h * moveSpeed, rb2d.velocity.y);

            if (h > 0 && !facingRight)
            {
                Flip();
                shotEnd.rotation = Quaternion.AngleAxis(0,Vector3.up);
            }

            else if (h < 0 && facingRight)
            {
                Flip();
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

    void Flip()
    {
        facingRight = !facingRight;

        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
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
