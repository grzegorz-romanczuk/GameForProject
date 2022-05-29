using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleSpeedPickup : MonoBehaviour
{
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        Destroy(gameObject, 120);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Contains("Player"))
        {
            player.GetComponent<PlayerMover>().isDoubleSpeed = true;
            player.GetComponent<PlayerMover>().DBSpeedDisabler();
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision other)
    {
        
    }
}
