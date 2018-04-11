using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyClimb : MonoBehaviour
{
    public float offset = 2f;
    public float distance;
    public LayerMask groundMask;
    public float moveSpeed;

    bool isCurrLeftHit = false;
    bool isCurrRightHit = false;
    bool isPrevLeftHit = false;
    bool isPrevRightHit = false;

    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Vector3 pos = transform.position;
        Vector3 up = transform.up;
        Vector3 down = -transform.up;
        Vector3 right = transform.right;
        Vector3 left = -transform.right;
        Vector3 size = transform.localScale;
        Vector3 halfSize = size * 0.5f;

        Vector3 bottomLeft = pos + left * halfSize.x + down * halfSize.y;
        Vector3 bottomRight = pos + right * halfSize.x + down * halfSize.y;

        Ray leftRay = new Ray(bottomLeft, down);
        Ray rightRay = new Ray(bottomRight, down);

        Debug.DrawLine(leftRay.origin, leftRay.origin + leftRay.direction * distance, Color.cyan);
        Debug.DrawLine(rightRay.origin, rightRay.origin + rightRay.direction * distance, Color.cyan);

        RaycastHit2D leftRayHit = Physics2D.Raycast(leftRay.origin, leftRay.direction, distance, groundMask);
        RaycastHit2D rightRayHit = Physics2D.Raycast(rightRay.origin, rightRay.direction, distance, groundMask);

        if (leftRayHit.collider != null)
        {
            isCurrLeftHit = true;
        }
        else
        {
            isCurrLeftHit = false;
        }
        if (rightRayHit.collider != null)
        {
            isCurrRightHit = true;
        }
        else
        {
            isCurrRightHit = false;
        }
        if (leftRayHit.collider == null && rightRayHit.collider == null)
        {
            if (isPrevLeftHit)
            {
                transform.position += down * offset * Time.deltaTime;
                transform.rotation *= Quaternion.AngleAxis(-90, Vector3.forward);
            }
            else if (isPrevRightHit)
            {
                transform.position += down * offset * Time.deltaTime;
                transform.rotation *= Quaternion.AngleAxis(90, Vector3.forward);
            }
        }

        transform.position -= transform.right * Time.deltaTime * moveSpeed;

        

        isPrevLeftHit = isCurrLeftHit;
        isPrevRightHit = isCurrRightHit;
    }
}
