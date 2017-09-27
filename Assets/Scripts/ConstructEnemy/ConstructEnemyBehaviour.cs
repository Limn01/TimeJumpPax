using System.Collections;
using UnityEngine;

public class ConstructEnemyBehaviour : MonoBehaviour
{
    [SerializeField]
    float moveSpeed;
    [SerializeField]
    float waitTime;
    [SerializeField]
    Transform pathHolder;
    [SerializeField]
    Transform point;
    
    bool coroutineStarted = false;

    GameObject player;
    Transform target;
    float distance;
    Animator anim;
    Vector3[] wayPoints;
    Coroutine coroutine;
    
    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        target = player.GetComponent<Transform>();
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

    private void Update()
    {
        distance = (transform.position - target.position).sqrMagnitude;

        if (distance < 50  && !coroutineStarted)
        {
            anim.SetBool("IsAwake", true);
            StartCoroutine(FollowPath(wayPoints));
            Debug.Log("StartedCor");
        }

        else if (distance > 50 && coroutineStarted)
        {
            anim.SetBool("IsAwake", false);
            coroutineStarted = false;
            Debug.Log("StoppedCor");
            StopAllCoroutines();
        }
    }

    IEnumerator FollowPath(Vector3[] wayPoints)
    {
        coroutineStarted = true;

        transform.position = wayPoints[0];

        int targetWaypointIndex = 1;
        Vector3 targetWaypoint = wayPoints[targetWaypointIndex];

        while (true)
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

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, distance);
    }
}
