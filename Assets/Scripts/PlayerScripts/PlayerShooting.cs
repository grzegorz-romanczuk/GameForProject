using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerShooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform gunPoint;
    public float speed = 100f;
    public float cooldown = 1f;
    float nextShot = 0;
    int currentWeapon = 0;
    List<int> weaponAmmo = new List<int>();

    private void Start()
    {
        createAmmoList();
        SelectWeapon();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && nextShot < Time.time)
        {
            CheckAmmo();                      
        }
    }

    void CheckAmmo()
    {
        if (currentWeapon == 0 || weaponAmmo[currentWeapon] > 0)
        {
            Shoot();
        }        
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, gunPoint);
        bullet.transform.parent = null;
        bullet.GetComponent<Rigidbody>().AddForce(transform.forward * speed * 100);
        nextShot = Time.time + cooldown;
    }

    void SelectWeapon()
    {
        int i = 0;
        foreach(Transform weapon in transform)
        {
            if (i == currentWeapon) 
            {
                weapon.gameObject.SetActive(true);
            } 
            else 
                weapon.gameObject.SetActive(false);
            i++;
        }
    }

    void createAmmoList()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            weaponAmmo.Add(0);       
        }        
    }
}
