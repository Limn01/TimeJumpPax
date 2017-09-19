using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Com.LuisPedroFonseca.ProCamera2D;

[RequireComponent(typeof(Controller2D))]
public class Enemy : MonoBehaviour
{
    public float moveSpeed;
    float gravity = 12;

    Controller2D controller;
    Vector3 velocity;

    bool hittingWall;
    public bool moveRight;
    bool notAtEdge;

    private void Start()
    {
        controller = GetComponent<Controller2D>();
    }

    private void Update()
    {
        velocity += Vector3.down * gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime, false);

        notAtEdge = controller.collisions.below || controller.collisions.above;
        hittingWall = controller.collisions.right || controller.collisions.left;

        if (controller.collisions.below)
        {
            velocity.y = 0;
        }
        
        if (hittingWall || !notAtEdge)
        {
            moveRight = !moveRight;
        }

        if (moveRight)
        {
            velocity = new Vector2(moveSpeed, velocity.y);
        }
        
        else
        {
            velocity = new Vector2(-moveSpeed, velocity.y);
        }
    }
}
