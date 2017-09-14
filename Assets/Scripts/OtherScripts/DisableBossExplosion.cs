using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableBossExplosion : MonoBehaviour
{
    private void OnEnable()
    {
        Invoke("Destroy", .5f);
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
