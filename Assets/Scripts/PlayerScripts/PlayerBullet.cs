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
            //trigger enemy damage
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
        gameObject.GetComponent<Rigidbody>().isKinematic = true;
        Destroy(gameObject, gameObject.GetComponent<TrailRenderer>().time);
    }
}
