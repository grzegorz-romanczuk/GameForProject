using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartPickup : MonoBehaviour
{
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        Destroy(gameObject,120);

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Contains("Player") && player.GetComponent<PlayerHealth>().currentHealth < player.GetComponent<PlayerHealth>().maxHealth)
        {
            player.GetComponent<PlayerHealth>().AddHealth(1);
            Destroy(gameObject);
        }
    }
    void OnCollisionEnter(Collision other)
    {
        
    }
}
