using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(EnemyStats))]
public class EnemyHealth : Health
{
    private float invulnerabilityTime = 0f;
    private GameObject gameManager;
    private EnemyStats enemyStats;
    // Start is called before the first frame update
    void Awake()
    {
        enemyStats = GetComponent<EnemyStats>();
        var health = enemyStats.health;
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
                //gameManager.GetComponent<DefeatedEnemyCount>().DefeatedEasyEnemy += 1; //Odsy?am informacj? o pokonanym easy przeciwniku
                gameManager.GetComponent<PlayerMoney>().AddMoney(enemyStats.cashValue);
                gameManager.GetComponent<PlayerScore>().AddScore(enemyStats.score);
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
