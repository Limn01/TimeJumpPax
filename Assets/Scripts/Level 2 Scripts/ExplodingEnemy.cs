using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodingEnemy : Enemy
{
    public ExplodeState explodeState;
    
    public int explodeBullets;
    public Transform explodePoint;
    public float timeBetweenEplosion;
    public float speed;
    public float verticalSpeed;
    public float amplitude;
    public GameObject enemyProjectile;
    
    float explodeTimer;
    float bulletRotation;
    float distance;
    int enemyProjectileIndex = 3;

    public Transform target;
    ObjectPooler objectPooler;

    protected override void Awake()
    {
        base.Awake();

        target = player.GetComponent<Transform>();
        objectPooler = ObjectPooler.SharedInstance;
    }

    protected override void Update()
    {
        base.Update();

        ExplodeStates();
    }

    void ExplodeStates()
    {
        switch (explodeState)
        {
            case ExplodeState.Wait:
                distance = (transform.position - target.position).sqrMagnitude;

                if (distance < 150)
                {
                    transform.position = Vector3.MoveTowards(transform.position, new Vector3(target.position.x, transform.position.y, transform.position.z), speed * Time.deltaTime);
                    transform.Translate(0, Mathf.Sin(Time.realtimeSinceStartup * verticalSpeed) * amplitude, 0);
                }

                if (transform.position.x == target.position.x)
                {
                    explodeState = ExplodeState.Explode;
                }
                break;

            case ExplodeState.Explode:
                bulletRotation = 360 / explodeBullets;
                for (int i = 0; i < explodeBullets; i++)
                {
                    if (i == 0)
                    {
                        enemyProjectile = objectPooler.GetPooledObject(enemyProjectileIndex);
                        enemyProjectile.transform.position = explodePoint.transform.position;
                        enemyProjectile.transform.rotation = Quaternion.Euler(90, 0, 0);
                        enemyProjectile.SetActive(true);
                        explodeState = ExplodeState.Cooldown;
                    }
                    else
                    {
                        float newEuler = bulletRotation * i;
                        enemyProjectile = objectPooler.GetPooledObject(enemyProjectileIndex);
                        enemyProjectile.transform.position = explodePoint.transform.position;
                        enemyProjectile.transform.rotation = Quaternion.Euler(0, 0, newEuler);
                        enemyProjectile.SetActive(true);
                        explodeState = ExplodeState.Cooldown;
                    }
                }
                break;

            case ExplodeState.Cooldown:
                explodeTimer += Time.deltaTime;

                if (explodeTimer >= timeBetweenEplosion)
                {
                    explodeTimer = 0;
                    explodeState = ExplodeState.Wait;
                }
                break;
        }
    }
}

public enum ExplodeState
{
    Wait,
    Explode,
    Cooldown
}
