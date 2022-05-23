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
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag.Contains("Player"))
        {
            player.GetComponent<PlayerMover>().moveSpeed *= 2;
            player.GetComponent<PlayerMover>().DBSpeedDisabler();
            Destroy(gameObject);
        }
    }
}
