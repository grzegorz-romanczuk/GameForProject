using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public float bulletLifeTime = 2f;
    private int bulletDamage = 1;
    
    void Start()
    {
        Invoke("DestroyBullet", bulletLifeTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        //damage Enemy
        if (other.gameObject.tag.Contains("Enemy"))
        {
            other.gameObject.GetComponent<Health>().DoDamage(bulletDamage);
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
        gameObject.GetComponent<Rigidbody>().velocity *= 0.2f;
        Destroy(gameObject, gameObject.GetComponent<TrailRenderer>().time);
    }
}
