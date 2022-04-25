using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Health : MonoBehaviour
{
    public int maxHealth = 5;
    public int currentHealth;
    public float hitCooldown = 0.1f;
    public bool invulnerable = false;
    public GameObject[] TurnOffObjects;
    public float destroyTime = 1f;   
    

    private void Start()
    {
                  
    }
    
    public void DisableObjects()
    {
        foreach (var item in TurnOffObjects)
        {
            item.SetActive(false);
        }
    }

    public void DestroyUnit(float time)
    {
        Destroy(gameObject, time);
    }

}