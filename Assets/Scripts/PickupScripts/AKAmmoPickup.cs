using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AKAmmoPickup : MonoBehaviour
{
    private GameObject AK;


    // Start is called before the first frame update
    void Start()
    {
        AK = GameObject.Find("Player/root/pelvis/spine_01/spine_02/spine_03/clavicle_r/upperarm_r/lowerarm_r/hand_r/Weapons/AK74");
        

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag.Contains("Player"))
        {
            AK.GetComponent<Gun>().AmmoBought();
            Destroy(gameObject);
        }
    }
}
