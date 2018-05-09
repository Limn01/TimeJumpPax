using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireFly : Enemy
{
    public float moveSpeed;
    public float verticalSpeed;
    public float amplitude;
    
    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    protected override void Update()
    {
        base.Update();

        Movement();
    }

    void Movement()
    {
        transform.Translate(-moveSpeed, Mathf.Sin(Time.realtimeSinceStartup * verticalSpeed) * amplitude, 0);
    }
}
