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
    public float reloadTime = 2f;
    public Animator animator;
    public float reloadAnimationTime = 4f;

    private int currentAmmo = 0;    
    private float nextShot = 0;
    private bool isShooting = false;
    private bool isReloading = false;
    private float animationMultiplier;

    private PlayerMover playerMover;


    void Update()
    {
        if (Input.GetMouseButtonDown(0) && nextShot <= Time.time && !isFullAuto && !isReloading)
        {            
            CheckAmmo();
        }
        else if (Input.GetMouseButton(0) && nextShot <= Time.time && isFullAuto && !isReloading)
        {
            isShooting = true;            
            CheckAmmo();
        }

        if (Input.GetMouseButtonUp(0) && (Ammo > 0 || ammoIsInfinite))
        {
            isShooting = false;
            animator.SetBool("IsShooting", false);
        }
        if (Input.GetKeyDown(KeyCode.R) && !isReloading && (Ammo > 0 || ammoIsInfinite))
        {
            StartReloading();
            
        }
        if (playerMover.GetIsDashing() && isReloading) StopReloading();
    }

    private void Start()
    {
        animationMultiplier = reloadAnimationTime / reloadTime;
        playerMover = GameObject.Find("Player").GetComponent<PlayerMover>();        
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
                StartReloading();
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
                StartReloading();
            }
            else
            {
                //no ammo
            }
        }
        
    }

    void Shoot()
    {
        animator.SetBool("IsShooting", true);
        var recoilValue = Random.Range(-recoil, recoil);
        GameObject bullet = Instantiate(bulletPrefab, gunPoint.position, Quaternion.Euler(0f, recoilValue, 0f));
        bullet.transform.parent = null;
        bullet.transform.rotation = Quaternion.identity;
        bullet.GetComponent<Rigidbody>().AddRelativeForce(transform.forward * bulletSpeed * 100);        
        bullet.GetComponent<PlayerBullet>().SetBulletDamage(bulletDamage);
        nextShot = Time.time + fireRate;
        if(!isFullAuto) animator.SetBool("IsShooting", false);
    }

    private void StartReloading()
    {
        animator.SetTrigger("Reload");
        

        animator.SetFloat("reloadMultiplier", animationMultiplier);
        StartCoroutine("Reload");           
    }
    public void StopReloading()
    {
        isReloading = false;
    }

    public bool IsShooting()
    {
        return isShooting;
    }

    public bool IsReloading()
    {
        return isReloading;
    }

    private IEnumerator Reload()
    {
        isReloading = true;       
        yield return new WaitForSeconds(reloadTime);
        if (isReloading)
        {           
            StopReloading();
            
            if (ammoIsInfinite)
            {
                currentAmmo = magazineSize;
            }
            else if (Ammo >= magazineSize)
            {
                Ammo += currentAmmo;
                Ammo -= magazineSize;
                currentAmmo = magazineSize;
            }
            else
            {
                currentAmmo = Ammo;
                Ammo = 0;
            }                        
        }
    }
}
