using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerHealth : Health
{
    private float invulnerabilityTime = 0f;
    public GameObject GameOver;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        GameOver = GameObject.Find("GameOverCanvas");
    }

    public void DoDamage(int damage)
    {
        if (invulnerabilityTime <= Time.time && !invulnerable)
        {
            invulnerabilityTime = Time.time + hitCooldown;
            currentHealth -= damage;
            if (currentHealth <= 0)
            {               
                GetComponent<PlayerAim>().enabled = false;
                GetComponent<PlayerMover>().enabled = false;
                GetComponent<Animator>().SetTrigger("Death");
                GetComponent<Rigidbody>().isKinematic = true;
                GetComponent<BoxCollider>().enabled = false;
                GameOver.GetComponent<GameOverFunctions>().Invoke("ActiveGameOverMenu", 3f);


                if (TurnOffObjects.Length > 0) DisableObjects();
                Destroy(this);                
            }
        }
    }
}
