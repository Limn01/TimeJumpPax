using System.Collections;
using Com.LuisPedroFonseca.ProCamera2D;
using UnityEngine;

public class Parallaxing : MonoBehaviour
{
    public Transform[] backgrounds;
    float[] parallaxScales;

    public float smoothing;

    Vector3 previousCamPos;

    private void Start()
    {
        previousCamPos = transform.position;

        parallaxScales = new float[backgrounds.Length];

        for (int i = 0; i < parallaxScales.Length; i++)
        {
            parallaxScales[i] = backgrounds[i].position.z * -1;
        }
    }

    private void LateUpdate()
    {
        for (int i = 0; i < backgrounds.Length; i++)
        {
            Vector3 parallax = (previousCamPos - transform.position) * (parallaxScales[i] / smoothing);

            backgrounds[i].position = new Vector3(backgrounds[i].position.x + parallax.x, backgrounds[i].position.y, backgrounds[i].position.z);
        }

        previousCamPos = transform.position;
    }
}
