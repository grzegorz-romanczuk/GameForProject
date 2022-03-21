using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform gunPoint;
    public float bulletSpeed = 100f;
    public float fireRate = 0.5f;    
    public int maxAmmo = 100;
    public bool ammoIsInfinite = false;
    public bool isFullAuto = false;
    public int bulletDamage = 1;

    private int currentAmmo = 0;
    private float nextShot = 0;
       

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && nextShot <= Time.time && !isFullAuto)
        {
            CheckAmmo();
        }
        else if (Input.GetMouseButton(0) && nextShot <= Time.time && isFullAuto)
        {
            CheckAmmo();
        }
    }    
    void CheckAmmo()
    {
        if (ammoIsInfinite)
        {
            Shoot();
        }
        else if (currentAmmo > 0)
        {
            currentAmmo--;
            Shoot();
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, gunPoint);
        bullet.transform.parent = null;
        bullet.GetComponent<Rigidbody>().AddForce(transform.forward * bulletSpeed * 100);
        bullet.GetComponent<PlayerBullet>().setBulletDamage(bulletDamage);
        nextShot = Time.time + fireRate;
    }   

}
