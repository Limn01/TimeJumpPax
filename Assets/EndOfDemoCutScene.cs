using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndOfDemoCutScene : MonoBehaviour
{
    public float moveSpeed;
    //public float waitTime;
    public Transform currentPoint;
    public Transform[] points;
    public int pointIndex;

    private void Start()
    {
        currentPoint = points[pointIndex];
    }

    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, currentPoint.position, moveSpeed * Time.deltaTime);

        if (transform.position == currentPoint.position)
        {
            pointIndex++;

            if (pointIndex == points.Length)
            {
                pointIndex = 0;
            }

            currentPoint = points[pointIndex];
        }
    }

    private void Move()
    {
        transform.position = Vector2.MoveTowards(transform.position, currentPoint.position, moveSpeed * Time.deltaTime);

        if (transform.position == currentPoint.position)
        {
            pointIndex++;

            if (pointIndex == points.Length)
            {
                pointIndex = 0;
            }

            currentPoint = points[pointIndex];
        }
    }
}
