using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContructMoveBehaviour : MonoBehaviour
{
    [SerializeField]
    float moveSpeed;
    
    Vector3 targetStoredPosition;
    Transform target;
    GameObject player;
    bool playerInRange;
    bool attack;
    float distance;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        target = player.GetComponent<Transform>();
    }

    private void Update()
    {
        distance = (transform.position - target.position).sqrMagnitude;

        if (!attack)
        {
            if (distance > 1)
            {
                transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);

                targetStoredPosition = target.transform.position;

                attack = true;
            }
        }

        if (attack)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetStoredPosition, moveSpeed * Time.deltaTime);

            if (transform.position == targetStoredPosition)
            {
                attack = false;
            }
        }

        EnemyTurn();
    }

    void EnemyTurn()
    {
        Vector3 vectorToTarget = target.position - transform.position;
        float angle = (Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg) + 90;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * moveSpeed);
    }
}
