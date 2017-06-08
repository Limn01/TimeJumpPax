using System.Collections;
using System.Collections.Generic;
using Com.LuisPedroFonseca.ProCamera2D;
using UnityEngine;


public class PatrolEnemy : MonoBehaviour
{
    [SerializeField]
    Transform[] waypoints;
    [SerializeField]
    float moveSpeed;
    [SerializeField]
    float waitTime;
    [SerializeField]
    float force;

    int currentPoint;
    bool playerInRange;
    Animator anim;
    GameObject player;

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");

        StartCoroutine("Patrol");
        anim.SetBool("Patrolling", true);
    }

    private void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == player)
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject == player)
        {
            playerInRange = false;
        }
    }

    IEnumerator Patrol()
    {
        Debug.Log("Started");

        while (true)
        {
            if (transform.position.x == waypoints[currentPoint].position.x)
            {
                
                currentPoint++;
                anim.SetBool("Patrolling", false);
                yield return new WaitForSeconds(waitTime);
                anim.SetBool("Patrolling", true);
            }

            if (currentPoint >= waypoints.Length)
            {
                currentPoint = 0;
            }

            transform.position = Vector2.MoveTowards(transform.position, new Vector2(waypoints[currentPoint].position.x, transform.position.y), moveSpeed * Time.deltaTime);

            if (transform.position.x < waypoints[currentPoint].position.x)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else if (transform.position.x > waypoints[currentPoint].position.x)
            {
                transform.localScale = Vector3.one;
                
            }

            yield return null;
        }
    }
}



