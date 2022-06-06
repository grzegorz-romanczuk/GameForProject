using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M249AmmoPickup : MonoBehaviour
{
    private GameObject M249;


    // Start is called before the first frame update
    void Start()
    {
        M249 = GameObject.Find("Player/root/pelvis/spine_01/spine_02/spine_03/clavicle_r/upperarm_r/lowerarm_r/hand_r/Weapons/M249");

        Destroy(gameObject, 120);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Contains("Player") && M249.GetComponent<Gun>().Ammo < M249.GetComponent<Gun>().maxAmmo)
        {
            M249.GetComponent<Gun>().AmmoBought();
            Destroy(gameObject);
        }
    }
    void OnCollisionEnter(Collision other)
    {

    }
}
