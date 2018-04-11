using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballCircle : ObstacleBase
{
    public Transform platform;

    public float rotateSpeed = 5f;
    public float radius = 0.1f;

    Vector2 center;
    float angle;

    protected override void Update()
    {
        base.Update();

        center = platform.transform.position;

        angle += rotateSpeed * Time.deltaTime;

        var offset = new Vector2(Mathf.Sin(angle), Mathf.Cos(angle)) * radius;
        transform.position = center + offset;
    }
}
