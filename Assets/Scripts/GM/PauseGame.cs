using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PauseGame : MonoBehaviour
{
    public static PauseGame instance;
    [SerializeField]
    AudioSource source;
    [SerializeField]
    GameObject pauseScreen;
    [SerializeField]
    Transform firstPoint;
    [SerializeField]
    Transform secondPoint;
    [SerializeField]
    Transform iconTransform;
    [SerializeField] float moveSpeed;



    bool isPaused;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        
        DontDestroyOnLoad(gameObject);
        iconTransform.position = firstPoint.position;
    }

	// Update is called once per frame
	void Update ()
    {
        if (isPaused)
        {
            Time.timeScale = 0f;
            PlayerMovement.instance.enabled = false;
            Shooting.instance.enabled = false;
            pauseScreen.SetActive(true);
        }

        else
        {
            Time.timeScale = 1f;
            PlayerMovement.instance.enabled = true;
            Shooting.instance.enabled = true;
            pauseScreen.SetActive(false);
        }

        if (Input.GetButtonDown("Pause") && !isPaused)
        {
            isPaused = !isPaused;
            source.Play();
        }

        float v = Input.GetAxis("Vertical");

        if (v < 0)
        {
            iconTransform.position = secondPoint.position;
        }

        if (v > 0)
        {
            iconTransform.position = firstPoint.position;
        }

        if (iconTransform.position == secondPoint.position && Input.GetButtonDown("Jump") && isPaused)
        {
            SceneManager.LoadScene("Level2");
        }

        if (iconTransform.position == firstPoint.position && Input.GetButtonDown("Jump") && isPaused)
        {
            isPaused = false;
        }
	}
}
