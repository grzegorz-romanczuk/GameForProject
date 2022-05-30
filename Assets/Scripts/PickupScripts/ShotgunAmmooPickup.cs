using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunAmmooPickup : MonoBehaviour
{
    private GameObject Shotgun;


    // Start is called before the first frame update
    void Start()
    {
        Shotgun = GameObject.Find("Player/root/pelvis/spine_01/spine_02/spine_03/clavicle_r/upperarm_r/lowerarm_r/hand_r/Weapons/Shotgun");

        Destroy(gameObject, 120);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Contains("Player") && Shotgun.GetComponent<Gun>().Ammo < Shotgun.GetComponent<Gun>().maxAmmo)
        {
            Shotgun.GetComponent<Gun>().AmmoBought();
            Destroy(gameObject);
        }
    }
    void OnCollisionEnter(Collision other)
    {

    }
}
