using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadStomp : MonoBehaviour
{
    public float damage = 1;
    public float bounceOnEnemy;
    public bool jumpOn = false;
    
    Player player;
    
    private void Start()
    {
        player = GetComponentInParent<Player>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            jumpOn = true;
            EnemyHealth enemyHealth = other.gameObject.GetComponent<EnemyHealth>();
            enemyHealth.TakeDamage(damage);
        }
        else if (other.gameObject.CompareTag("FlyingEnemies"))
        {
            jumpOn = true;
            FlyingEnemies flyingEnemies = other.gameObject.GetComponent<FlyingEnemies>();
            flyingEnemies.TakeDamage(damage);
        }

        else if (other.gameObject.CompareTag("Turrent"))
        {
            jumpOn = true;
            Turrent turrent = other.gameObject.GetComponent<Turrent>();
            turrent.TakeDamage(damage);
        }
    }
}
