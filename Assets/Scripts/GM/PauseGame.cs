using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Com.LuisPedroFonseca.ProCamera2D;


public class PauseGame : MonoBehaviour
{
    [SerializeField]
    GameObject pauseScreen;
    [SerializeField]
    Transform firstPoint;
    [SerializeField]
    Transform secondPoint;
    [SerializeField]
    Transform iconTransform;
    [SerializeField] float moveSpeed;

    GameObject player;
    AudioManager audioManager;
    Player playerMovement;
    Shooting playerShooting;
    float timeScaleStore;
    bool isPaused;
    

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerShooting = player.GetComponent<Shooting>();
        playerMovement = player.GetComponent<Player>();
        audioManager = FindObjectOfType<AudioManager>();
    }

    private void OnEnable()
    {
        iconTransform.position = firstPoint.position;

        timeScaleStore = Time.timeScale;
    }

    // Update is called once per frame
    void Update ()
    {
        if (isPaused)
        {
            Time.timeScale = 0f;
            playerMovement.enabled = false;
            playerShooting.enabled = false;
            pauseScreen.SetActive(true);
        }

        else if (!isPaused)
        {
            Time.timeScale = timeScaleStore;
            playerMovement.enabled = true;
            playerShooting.enabled = true;
            pauseScreen.SetActive(false);
        }
       
        if (Input.GetButtonDown("Pause") && !isPaused)
        {
            isPaused = !isPaused;
            audioManager.Play("PauseSelect");
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
            SceneManager.LoadScene("MainMenu");
        }

        if (iconTransform.position == firstPoint.position && Input.GetButtonDown("Jump") && isPaused)
        {
            isPaused = false;
        }
	}
}
