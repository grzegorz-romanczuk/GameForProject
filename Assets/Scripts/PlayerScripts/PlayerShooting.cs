using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerShooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform gunPoint;
    public float speed = 10f;
    public float cooldown = 1f;
    float nextShot = 0;

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && nextShot < Time.time)
        {
            GameObject bullet = Instantiate(bulletPrefab, gunPoint);
            bullet.transform.parent = null;
            bullet.GetComponent<Rigidbody>().AddForce(transform.forward * speed * 100);
            nextShot = Time.time + cooldown;
        }

    }
}
