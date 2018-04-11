using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndOfDemoCutScene : MonoBehaviour
{
    public float moveSpeed;
    public float waitTime;
    public Transform pathHolder;

    AudioManager audioManager;
    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
        audioManager = FindObjectOfType<AudioManager>();

        audioManager.Play("EndOfDemo");

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
            anim.SetBool("IsMoving", true);

            if (transform.position == targetWaypoint)
            {
                anim.SetBool("IsMoving", false);
                targetWaypointIndex = (targetWaypointIndex + 1) % wayPoints.Length;
                targetWaypoint = wayPoints[targetWaypointIndex];
                transform.localScale = new Vector3(-1, 1, 1);
                audioManager.Play("Ooh");
                
                yield return new WaitForSeconds(waitTime);
                transform.localScale = new Vector3(1, 1, 1);
                audioManager.gameObject.SetActive(false);
            }
            yield return null;
        }
    }
}
