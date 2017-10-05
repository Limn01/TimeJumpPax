using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndOfDemoCutScene : MonoBehaviour
{
    public float moveSpeed;
    public float waitTime;
    public Transform pathHolder;

    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();

        Vector3[] wayPoints = new Vector3[pathHolder.childCount];
        for (int i = 0; i < wayPoints.Length; i++)
        {
            wayPoints[i] = pathHolder.GetChild(i).position;
        }

        StartCoroutine(FollowPath(wayPoints));
    }

    IEnumerator FollowPath(Vector3[] wayPoints)
    {
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
                transform.localScale = new Vector3(-1, 1, 1);
                yield return new WaitForSeconds(waitTime);
                transform.localScale = new Vector3(1, 1, 1);
            }
            yield return null;
        }
    }
}
