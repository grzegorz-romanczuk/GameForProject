using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteHPPickup : MonoBehaviour
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
            player.GetComponent<PlayerHealth>().invulnerable = true;
            player.GetComponent<PlayerHealth>().infHPDisabler();
            Destroy(gameObject);
        }
    }

}
