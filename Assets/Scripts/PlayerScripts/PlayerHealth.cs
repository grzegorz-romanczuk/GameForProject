using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerHealth : Health
{
    private float invulnerabilityTime = 0f;
    public GameObject GameOver;
    public AudioClip deathClip;
    private AudioSource audioSource;
    [Header("Armor")]
    public int startingArmor = 1;
    public int currentArmor = 0;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        currentArmor = startingArmor;
        audioSource = GetComponent<AudioSource>();
        if(!GameOver) GameOver = GameObject.Find("GameOverCanvas");
    }

    public void DoDamage(int damage)
    {
        if (invulnerabilityTime <= Time.time && !invulnerable)
        {
            invulnerabilityTime = Time.time + hitCooldown;

            if (currentArmor > 0)
            {
                currentArmor--;
                audioSource.Play();
            }
            else
            {
                currentHealth -= damage;
                audioSource.Play();
            }
            if (currentHealth <= 0)
            {
                audioSource.clip = deathClip;
                audioSource.Play();
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

    public void AddHealth(int amount)
    {
        currentHealth += amount;
    }

    public void infHPDisabler()
    {
        StopCoroutine(nameof(disableInfHP));
        StartCoroutine(nameof(disableInfHP), 10f);
    }
    IEnumerator disableInfHP(float time)
    {
        yield return new WaitForSeconds(time);
        invulnerable = false;
    }
}
