using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimulateDeltaTime : MonoBehaviour
{
    float lastTime;

    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        lastTime = Time.realtimeSinceStartup;
    }

    private void Update()
    {
        float deltaTime = Time.realtimeSinceStartup - lastTime;
        
        lastTime = Time.realtimeSinceStartup;
    }
}
