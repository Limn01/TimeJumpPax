using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    Animator anim;

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        anim.SetFloat("Speed", Mathf.Abs(Input.GetAxisRaw("Horizontal")));

        if (Input.GetAxisRaw("DpadHorizontal") > 0.1f)
        {
            anim.SetFloat("Speed", Mathf.Abs(Input.GetAxisRaw("DpadHorizontal")));
        }
        else
        {
            anim.SetFloat("Speed", Mathf.Abs(Input.GetAxisRaw("DpadHorizontal")));
        }
    }
}
