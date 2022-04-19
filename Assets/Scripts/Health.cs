using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Health : MonoBehaviour
{
    public int maxHealth = 5;
    public int currentHealth;
    public float hitCooldown = 0.1f;
    public bool isPlayer = false, invulnerable = false;
    public GameObject[] TurnOffObjects;
    public float destroyTime = 1f;

    private float invulnerabilityTime = 0f;
    private void Start()
    {
        if (!isPlayer)
        {
            var health = GetComponent<EnemyAi>().maxHealth;
            if (health > 0) maxHealth = health;
        }
        currentHealth = maxHealth;
    }

    public void DoDamage(int damage)
    {
        if (invulnerabilityTime <= Time.time && !invulnerable)
        {
            invulnerabilityTime = Time.time + hitCooldown;
            currentHealth -= damage;
            if (currentHealth <= 0)
            {
                if (isPlayer)
                {                   
                                        
                    GetComponent<PlayerAim>().enabled = false;
                    GetComponent<PlayerMover>().enabled = false;
                    GetComponent<Animator>().SetTrigger("Death");
                    GetComponent<Rigidbody>().isKinematic = true;
                    GetComponent<BoxCollider>().enabled = false;                   
                    if (TurnOffObjects.Length > 0) DisableObjects();
                    Destroy(this);
                }
                else 
                {
                    GameObject.Find("GameManager").GetComponent<DefeatedEnemyCount>().DefeatedEasyEnemy+=1; //Odsy?am informacj? o pokonanym easy przeciwniku
                    DestroyUnit(destroyTime);
                    
                    GetComponent<EnemyAi>().enabled = false;
                    GetComponent<NavMeshAgent>().enabled = false;
                    GetComponent<Animator>().SetTrigger("Death");
                    GetComponent<Rigidbody>().isKinematic = true;

                    if (TurnOffObjects.Length > 0) DisableObjects();
                    Destroy(this);
                }                
            }
        }
    }
    private void DisableObjects()
    {        
        foreach(var item in TurnOffObjects)
        {
            item.SetActive(false);
        }
    }
    private void DestroyUnit(float time)
    {
        Destroy(gameObject, time);
    }

}
