using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContructMoveBehaviour : Enemy
{
    public GameObject contructModel;

    [SerializeField]
    float moveSpeed;
    [SerializeField]
    float rotateSpeed = 200f;

    Vector3 targetStoredPosition;
    Transform target;
    Rigidbody2D rb;

    bool playerInRange;
    bool attack;
    float distance;

    protected override void Awake()
    {
        base.Awake();
        target = player.GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
    }

    protected override void Update()
    {
        base.Update();

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
        Vector2 direction = (Vector2)target.position - rb.position;
        direction.Normalize();
        float rotateAmount = Vector3.Cross(direction, transform.up).z;
        rb.angularVelocity = -rotateAmount * rotateSpeed;

        //Vector3 vectorToTarget = target.position - transform.position;
        //float angle = (Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg) + /90;
        //Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        //transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * moveSpeed);
    }
}
