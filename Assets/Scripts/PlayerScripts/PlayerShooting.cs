using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform gunPoint;
    public float speed = 10f;
       
    void Update()
    {        
        if(Input.GetMouseButtonDown(0))
        {
            GameObject bullet = Instantiate(bulletPrefab, gunPoint);
            bullet.transform.parent = null;
            bullet.GetComponent<Rigidbody>().AddForce(transform.forward * speed * 100);

        }
        
    }    
}
