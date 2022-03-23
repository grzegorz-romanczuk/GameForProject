using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public bool isUnlocked = false;
    public GameObject bulletPrefab;
    public Transform gunPoint;
    public float bulletSpeed = 100f;
    public float fireRate = 0.5f;
    public float recoil = 0f;
    public int maxAmmo = 100;
    public int Ammo = 0;
    public int magazineSize = 30;
    public bool ammoIsInfinite = false;
    public bool isFullAuto = false;
    public int bulletDamage = 1;

    public int currentAmmo = 0;    
    private float nextShot = 0;
    private bool isShooting = false;
       

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && nextShot <= Time.time && !isFullAuto)
        {            
            CheckAmmo();
        }
        else if (Input.GetMouseButton(0) && nextShot <= Time.time && isFullAuto)
        {
            isShooting = true;            
            CheckAmmo();
        }
        if(Input.GetMouseButtonUp(0)) isShooting = false;
    }    
    void CheckAmmo()
    {
        if (ammoIsInfinite)
        {            
            if (currentAmmo > 0)
            {
                currentAmmo--;
                Shoot();
            }
            else
            {
                //reload
            }
        }
        else
        {
            if (currentAmmo > 0)
            {
                currentAmmo--;
                Shoot();
            }
            else if (Ammo > 0)
            {
                //reload
            }
            else
            {
                //no ammo
            }
        }
        
    }

    void Shoot()
    {
        var recoilValue = Random.Range(-recoil, recoil);
        GameObject bullet = Instantiate(bulletPrefab, gunPoint.position, Quaternion.Euler(0f, recoilValue, 0f));
        bullet.transform.parent = null;
        bullet.GetComponent<Rigidbody>().AddRelativeForce(transform.forward * bulletSpeed * 100);
        bullet.GetComponent<PlayerBullet>().SetBulletDamage(bulletDamage);
        nextShot = Time.time + fireRate;
    }   

    public bool IsShooting()
    {
        return isShooting;
    }
}
