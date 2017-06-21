using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Com.LuisPedroFonseca.ProCamera2D;


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

    GameObject player;
    PlayerMovement playerMovement;
    Shooting playerShooting;
    float timeScaleStore;
    bool isPaused;
    TimeSlow timeSlow;

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

        player = GameObject.FindGameObjectWithTag("Player");
        playerMovement = player.GetComponent<PlayerMovement>();
        playerShooting = player.GetComponent<Shooting>();
        iconTransform.position = firstPoint.position;
        timeSlow = player.GetComponent<TimeSlow>();

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

        else if (!isPaused && !timeSlow.isSlow)
        {
            Time.timeScale = timeScaleStore;
            playerMovement.enabled = true;
            playerShooting.enabled = true;
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
