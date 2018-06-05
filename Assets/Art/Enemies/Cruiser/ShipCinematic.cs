using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipCinematic : MonoBehaviour
{
    public Transform target;
    public float moveSpeed;
    public float WarmupRate = 3.0f;
    public float ShootDuration = 0.3f;
    ShootingState shootingState;
    public float timer;
    bool ExpSearch;
    LineRenderer Laser;
    public GameObject LaserCharge;
    public GameObject Explosion;

    //private Quaternion Startrot = Quaternion.Euler(90, 90, 90);
    void Start()
    {
        Laser = GetComponent<LineRenderer>();
    }
    void ExplosionSearch()
    {
        Vector3 fwd = this.transform.forward; //TransformDirection(Vector3.forward);
        Vector3 pos = transform.position;
        RaycastHit hit;
        Ray ray = new Ray(pos, fwd);
        if (Physics.Raycast(ray.origin, ray.direction, out hit))
        {
            if (hit.collider != null)
            {
                Explosion.transform.position = hit.point;
                print("LeunMeme");
            }
            
        }
        else
        {
            print("Goopieman");
        }
    }

    private void Update()
    {
        ActorMove();
        ShootToot();
    }

    void ActorMove()
    {
        transform.position = Vector3.Lerp(transform.position, target.position, moveSpeed * Time.deltaTime);
        transform.LookAt(target);
        Vector2 direction = transform.position - target.position;
        direction.Normalize();
        if (ExpSearch == false)
        {
            ExpSearch = true;
            ExplosionSearch();
        }
    }
    void ShootToot()
    {
        switch (shootingState)
        {
            case ShootingState.WarmUp:

                timer += Time.deltaTime;
                LaserCharge.SetActive(true);

                if (timer >= WarmupRate)
                {
                    shootingState = ShootingState.Shooting;
                    LaserCharge.SetActive(false);
                }
                break;

            case ShootingState.Shooting:

                Laser.enabled = true;
                Explosion.SetActive(true);

                timer -= Time.deltaTime / ShootDuration;

                if (timer <= 0)
                {
                    Laser.enabled = false;
                    Explosion.SetActive(false);
                    shootingState = ShootingState.WarmUp;
                }
                break;
        }
    }
}
public enum ShootingState
{
    WarmUp,
    Shooting
}
