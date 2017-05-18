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

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        theText = GetComponent<Text>();

        lifeCounter = startingLives;
    }

    void Update()
    {
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
