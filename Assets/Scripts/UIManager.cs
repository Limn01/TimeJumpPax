using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public Animator titleAnim;
    public Animator portalAnim;
    public Animator fadeOut;
    public float waitChangeScene;
    public AudioSource selectSound;
    public AudioSource mainmenuMusic;
    public Animator select;
    public float fadeOutWait;

    private void Start()
    {
        titleAnim.SetTrigger("IsOpen");
        portalAnim.SetTrigger("IsOn");
    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            select.SetTrigger("Select");
            selectSound.Play();
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

        SceneManager.LoadScene("Level1");
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }
}
