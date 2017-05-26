using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
