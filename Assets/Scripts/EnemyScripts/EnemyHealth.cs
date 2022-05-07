using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : Health
{
    private float invulnerabilityTime = 0f;
    private GameObject gameManager;
    // Start is called before the first frame update
    void Start()
    {
        var health = GetComponent<EnemyAi>().maxHealth;
        if (health > 0) maxHealth = health;
        currentHealth = maxHealth;
        gameManager = GameObject.Find("GameManager");
    }

    public void DoDamage(int damage)
    {
        if (invulnerabilityTime <= Time.time && !invulnerable)
        {
            invulnerabilityTime = Time.time + hitCooldown;
            currentHealth -= damage;
            if (currentHealth <= 0)
            {
                gameManager.GetComponent<WaveSystem>().EnemieDeath();
                gameManager.GetComponent<DefeatedEnemyCount>().DefeatedEasyEnemy += 1; //Odsy?am informacj? o pokonanym easy przeciwniku
                DestroyUnit(destroyTime);

                GetComponent<EnemyDrop>().CoinDrop();
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
