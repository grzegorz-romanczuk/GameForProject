using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public float bulletLifeTime = 2f;
    private int bulletDamage = 1;
    public AudioClip enemyHitClip;
    public AudioClip terrainHitClip;

    void Start()
    {
        Invoke("DestroyBullet", bulletLifeTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        //damage Enemy
        var audio = GetComponent<AudioSource>();
        if (other.gameObject.tag.Contains("Enemy"))
        {
            EnemyHealth health;
            other.gameObject.TryGetComponent<EnemyHealth>(out health);     
            if(health) health.DoDamage(bulletDamage);            
            audio.clip = enemyHitClip;
            audio.Play();
        }
        else
        {
            
            audio.clip = terrainHitClip;
            audio.Play();
        }

        DestroyBullet();
    }

    public void SetBulletDamage(int damage)
    {
        bulletDamage = damage;
    }
    private void DestroyBullet()
    {
        gameObject.GetComponent<SphereCollider>().enabled = false;
        gameObject.GetComponent<Rigidbody>().velocity *= 0.1f;
        Destroy(gameObject, gameObject.GetComponent<TrailRenderer>().time);
    }
    
    public void ShotgunShell()
    {
        bulletDamage--;
        if(bulletDamage > 1) Invoke("ShotgunShell", 0.25f);
    }
}
