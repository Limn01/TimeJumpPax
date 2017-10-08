using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Com.LuisPedroFonseca.ProCamera2D;

public class LevelManager : MonoBehaviour
{
    public GameObject currentCheckPoint;

    [SerializeField]
    float restartDelay = 1f;
    [SerializeField]
    float restartTimer;
    [SerializeField]
    Animator anim;
    PlayerHealth playerHealth;
    Player playerMovement;
    GameObject cameraGame;
    [SerializeField]
    ProCamera2D proCamera;
    GameObject camera;
    ProCamera2DTransitionsFX transition;
    GameObject player;
    GameObject gameOver;

    // Use this for initialization
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
        playerMovement = player.GetComponent<Player>();
        camera = GameObject.FindGameObjectWithTag("MainCamera");
        proCamera = camera.GetComponent<ProCamera2D>();
        transition = camera.GetComponent<ProCamera2DTransitionsFX>();
        gameOver = GameObject.FindGameObjectWithTag("GameOver");

        transition.TransitionEnter();
    }

    private void Start()
    {
       
    }

    private void Update()
    {
        if (playerHealth.CurrentHealth <= 0 && LifeManager.instance.lifeCounter < 0)
        {
            Debug.Log("Stopped Respawn");
            gameOver.SetActive(true);
        }
    }

    public IEnumerator RespawnPlayer()
    {
        while (LifeManager.instance.lifeCounter != 0)
        {
            player.gameObject.SetActive(false);
            playerMovement.enabled = false;
            transition.TransitionExit();

            yield return new WaitForSeconds(3f);

            player.gameObject.SetActive(true);
            player.transform.position = currentCheckPoint.transform.position;

            yield return new WaitForSeconds(1.5f);
            transition.TransitionEnter();
            playerMovement.enabled = true;
            LifeManager.instance.TakeLife();
            playerHealth.FullHealth();
        }
    }
}


