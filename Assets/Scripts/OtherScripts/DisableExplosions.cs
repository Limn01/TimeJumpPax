using System.Collections;
using UnityEngine;

public class DisableExplosions : MonoBehaviour
{
    private void OnEnable()
    {
        Invoke("Destroy", .2f);
    }

    void Destroy()
    {
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }
}
