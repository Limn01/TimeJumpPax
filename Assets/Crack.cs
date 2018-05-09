using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crack : MonoBehaviour
{
    public Transform target;
    public float moveSpeed;

    private void Update()
    {
        ActorMove();
    }

    void ActorMove()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
    }
}
