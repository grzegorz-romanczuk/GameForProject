using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRocket : MonoBehaviour
{
    public float bulletLifeTime = 2f;
    public float explosionRadius = 5f;
    public LayerMask enemyLayer;
    private int bulletDamage = 1;
    private ParticleSystem particles;
    public GameObject explosionEffect;

    void Start()
    {
        Invoke("DestroyBullet", bulletLifeTime);
        particles = gameObject.transform.GetChild(0).GetComponent<ParticleSystem>();
    }

    private void OnTriggerEnter(Collider other)
    {
       
        //find all enemies hit
        var hitColliders = Physics.OverlapSphere(transform.position, explosionRadius, enemyLayer);
        foreach(var hitCollider in hitColliders)
        {
            //damage Enemy
            if (hitCollider.tag.Contains("Enemy")){
                EnemyHealth health;
                hitCollider.gameObject.TryGetComponent<EnemyHealth>(out health);
                if (health) health.DoDamage(bulletDamage);
            }
        }

        DestroyBullet();
    }

    public void SetBulletDamage(int damage)
    {
        bulletDamage = damage;
    }
    private void DestroyBullet()
    {
        gameObject.GetComponent<CapsuleCollider>().enabled = false;
        gameObject.GetComponent<Rigidbody>().velocity *= 0;
        var effect = Instantiate(explosionEffect, transform.position, Quaternion.identity);
        Destroy(effect, 2);
        particles.transform.parent = null;
        particles.Stop();
        Destroy(particles, particles.main.duration);
        gameObject.SetActive(false);
        Destroy(gameObject, particles.main.duration);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);        
    }

}
