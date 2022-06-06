using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteHPPickup : MonoBehaviour
{
    public Sprite PngImage;

    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        Destroy(gameObject, 120);
        Debug.Log("InfHp");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Contains("Player"))
        {
            player.GetComponent<PlayerHealth>().invulnerable = true;
            player.GetComponent<PlayerHealth>().infHPDisabler();
            Destroy(gameObject);
        }
    }
    void OnCollisionEnter(Collision other)
    {
        
    }

}
