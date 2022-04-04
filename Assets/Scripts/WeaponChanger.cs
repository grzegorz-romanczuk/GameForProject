using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponChanger : MonoBehaviour
{
    private int currentWeaponId = 0;
    private float weaponChangeCooldown = 0;
    private int numOfWeapons = 0;
    private Gun currentWeapon;
    public Animator animator;

    private void Start()
    {        
        ActiveWeapon();
        numOfWeapons = transform.childCount;
        currentWeapon = transform.GetChild(currentWeaponId).GetComponent<Gun>();
    }
    void Update()
    {
        if (!currentWeapon.IsShooting())
        {
            if (Input.inputString != "" && Time.time >= weaponChangeCooldown)
            {
                int number;
                bool is_a_number = int.TryParse(Input.inputString, out number);
                if (is_a_number && number >= 1 && number < 10)
                {
                    ChangeWeapon(--number);
                }
            }
            else if (Input.mouseScrollDelta.y > 0 && Time.time >= weaponChangeCooldown)
            {
                NextWeapon(currentWeaponId + 1);
            }
            else if (Input.mouseScrollDelta.y < 0 && Time.time >= weaponChangeCooldown)
            {
                PreviousWeapon(currentWeaponId - 1);
            }
        }
    }

    void ChangeWeapon(int weaponId)
    {        
        if(weaponId < numOfWeapons)
        {
            var selectedWeapon = transform.GetChild(weaponId).GetComponent<Gun>();
            if (selectedWeapon.isUnlocked)
            {
                if (currentWeapon.IsReloading())
                {
                    CancelReload(currentWeapon);
                }
                weaponChangeCooldown = Time.time + 0.15f;
                currentWeaponId = weaponId;
                currentWeapon = selectedWeapon;
                ActiveWeapon();

            }
        }        
    }
    
    void NextWeapon(int weaponId)
    {
        weaponChangeCooldown = Time.time + 0.1f;
        if (weaponId < numOfWeapons)
        {
            var selectedWeapon = transform.GetChild(weaponId).GetComponent<Gun>();
            if (selectedWeapon.isUnlocked)
            {
                if (currentWeapon.IsReloading())
                {
                    CancelReload(currentWeapon);
                }
                currentWeaponId = weaponId;
                currentWeapon = selectedWeapon;
                ActiveWeapon();
            }
            else
            {
                NextWeapon(weaponId + 1);
            }
        }
        else NextWeapon(0);
    }
    void PreviousWeapon(int weaponId)
    {
        weaponChangeCooldown = Time.time + 0.10f;
        if (weaponId >= 0)
        {
            var selectedWeapon = transform.GetChild(weaponId).GetComponent<Gun>();
            if (selectedWeapon.isUnlocked )
            {
                if (currentWeapon.IsReloading())
                {
                    CancelReload(currentWeapon);
                }
                currentWeaponId = weaponId;
                currentWeapon = selectedWeapon;
                ActiveWeapon();
            }
            else
            {
                PreviousWeapon(weaponId - 1);
            }
        }
        else PreviousWeapon(numOfWeapons-1);
    }
    private void ActiveWeapon()
    {
        int i = 0;
        
        foreach (Transform weapon in transform)
        {
            if (i == currentWeaponId)
            {
                animator.SetTrigger("WeaponChange");
                weapon.gameObject.SetActive(true);
                weapon.GetComponent<PlayerLaser>().Invoke("laserEnable", 0.1f);
            }
            else
            {
                weapon.gameObject.SetActive(false);
                weapon.GetComponent<PlayerLaser>().Invoke("laserDisable", 0.1f);
            }


            i++;
        }
    }

    private void CancelReload(Gun weapon)
    {
        weapon.StopReloading();
    }
}
