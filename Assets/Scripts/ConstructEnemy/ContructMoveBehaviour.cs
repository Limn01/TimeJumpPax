using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContructMoveBehaviour : MonoBehaviour
{
    [SerializeField]
    float range;
    [SerializeField]
    LayerMask playerLayer;
    [SerializeField]
    float moveSpeed;
    [SerializeField]
    Vector2 targetStoredPosition;

    public Transform target;
    GameObject player;
    bool playerInRange;
    bool attack;

    private void Awake()
    {
        
    }

    private void Update()
    {
        playerInRange = Physics2D.OverlapCircle(transform.position, range, playerLayer);

        

        if (!attack)
        {
            if (playerInRange)
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
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0, 0, 1, 0.5f);
        Gizmos.DrawSphere(transform.position, range);
    }
}
