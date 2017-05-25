using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraShake : MonoBehaviour
{
    public static CamaraShake instance;

    [SerializeField]
    float shakeTimer;
    [SerializeField]
    float shakeAmount;
	// Update is called once per frame

    void Awake()
    {
        instance = this;
    }

	void Update ()
    {
        if (shakeTimer >= 0)
        {
            Vector2 shakePos = Random.insideUnitCircle * shakeAmount;
            transform.position = new Vector3(transform.position.x + shakePos.x, transform.position.y + shakePos.y, transform.position.z);
            shakeTimer -= Time.deltaTime;
        }
	}

    public void ShakeCamera(float shakePower,float shakeDuration)
    {
        shakeAmount = shakePower;
        shakeTimer = shakeDuration;
    }
}
