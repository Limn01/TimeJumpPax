using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LifeManager : MonoBehaviour
{
    public static LifeManager instance;
    public int startingLives;

    Text theText;
    int lifeCounter;

    [SerializeField]
    GameObject gameOverScreen;
    [SerializeField]
    float restartDelay = 5f;
    [SerializeField]
    Transform restartPoint;
    [SerializeField]
    float restartTimer;

    [SerializeField]
    GameObject player;
    
    
    

    void Awake()
    {
        instance = this;

        player = GameObject.FindGameObjectWithTag("Player");
        
    }

    void Start()
    {
        theText = GetComponent<Text>();

        lifeCounter = startingLives;
    }

    void Update()
    {
        if (lifeCounter < 0)
        {
            gameOverScreen.SetActive(true);
            player.SetActive(false);
            restartTimer += Time.deltaTime;

            if (restartTimer >= restartDelay)
            {
                SceneManager.LoadScene("Level1");
                //player.transform.position = restartPoint.position;
                //player.SetActive(true);
                //gameOverScreen.SetActive(false);
                //lifeCounter = startingLives;
            }
            //PlayerMovement.instance.gameObject.SetActive(false);
            
        }

        

        theText.text = "x " + lifeCounter;
    }

    public void GiveLife()
    {
        lifeCounter++;
    }

    public void TakeLife()
    {
        lifeCounter--;
    }
}
