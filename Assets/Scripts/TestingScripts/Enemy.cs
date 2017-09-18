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

    private void Start()
    {
        controller = GetComponent<Controller2D>();
    }

    private void Update()
    {
        velocity += Vector3.down * gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime, false);

        if (controller.collisions.below)
        {
            velocity.y = 0;
            //velocity = new Vector2(moveSpeed, velocity.y);
        }

        if (controller.collisions.right || controller.collisions.left)
        {
            moveRight = !moveRight;
        }

        

        if (moveRight)
        {
            velocity = new Vector2(moveSpeed, velocity.y);
        }

        if (!moveRight)
        {
            velocity = new Vector2(-moveSpeed, velocity.y);
        }

        //if (hittingWall)
        //{
        //    moveRight = !moveRight;
        //}

        
    }
}
