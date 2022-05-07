using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    public GameObject Magazine;


    // Start is called before the first frame update
    void Start()
    {
        Magazine.GetComponent<Collider>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag.Contains("Player"))
        {
            Destroy(gameObject);
        }
    }
}
