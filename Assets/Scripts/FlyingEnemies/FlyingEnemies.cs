using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemies : Enemy
{
    public float moveSpeed;
    public float playerRange;
    public LayerMask playerLayer;
    public Vector2 tempPos;
    public float verticalSpeed;
    public float amplitude;
   
    Transform target;
    float distance;

    protected override void Awake()
    {
        base.Awake();
        target = player.GetComponent<Transform>();
    }

    protected override void Update()
    {
        base.Update();

        distance = (transform.position - target.position).sqrMagnitude;
        
        if (distance < 100)
        {
            transform.position = Vector3.MoveTowards(transform.position,player.transform.position,moveSpeed * Time.deltaTime);
        }
        else
        {
            transform.Translate(0, Mathf.Sin(Time.realtimeSinceStartup * verticalSpeed) * amplitude, 0);
        }
    }
}



