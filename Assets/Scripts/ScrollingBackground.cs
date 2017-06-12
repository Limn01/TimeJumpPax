using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBackground : MonoBehaviour
{
    [SerializeField]
    Transform[] backgrounds;
    [SerializeField]
    float smoothing;

    float[] paralaxScales;
    Vector3 prevoiusCameraPos;

    Transform cam;

    private void Start()
    {
        cam = Camera.main.transform;

        prevoiusCameraPos = cam.position;

        paralaxScales = new float[backgrounds.Length];

        for (int i = 0; i < backgrounds.Length; i++)
        {
            paralaxScales[i] = backgrounds[i].position.z * -1;
        }
    }

    private void LateUpdate()
    {
        for (int i = 0; i < backgrounds.Length; i++)
        {
            float parallax = (prevoiusCameraPos.x - cam.position.x) * paralaxScales[i];

            float backgroundTargetPosX = backgrounds[i].position.x + parallax;

            Vector3 backgroundTargetPos = new Vector3(backgroundTargetPosX, backgrounds[i].position.y, backgrounds[i].position.z);

            backgrounds[i].position = Vector3.Lerp(backgrounds[i].position, backgroundTargetPos, smoothing * Time.deltaTime);
        }

        prevoiusCameraPos = cam.position;
    }
}
