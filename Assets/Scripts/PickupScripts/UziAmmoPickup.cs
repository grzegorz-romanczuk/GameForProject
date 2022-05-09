using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UziAmmoPickup : MonoBehaviour
{
    private GameObject Uzi;


    // Start is called before the first frame update
    void Start()
    {
        Uzi = GameObject.Find("Player/root/pelvis/spine_01/spine_02/spine_03/clavicle_r/upperarm_r/lowerarm_r/hand_r/Weapons/Uzi");


    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag.Contains("Player"))
        {
            Uzi.GetComponent<Gun>().AmmoBought();
            Destroy(gameObject);
        }
    }
}
