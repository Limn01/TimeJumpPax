using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombController : MonoBehaviour
{
    public float bombSpeedLow;
    public float bombSpeedHigh;
    public float bombAngle;
    public float bombTorquAngle;
    public Transform target;

    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        //Vector2 direction = target.position - transform.position;
        //
        //direction.Normalize();

        rb.AddForce(new Vector2(Random.Range(-bombAngle, bombAngle), Random.Range(bombSpeedLow, bombSpeedHigh)), ForceMode2D.Impulse);
        rb.AddTorque(Random.Range(-bombTorquAngle, bombTorquAngle));
    }
}
