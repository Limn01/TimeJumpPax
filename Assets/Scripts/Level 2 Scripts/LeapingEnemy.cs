using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeapingEnemy : LeapingEnemyDamage
{
    public JumpingState jumpingState;
    
    public float wallCheckRadious;
    public Transform target;
    public float jumpAngle;
    public float jumptorqueAngle;
    public float timeBetweenJump;
    public float rayDistance;

    public float timer;

    bool hittingWall;
    bool notAtEdge;

    Rigidbody2D rb;
    BoxCollider2D box2D;

    protected override void Awake()
    {
        base.Awake();

        rb = GetComponent<Rigidbody2D>();
        box2D = GetComponent<BoxCollider2D>();
    }

    protected override void Update()
    {
        base.Update();

       switch (jumpingState)
       {
           case JumpingState.Jumping:

                Vector3 direction = transform.position - target.position;

                direction.Normalize();

                rb.velocity = new Vector2(-jumpAngle, jumpAngle);

               jumpingState = JumpingState.NotJumping;
      
               break;
      
           case JumpingState.NotJumping:
                
               timer += Time.deltaTime;
      
               if (timer >= timeBetweenJump)
               {
                   timer = 0;
                   jumpingState = JumpingState.Jumping;
               }
      
               break;
       }
    }
}

public enum JumpingState
{
    Jumping,
    NotJumping
}
