using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityAttractor : MonoBehaviour
{
    public float gravity = -9.8f;

    public void Attract(Rigidbody2D body)
    {
        Vector3 gravityUp = (body.transform.position - transform.position).normalized;
        Vector3 localUp = body.transform.up;

        body.AddForce(gravityUp * gravity);
        body.transform.rotation = Quaternion.FromToRotation(localUp, gravityUp) * body.transform.rotation;
        
    }
}
