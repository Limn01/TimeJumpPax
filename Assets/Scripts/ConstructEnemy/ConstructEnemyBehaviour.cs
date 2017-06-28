using System.Collections;
using UnityEngine;

public class ConstructEnemyBehaviour : MonoBehaviour
{
    [SerializeField]
    float range;
    [SerializeField]
    LayerMask playerLayer;
    [SerializeField]
    float moveSpeed;
    [SerializeField]
    float waitTime;
    [SerializeField]
    Transform pathHolder;
    [SerializeField]
    Transform point;

    bool playerInRange;
    bool coroutineStarted = false;
    
    Animator anim;
    Vector3[] wayPoints;
    
    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        anim.SetBool("IsAwake", false);

        wayPoints = new Vector3[pathHolder.childCount];
        for (int i = 0; i < wayPoints.Length; i++)
        {
            wayPoints[i] = pathHolder.GetChild(i).position;
        }
    }

    private void FixedUpdate()
    {
        playerInRange = Physics2D.OverlapCircle(transform.position, range, playerLayer);
    }

    private void Update()
    {
        

        if (playerInRange && !coroutineStarted)
        {
            anim.SetBool("IsAwake", true);
            StartCoroutine(FollowPath(wayPoints));
        }

        else if (coroutineStarted && !playerInRange)
        {
            anim.SetBool("IsAwake", false);
            coroutineStarted = false;

            transform.position = Vector3.Lerp(transform.position, point.position, moveSpeed * Time.deltaTime);
        }
    }

    IEnumerator FollowPath(Vector3[] wayPoints)
    {
        coroutineStarted = true;

        transform.position = wayPoints[0];

        int targetWaypointIndex = 1;
        Vector3 targetWaypoint = wayPoints[targetWaypointIndex];

        while (playerInRange)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetWaypoint, moveSpeed * Time.deltaTime);

            if (transform.position == targetWaypoint)
            {
                targetWaypointIndex = (targetWaypointIndex + 1) % wayPoints.Length;
                targetWaypoint = wayPoints[targetWaypointIndex];
                yield return new WaitForSeconds(waitTime);
            }

            yield return null;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawSphere(transform.position, range);
    }
}
