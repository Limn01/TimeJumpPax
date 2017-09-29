using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class GravityBody : MonoBehaviour
{
    GravityAttractor portal;
    Rigidbody2D rb;
    GameObject _portal;

    private void Awake()
    {
        //portal = GameObject.FindGameObjectWithTag("Portal").GetComponent<GravityAttractor>();
        
        rb = GetComponent<Rigidbody2D>();
        //_portal = GameObject.FindGameObjectWithTag("Portal");
            
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    private void Update()
    {
        if (_portal.activeInHierarchy)
        {
            Debug.Log("refference portal");
            _portal = GameObject.FindGameObjectWithTag("Portal");
            portal = GameObject.FindGameObjectWithTag("Portal").GetComponent<GravityAttractor>();
        }
    }

    private void FixedUpdate()
    {
        portal.Attract(rb);
    }
}
