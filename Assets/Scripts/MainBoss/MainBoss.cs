using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class MainBoss : MonoBehaviour
{
    public float timeTojump = 2f;
    public float timeToShoot = 1f;
    public float chance;
    public Vector2 jumpForce;
    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask whatIsGround;
    public Transform[] shotPoints;
    public GameObject bullet;
    public float bulletSpeed;
    public Transform shotPoint;
    public Transform shotPoint1;
    public Transform shotPoint2;
    public float timeToHoldJump = 2f;
    public Transform jumpTo;
    public float moveSpeed;
    public Vector2 targetStoredPos;
    public GameObject jetBooster1;
    public GameObject jetBooster2;

    float gravity;
    float maxJumpHeight = 6;
    float minJumpHeight = 1;
    float timeToJumpApex = .4f;
    float accerlerationTimeAirborne = .2f;
    float accelerationTimeGrounded = .1f;
    float maxJumpVelocity;
    float minJumpVelocity;
    float gravityStore;
    

    bool coroutineStarted = false;
    bool otherCoroutineStarted = false;
    bool isGrounded;
    bool attack;
    bool jumping;
    bool facingRight;
    bool facingLeft = true;
    Transform target;
    GameObject player;
    Rigidbody2D rb;
    BossHealth bossHealth;
    Animator anim;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        bossHealth = GetComponent<BossHealth>();
        player = GameObject.FindGameObjectWithTag("Player");
        target = player.GetComponent<Transform>();
        anim = GetComponentInChildren<Animator>();
        

        jetBooster1.SetActive(false);
        jetBooster2.SetActive(false);
    }

    private void Start()
    {
        gravity = -(2 * maxJumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        gravityStore = gravity;

        if (!coroutineStarted)
        {
            StartCoroutine(BossMove());
        }
    }

    private void FixedUpdate()
    {
        rb.AddForce(Vector2.down * -gravity * Time.deltaTime, ForceMode2D.Impulse);

        isGrounded = false;

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
    }

    private void Update()
    {
        if (isGrounded)
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            jumping = false;
            jetBooster1.SetActive(false);
            jetBooster2.SetActive(false);
        }

        if (bossHealth.currentHealth <= 10 && coroutineStarted && !otherCoroutineStarted)
        {
            coroutineStarted = false;
            StopAllCoroutines();
            StartCoroutine(BossMovementChange());
        }
    }

    IEnumerator BossMove()
    {
        coroutineStarted = true;
    
        while (true)
        {
            yield return new WaitForSeconds(timeTojump);
    
            int i = 1;
    
            if (Random.value < chance)
            {
                if (player.transform.position.x < transform.position.x)
                {
                    facingLeft = true;
                    facingRight = false;
                    i = -1;
                    transform.localScale = new Vector3(1, 1, 1);
                }
            }
            if (player.transform.position.x > transform.position.x)
            {
                facingRight = true;
                facingLeft = false;
                i = 1;
                transform.localScale = new Vector3(-1, 1, 1);
            }
    
            anim.SetTrigger("Jumping");
            
            rb.velocity = new Vector2(jumpForce.x * i, jumpForce.y);

            jetBooster1.SetActive(true);
            jetBooster2.SetActive(true);
    
            yield return new WaitForSeconds(timeToShoot);
    
            if (!facingLeft)
            {
                SpredShotRight();
            }
            else if (facingLeft)
            {
                SpreadShotLeft();
            }
        }
    }

    void SpreadShotLeft()
    {
        for (int i = 0; i < shotPoints.Length; i++)
        {
            GameObject obj = EnemyBulletPool.current.GetPooledObject();
            List<GameObject> pooledObj = new List<GameObject>();
            pooledObj.Add(obj);
            int randomIndex = Random.Range(0, pooledObj.Count);
            if (!pooledObj[randomIndex].activeInHierarchy)
            {
                pooledObj[randomIndex].SetActive(true);
                pooledObj[randomIndex].transform.position = shotPoints[i].transform.position;
                pooledObj[randomIndex].transform.rotation = shotPoints[i].transform.rotation;
                pooledObj[randomIndex].GetComponent<Rigidbody2D>().AddForce(-shotPoints[i].right * bulletSpeed, ForceMode2D.Impulse);
            }
        }
    }

    void SpredShotRight()
    {
        for (int i = 0; i < shotPoints.Length; i++)
        {
            GameObject obj = EnemyBulletPool.current.GetPooledObject();
            List<GameObject> pooledObj = new List<GameObject>();
            pooledObj.Add(obj);
            int randomIndex = Random.Range(0, pooledObj.Count);
            if (!pooledObj[randomIndex].activeInHierarchy)
            {
                pooledObj[randomIndex].SetActive(true);
                pooledObj[randomIndex].transform.position = shotPoints[i].transform.position;
                pooledObj[randomIndex].transform.rotation = shotPoints[i].transform.rotation;
                pooledObj[randomIndex].GetComponent<Rigidbody2D>().AddForce(shotPoints[i].right * bulletSpeed, ForceMode2D.Impulse);
            }
        }
    }

    IEnumerator BossMovementChange()
    {
        otherCoroutineStarted = true;

        while (true)
        {
            yield return new WaitForSeconds(timeTojump);

            gravity = 0;

            transform.position = Vector3.Lerp(transform.position, jumpTo.position, moveSpeed * Time.deltaTime);

            targetStoredPos = target.position; 

            yield return new WaitForSeconds(timeToHoldJump);

            float distance = (transform.position - target.transform.position).sqrMagnitude;

            if (distance > 1)
            {
                gravity = gravityStore;
                transform.position = Vector3.Lerp(transform.position, targetStoredPos, moveSpeed * Time.deltaTime);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(groundCheck.position, groundCheckRadius);
    }
}
