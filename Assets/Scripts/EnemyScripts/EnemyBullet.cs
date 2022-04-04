using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float bulletLifeTime = 2f;
    public Animator animator;
    private int bulletDamage = 1;
    
    void Start()
    {
        Invoke("DestroyBullet", bulletLifeTime);
    }

    private void OnTriggerEnter(Collider other)
    {        
        //damage Enemy
        if (other.gameObject.tag.Contains("Player"))
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
        gameObject.GetComponent<Rigidbody>().isKinematic = true;
        animator.SetBool("IsDestroyed", true);
        Destroy(gameObject, gameObject.GetComponent<TrailRenderer>().time);
    }
}
