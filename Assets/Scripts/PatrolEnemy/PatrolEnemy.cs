using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolEnemy : MonoBehaviour
{
    public float moveSpeed;
    public float waitTime;
    public Transform pathHolder;
    public float rayDistance;
    public Transform player;

    RaycastHit2D hit;
    Rigidbody2D rb2d;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();

        Vector3[] wayPoints = new Vector3[pathHolder.childCount];
        for (int i = 0; i < wayPoints.Length; i++)
        {
            wayPoints[i] = pathHolder.GetChild(i).position;
        }

        StartCoroutine(FollowPath(wayPoints));
    }

    void Update()
    {
        hit = Physics2D.Raycast(transform.position, Vector2.left, rayDistance);

        if (hit.collider != null && hit.collider.tag == "Player")
        {
            HandleAttack();
            Debug.Log("Attacking");
          
        }
        
    }

    IEnumerator FollowPath(Vector3[] wayPoints)
    {
        transform.position = wayPoints[0];

        int targetWaypointIndex = 1;
        Vector3 targetWaypoint = wayPoints[targetWaypointIndex];

        while (true)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetWaypoint, moveSpeed * Time.deltaTime);


            if (transform.position.x == targetWaypoint.x)
            {
                transform.localScale = new Vector3(1, 1, 1);
                targetWaypointIndex = (targetWaypointIndex + 1) % wayPoints.Length;
                targetWaypoint = wayPoints[targetWaypointIndex];
                yield return new WaitForSeconds(waitTime);
            }

            else if (transform.position.x <= targetWaypoint.x)
            {
                transform.localScale = new Vector3(-1,1,1);
            }

            if (transform.position.x >= targetWaypoint.x)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
            yield return null;
        }

    }

    void HandleAttack()
    {
        float dist = Vector2.Distance(transform.position, player.position);

        if (dist < rayDistance)
        {
            rb2d.velocity = new Vector2(hit.collider.transform.position.x - transform.position.x, 0);
        }
        
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + transform.localScale.x * Vector3.left * rayDistance);
    }
}
