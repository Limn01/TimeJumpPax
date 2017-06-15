using System.Collections;
using UnityEngine;
using Com.LuisPedroFonseca.ProCamera2D;

public class ScrollingBackground : MonoBehaviour
{
    public Transform[] backgrounds;     // Array of all the backgrounds to be parralaxed.
    float[] parralaxingScales;      // The proportion of the camera's movement to move the backgrounds
    public float smoothing = 1f;    // How smooth the parallax is going to be 

    Transform cam;                  // Reference to the main camera Transform
    Vector3 previousCamPos;         // The position of the camera in the previuos frame

    private void Awake()
    {
        cam = Camera.main.transform;
    }

    private void Start()
    {
        // The previous frame had the current frame's camera position
        previousCamPos = cam.position;

        // Assiging corresponding parrallax scales
        parralaxingScales = new float[backgrounds.Length];

        for (int i = 0; i < backgrounds.Length; i++)
        {
            parralaxingScales[i] = backgrounds[i].position.z * -1;
        }
    }

    private void Update()
    {
        // for each background
        for (int i = 0; i < backgrounds.Length; i++)
        {
            // the parallax is the opposite of the camera movement because the previous frame muiltiplied by the scale
            float parallax = (previousCamPos.x - cam.position.x) * parralaxingScales[i];

            // set a target x position which is the current position plus the parallax
            float backgroundTargetPosX = backgrounds[i].position.x + parallax;

            // create a target position which is the backgrounds current position with its target x position
            Vector3 backgroundTargetPos = new Vector3(backgroundTargetPosX, backgrounds[i].position.y, backgrounds[i].position.z);

            // fade between currrent position and the target position using lerp
            backgrounds[i].position = Vector3.Lerp(backgrounds[i].position, backgroundTargetPos, smoothing * Time.deltaTime);
        }

        // set the previuos cam pos to the camera's position at the end of the frame
        previousCamPos = cam.position;
    }
}
