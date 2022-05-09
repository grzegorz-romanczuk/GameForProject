using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M4AmmoPickup : MonoBehaviour
{
    private GameObject M4;


    // Start is called before the first frame update
    void Start()
    {
        M4 = GameObject.Find("Player/root/pelvis/spine_01/spine_02/spine_03/clavicle_r/upperarm_r/lowerarm_r/hand_r/Weapons/M4");


    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag.Contains("Player"))
        {
            M4.GetComponent<Gun>().AmmoBought();
            Destroy(gameObject);
        }
    }
}
