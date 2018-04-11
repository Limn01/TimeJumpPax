using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Com.LuisPedroFonseca.ProCamera2D;

public class EnemyHealth : MonoBehaviour
{  
    public float startingHealth;
    public float currentHealth;
    public float flashSpeed;
    public GameObject FlashingDamage;
    public float timeBetweenHit;

    bool isHit;
    bool isDead;

    float timer;

    AudioManager audio;
    Renderer render;

    private void OnEnable()
    {
        render = GetComponentInChildren<SpriteRenderer>();
        audio = FindObjectOfType<AudioManager>();

        currentHealth = startingHealth;
        isDead = false;
    }

    private void Update()
    {
        if (isHit)
        {
            InvokeRepeating("DamageFlash", 0, .05f);
        }

        

        //if (isHit)
        //{
        //    render.material.color = Color.clear;
        //}
        //else
        //{
        //    render.material.color = Color.Lerp(render.material.color, Color.white, flashSpeed * Time.deltaTime);
        //}

        isHit = false;
    }

    public void TakeDamage(float amount)
    {
        isHit = true;

        if (isDead)
        {
            return;
        }

        audio.Play("EnemyHurt");
        
        currentHealth -= amount;

        if (currentHealth <= 0 && !isDead)
        {
            Death();
        }
    }

    void Death()
    {
        isDead = true;

        //GameObject obj = ExplosionsPool.current.GetPooledObject();
        //List<GameObject> pooledObj = new List<GameObject>();
        //pooledObj.Add(obj);
        //int randomIndex = Random.Range(0, pooledObj.Count);
        //if (!pooledObj[randomIndex].activeInHierarchy)
        //{
        //    pooledObj[randomIndex].SetActive(true);
        //    pooledObj[randomIndex].transform.position = transform.position;
        //    pooledObj[randomIndex].transform.rotation = transform.rotation;
        //}

        gameObject.SetActive(false);
    }

    void DamageFlash()
    {
        if (FlashingDamage.activeSelf)
        {
            FlashingDamage.SetActive(false);
        }
        else
        {
            FlashingDamage.SetActive(true);
        }
    }
}



