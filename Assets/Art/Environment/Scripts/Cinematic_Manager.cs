using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cinematic_Manager : MonoBehaviour {

    BoxCollider2D Collider;
    GameObject Player;
    GameObject CinematicMaster;

    // Use this for initialization
    void Start ()
    {
    Collider = GetComponent<BoxCollider2D>();
    Player = GameObject.Find("Player");
    CinematicMaster = GameObject.Find("Cinematic_Cruisers");
    CinematicMaster.SetActive(false);
    
    }
    void OnTriggerEnter2D(Collider2D Other)
    {
        //Start the cinematic when the player enters the trigger
        if (Other == Player.GetComponent<BoxCollider2D>())
        {
            CinematicMaster.SetActive(true);
        }
    }

}
