using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteAmmoPickup : MonoBehaviour
{
    private GameObject M4;
    private GameObject Uzi;
    private GameObject AK;

    // Start is called before the first frame update
    void Start()
    {
        M4 = GameObject.Find("Player/root/pelvis/spine_01/spine_02/spine_03/clavicle_r/upperarm_r/lowerarm_r/hand_r/Weapons/M4");
        Uzi = GameObject.Find("Player/root/pelvis/spine_01/spine_02/spine_03/clavicle_r/upperarm_r/lowerarm_r/hand_r/Weapons/Uzi");
        AK = GameObject.Find("Player/root/pelvis/spine_01/spine_02/spine_03/clavicle_r/upperarm_r/lowerarm_r/hand_r/Weapons/AK74");
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag.Contains("Player"))
        {
            M4.GetComponent<Gun>().ammoIsInfinite = true;
            M4.GetComponent<Gun>().infAmmoDisabler();
            Uzi.GetComponent<Gun>().ammoIsInfinite = true;
            Uzi.GetComponent<Gun>().infAmmoDisabler();
            AK.GetComponent<Gun>().ammoIsInfinite = true;
            AK.GetComponent<Gun>().infAmmoDisabler();
            Destroy(gameObject);
        }
    }
}
