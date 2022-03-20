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

    private void OnCollisionEnter(Collision collision)
    {
        //damage Enemy
        if (collision.gameObject.tag.Contains("Enemy"))
        {
            //trigger enemy damage
        }

        DestroyBullet();
    }

    public void setBulletDamage(int damage)
    {
        bulletDamage = damage;
    }
    private void DestroyBullet()
    {
        gameObject.GetComponent<SphereCollider>().enabled = false;
        gameObject.GetComponent<Rigidbody>().isKinematic = true;
        Destroy(gameObject, gameObject.GetComponent<TrailRenderer>().time);
    }
}
