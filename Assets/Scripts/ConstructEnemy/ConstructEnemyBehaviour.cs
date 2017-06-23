using System.Collections;
using UnityEngine;

public class ConstructEnemyBehaviour : MonoBehaviour
{
    [SerializeField]
    Transform[] waypoints;
    [SerializeField]
    float range;
    [SerializeField]
    LayerMask playerLayer;
    [SerializeField]
    float moveSpeed;
    [SerializeField]
    float waitTime;

    int currentPoint;
    bool playerInRange;
    PlayerMovement playerMovement;
    GameObject player;
    Animator anim;


    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerMovement = player.GetComponent<PlayerMovement>();
        anim = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        anim.SetBool("IsAwake", false);
    }

    private void Update()
    {
        playerInRange = Physics2D.OverlapCircle(transform.position, range, playerLayer);

        if (playerInRange)
        {
            StartCoroutine(MoveBetweenPoints());
        }

        //else if (!playerInRange)
        //{
        //    transform.position
        //}
    }

    IEnumerator MoveBetweenPoints()
    {
        while (true)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(waypoints[currentPoint].position.x, transform.position.y), moveSpeed * Time.deltaTime);

            if (transform.position.x == waypoints[currentPoint].position.x)
            {
                currentPoint++;
                anim.SetBool("IsAwake", true);
                yield return new WaitForSeconds(waitTime);
            }

            if (currentPoint >= waypoints.Length)
            {
                currentPoint = 0;
            }

            

            yield return null;
        }
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.DrawSphere(transform.position, range);
    //}
}
