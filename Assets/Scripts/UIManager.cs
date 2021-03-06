﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public Animator titleAnim;
    public Animator portalAnim;
    public Animator fadeOut;
    public float waitChangeScene;
    public AudioSource mainmenuMusic;
    public Animator select;
    public float fadeOutWait;

    AudioManager audioManager;

    private void Start()
    {
        titleAnim.SetTrigger("IsOpen");
        portalAnim.SetTrigger("IsOn");
        audioManager = FindObjectOfType<AudioManager>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            select.SetTrigger("Select");
            audioManager.Play("StartGame");
            mainmenuMusic.Stop();
            StartCoroutine(ChangeScene());
        }

        if (Input.GetButtonDown("Fire2"))
        {
            Application.Quit();
        }
    } 

    IEnumerator ChangeScene()
    {
        yield return new WaitForSeconds(fadeOutWait);

        fadeOut.SetTrigger("FadeOut");

        yield return new WaitForSeconds(waitChangeScene);

        SceneManager.LoadSceneAsync("Level1");
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }
}
