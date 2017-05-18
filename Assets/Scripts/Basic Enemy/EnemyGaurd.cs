using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGaurd : MonoBehaviour
{
    public float speed = 5;
    public Transform pathHolder;
    public float waitTime;

    void Start()
    {
        Vector3[] wayPoints = new Vector3[pathHolder.childCount];
        for (int i = 0; i < wayPoints.Length; i++)
        {
            wayPoints[i] = pathHolder.GetChild(i).position;
        }
    }

    IEnumerator FollowPath(Vector3[] wayPoints)
    {
        transform.position = wayPoints[0];

        int targetWayPointIndex = 1;
        Vector3 targetWaypoint = wayPoints[targetWayPointIndex];

        while (true)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetWaypoint, speed * Time.deltaTime);
            if (transform.position == targetWaypoint)
            {
                targetWayPointIndex = (targetWayPointIndex + 1) %  wayPoints.Length;
                targetWaypoint = wayPoints[targetWayPointIndex];
                yield return new WaitForSeconds(waitTime);
            }
            yield return null;
        }
    }

    void OnDrawGizmos()
    {
        foreach (Transform Waypoint in pathHolder)
        {
            Gizmos.DrawSphere(Waypoint.position, .100f);
        }
    }
}
