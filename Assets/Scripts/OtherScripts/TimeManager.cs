using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public float slowDownFactor = 0.1f;
    public float slowDownLength = 2f;

    private void Update()
    {
        Time.timeScale += (1f / slowDownLength) * Time.fixedDeltaTime;
        Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);
    }

    public void DoSlowmotion()
    {
        Time.timeScale = slowDownFactor;
        Time.fixedDeltaTime = Time.timeScale * .02f;
    }
}
