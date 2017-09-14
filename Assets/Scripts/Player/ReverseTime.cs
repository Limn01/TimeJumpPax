using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReverseTime : MonoBehaviour
{
    bool isRewinding = false;

    public float recordTime = 5f;

    List<PointInTime> pointsInTime;

    //Rigidbody2D rb;

    GameObject timeEffect;
    Animator anim;

    private void Awake()
    {
        timeEffect = GameObject.FindGameObjectWithTag("TimeEffect");
        anim = timeEffect.GetComponent<Animator>();
        //rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        pointsInTime = new List<PointInTime>();
    }

    private void Update()
    {
        if (Input.GetButton("Slow"))
        {
            StartRewind();
        }
        else
        {
            StopRewind();
        }
    }

    private void FixedUpdate()
    {
        if (isRewinding)
        {
            Rewind();
        }
        else
        {
            Record();
        }
    }

    void Rewind()
    {
        if (pointsInTime.Count > 0)
        {
            PointInTime pointInTime = pointsInTime[0];
            transform.position = pointInTime.position;
            transform.localScale = pointInTime.rotation;
            pointsInTime.RemoveAt(0);
        }
        else
        {
            StopRewind();
        }
    }

    void Record()
    {
        if (pointsInTime.Count > Mathf.Round(recordTime / Time.fixedDeltaTime))
        {
            pointsInTime.RemoveAt(pointsInTime.Count - 1);
        }

        pointsInTime.Insert(0, new PointInTime(transform.position,transform.localScale));
    }

    public void StartRewind()
    {
        isRewinding = true;
        //rb.isKinematic = true;
        anim.SetBool("IsOn", true);
    }

    public void StopRewind()
    {
        isRewinding = false;
        //rb.isKinematic = false;
        anim.SetBool("IsOn", false);
    }
}
