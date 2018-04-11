using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PixelPerfectCamera : MonoBehaviour
{
    public static float pixelToUnits = 1f;
    public static float scale = 2f;

    public Vector2 nativeResolution = new Vector2(16, 9);

    Camera cam;

    private void Awake()
    {
        cam = Camera.main.GetComponent<Camera>();

        if (cam.orthographic)
        {
            scale = Screen.height / nativeResolution.y;
            pixelToUnits *= scale;
            cam.orthographicSize = (Screen.height / 2.0f) / pixelToUnits;
        }
    }
}
