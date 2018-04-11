using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotWormHealth : MonoBehaviour
{
    public float startingHealth = 2f;
    public float currentHealth;

    bool isDamaged;
    bool isDead;
    
    private void OnEnable()
    {
        currentHealth = startingHealth;
    }

    public void TakeDamage(float amount)
    {
        isDamaged = true;

        currentHealth -= amount;

        if (currentHealth <= 0 && !isDead)
        {
            Death();
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

        this.gameObject.SetActive(false);
    }
}
