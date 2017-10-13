using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyWallClimbing : MonoBehaviour
{
    public float offset = 2f;
    public float distance;
    public float ray2Distance;
    public LayerMask collisionMask;
    public float moveSpeed;
    public Vector3 rayOffset;
    public float yOffset;

    //float maxRotationDegrees = 90f;
    //BoxCollider2D collider;
    //float positionoffsetY;
    //bool moveleft;
    //Vector3 velcoity;
    //Rigidbody2D rb;
    //Vector2 rayOrigin;
    //public bool movingleft;

    private bool isCurrLeftHit = false;
    private bool isCurrRightHit = false;
    private bool isPrevLeftHit = false;
    private bool isPrevRightHit = false;

    private void Awake()
    {
        
    }

    private void Start()
    {

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

        Ray rayLeft = new Ray(bottomLeft, down);
        Ray rayRight = new Ray(bottomRight, down);

        Debug.DrawLine(rayLeft.origin, rayLeft.origin + rayLeft.direction * distance, Color.red);
        Debug.DrawLine(rayRight.origin, rayRight.origin + rayRight.direction * distance, Color.red);

        RaycastHit2D rayLeftHit = Physics2D.Raycast(rayLeft.origin, rayLeft.direction, distance, collisionMask);
        RaycastHit2D rayRightHit = Physics2D.Raycast(rayRight.origin, rayRight.direction, distance, collisionMask);

        if (rayLeftHit.collider != null)
        {
            isCurrLeftHit = true;
        }
        else
        {
            isCurrLeftHit = false;
        }
        if (rayRightHit.collider != null)
        {
            isCurrRightHit = true;
        }
        else
        {
            isCurrRightHit = false;
        }

        if (rayLeftHit.collider == null &&
           rayRightHit.collider == null)
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

        #region CIRCLES
        /*
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -transform.up, distance, collisionMask);

        RaycastHit2D hit3 = Physics2D.Raycast(transform.position, -transform.up, distance, collisionMask);

        Vector2 rayAngle = Quaternion.AngleAxis(-100, Vector2.down) * -transform.forward;

        rayAngle = transform.position - transform.right;
        RaycastHit2D hit2 = Physics2D.Raycast(rayOrigin, rayAngle, distance, collisionMask);

        Debug.DrawRay(rayOrigin, rayAngle * distance, Color.blue);
        Debug.DrawRay(transform.position + new Vector3(-.5f, 0, 0), -transform.up + new Vector3(-.5f, 0, 0), Color.cyan);

        Debug.DrawRay(transform.localPosition, -transform.up * distance, Color.red);

        if (hit)
        {
            Vector2 targetLocation = hit.point;
            //Vector2 targetLocationRight = hit3.point;

            //targetLocationRight += new Vector2(0, transform.localScale.x / 2);
            // transform.position = targetLocationRight;

            targetLocation += new Vector2(0, transform.localScale.y / 2);

            transform.position = targetLocation;
        }

        Vector3 rayOrigin1 = transform.position - transform.right;
        Vector3 rayOriginRight = transform.position - transform.right;

        RaycastHit2D hit1 = Physics2D.Raycast(rayOrigin1, Quaternion.Euler(-40, 60, 0) * -transform.up, ray2Distance, collisionMask);
        RaycastHit2D hit2 = Physics2D.Raycast(rayOriginRight, Quaternion.Euler(40, 60, 0) * -transform.up, ray2Distance, collisionMask);

        Debug.DrawRay(rayOrigin1, Quaternion.Euler(-40, 60, 0) * -transform.up * ray2Distance, Color.blue);
        Debug.DrawRay(rayOriginRight, Quaternion.Euler(40, 60, 0) * -transform.up * ray2Distance, Color.blue);

        Vector3 averageNormal = (hit1.normal + hit2.normal) / 2;
        Vector3 averagePoint = (hit1.point + hit2.point) / 2;

        Quaternion targetRotation = Quaternion.FromToRotation(Vector3.up, averageNormal);
        Quaternion finalRotation = Quaternion.RotateTowards(transform.rotation, targetRotation, maxRotationDegrees);
        transform.rotation = Quaternion.Euler(0, 0, finalRotation.eulerAngles.z);
        transform.rotation = targetRotation;


        transform.position = averagePoint + transform.up * yOffset;

        transform.position -= transform.right * Time.deltaTime * moveSpeed;

        moveleft = true;

        if (movingleft)
        {
            Vector3 overrideLeftInfoHitOrigin = transform.position - transform.right;

            RaycastHit2D overrideLeftHitInfo = Physics2D.Raycast(overrideLeftInfoHitOrigin, Quaternion.Euler(-40, 60, 0) * -transform.up, ray2Distance, collisionMask);

            if (overrideLeftHitInfo)
            {
                hit1 = overrideLeftHitInfo;
            }
        }
        */
        #endregion
    }
}
