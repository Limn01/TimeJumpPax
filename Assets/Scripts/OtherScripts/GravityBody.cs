using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class GravityBody : MonoBehaviour
{
    GravityAttractor portal;
    Rigidbody2D rb;
    //Rigidbody rB;

    private void Awake()
    {
        portal = GameObject.FindGameObjectWithTag("Portal").GetComponent<GravityAttractor>();
        rb = GetComponent<Rigidbody2D>();

        //rb.gravityScale = 0;
            
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    private void FixedUpdate()
    {
        portal.Attract(rb);

    }
}
