using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle_Follow : MonoBehaviour {

    GameObject Player;
    Vector3 Loc;

	// Use this for initialization
	void Start () {

    Player = GameObject.Find("Player");
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        Loc = new Vector3(Player.transform.position.x, this.transform.position.y, 0);
        this.transform.position = Loc;
	}
}
