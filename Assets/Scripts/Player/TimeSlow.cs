using System.Collections;
using UnityEngine;

public class TimeSlow : MonoBehaviour
{
    [SerializeField]
    float slowDownFactor = 0.05f;
    [SerializeField]
    float slowdownLength = 2f;

    public bool isSlow;

    private void Update()
    {

        if (Input.GetButtonDown("Slow") && !isSlow)
        {
            isSlow = true;

            Debug.Log("Pressed");
            DoSlowmotion();
        }

        Time.timeScale += (1f / slowdownLength) * Time.unscaledDeltaTime;
        Time.timeScale = Mathf.Clamp01(Time.timeScale);

        if (Time.timeScale == 1)
        {
            isSlow = false;
        }
    }

    void DoSlowmotion()
    {
        Time.timeScale = slowDownFactor;
        Debug.Log(Time.timeScale); 
        Time.fixedDeltaTime = Time.timeScale * .02f;
    }
}
