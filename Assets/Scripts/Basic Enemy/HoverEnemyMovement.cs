﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverEnemyMovement : ObstacleBase
{
    public float moveSpeed;
    public bool movingRight;

    public Transform wallCheck;
    public float wallCheckRadius;
    public LayerMask whatIsWall;

    Rigidbody2D rb2D;
    bool hittingWall;

    float timeCounter = 0;

    protected override void Awake()
    {
        base.Awake();
        rb2D = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        hittingWall = Physics2D.OverlapCircle(wallCheck.position, wallCheckRadius, whatIsWall);

        if (hittingWall)
        {
            movingRight = !movingRight;
        }

        if (movingRight)
        {
            transform.localScale = new Vector2(-1, 1);
            rb2D.velocity = new Vector2(moveSpeed, rb2D.velocity.y);
        }
        else
        {
            transform.localScale = new Vector2(1, 1);
            rb2D.velocity = new Vector2(-moveSpeed, rb2D.velocity.y);
        }
    }
}
