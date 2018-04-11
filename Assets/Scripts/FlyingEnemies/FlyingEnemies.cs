using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemies : MonoBehaviour
{
    public float moveSpeed;
    public float playerRange;
    public LayerMask playerLayer;
    public Vector2 tempPos;
    public float verticalSpeed;
    public float amplitude;

    Player playerMovement;
    Transform target;
    GameObject player;
    PlayerHealth playerHealth;
    float distance;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
        playerMovement = player.GetComponent<Player>();
        target = player.GetComponent<Transform>();
    }

    void Update()
    {
        distance = (transform.position - target.position).sqrMagnitude;
        
        if (distance < 100)
        {
            transform.position = Vector3.MoveTowards(transform.position,player.transform.position,moveSpeed * Time.deltaTime);
        }
        else
        {
            transform.Translate(0, Mathf.Sin(Time.realtimeSinceStartup * verticalSpeed) * amplitude, 0);
        }
    }
}



