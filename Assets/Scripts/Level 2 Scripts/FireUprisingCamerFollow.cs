using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireUprisingCamerFollow : MonoBehaviour
{
    public Transform cam;

    public Vector3 offset;

    private void LateUpdate()
    {
        transform.position = new Vector3(cam.position.x, cam.position.y, 0) + offset;
    }
}
