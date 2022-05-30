using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPGAmmoPickup : MonoBehaviour
{
    private GameObject RPG;


    // Start is called before the first frame update
    void Start()
    {
        RPG = GameObject.Find("Player/root/pelvis/spine_01/spine_02/spine_03/clavicle_r/upperarm_r/lowerarm_r/hand_r/Weapons/RPG-7");

        Destroy(gameObject, 120);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Contains("Player") && RPG.GetComponent<Gun>().Ammo < RPG.GetComponent<Gun>().maxAmmo)
        {
            RPG.GetComponent<Gun>().AmmoBought();
            Destroy(gameObject);
        }
    }
    void OnCollisionEnter(Collision other)
    {

    }
}
