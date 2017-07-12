using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Com.LuisPedroFonseca.ProCamera2D;

public class EnemyHealth : MonoBehaviour,Idamageable
{
    public float damage = 1;
   
    [SerializeField]
    float startingHealth;
    [SerializeField]
    float currentHealth;

    GameObject mainCamera;
    ProCamera2DShake shake;
    bool damaged;
    bool isDead;
    
    private void OnEnable()
    {
        currentHealth = startingHealth;
        isDead = false;
    }

    private void Awake()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        shake = mainCamera.GetComponent<ProCamera2DShake>();
    }

    public void TakeDamage(float amount)
    {
        damaged = true;
        
        currentHealth -= amount;

        if (currentHealth <= 0 && !isDead)
        {
            Death();
            shake.Shake("PlayerHit");
        }
    }

    void Death()
    {
        isDead = true;

        GameObject obj = ExplosionsPool.current.GetPooledObject();
        List<GameObject> pooledObj = new List<GameObject>();
        pooledObj.Add(obj);
        int randomIndex = Random.Range(0, pooledObj.Count);
        if (!pooledObj[randomIndex].activeInHierarchy)
        {
            pooledObj[randomIndex].SetActive(true);
            pooledObj[randomIndex].transform.position = transform.position;
            pooledObj[randomIndex].transform.rotation = transform.rotation;
        }

        gameObject.SetActive(false);
    }

}



