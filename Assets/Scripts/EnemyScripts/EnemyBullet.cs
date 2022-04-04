using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float bulletLifeTime = 2f;    
    private int bulletDamage = 1;
    public GameObject destroyEffect;
    Animator animator;
    
    void Start()
    {
        animator = GetComponent<Animator>();
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
        animator.SetTrigger("Destroy");
        var effect = Instantiate(destroyEffect, transform.position, Quaternion.identity);
        effect.transform.rotation = transform.rotation;
        Destroy(effect, 1f);
        gameObject.GetComponent<CapsuleCollider>().enabled = false;
        gameObject.GetComponent<Rigidbody>().velocity *= 0.2f;
        Destroy(gameObject, 0.15f);
    }
}
