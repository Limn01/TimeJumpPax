using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropPlatform : MonoBehaviour
{
    GameObject platform;
    Animator anim;

    void Start()
    {
        platform = GameObject.FindGameObjectWithTag("FlipPlatform");
        anim = platform.GetComponent<Animator>();
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == platform)
        {
            anim.SetBool("GoDown",true);
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject == platform)
        {
            anim.SetBool("GoUp", true);
            
        }

    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject == platform)
        {
            anim.SetBool("GoUp", false);
            anim.SetBool("GoDown", false);
        }
    }
}
