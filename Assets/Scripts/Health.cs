using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth = 5;
    public int currentHealth;
    public float hitCooldown = 0.1f;

    private float invulnerabilityTime = 0f;
    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void DoDamage(int damage)
    {
        if (invulnerabilityTime <= Time.time)
        {
            invulnerabilityTime = Time.time + hitCooldown;
            currentHealth -= damage;
            if (currentHealth <= 0)
            {
                DestroyUnit();
            }
        }
    }

    private void DestroyUnit()
    {
        Destroy(gameObject);
    }

}
