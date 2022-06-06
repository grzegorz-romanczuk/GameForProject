using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTurretPlacer : MonoBehaviour
{
    public GameObject Turret;
    public GameObject Player;
    public bool isTurretInEQ = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey("t"))
        {
            if(isTurretInEQ == true)
            {
                PlaceTurret();
            }
  
            
        }
    }

    void PlaceTurret()
    {
        Vector3 tmp = Player.transform.position;
        tmp = tmp + transform.forward;
        Instantiate(Turret, tmp, Quaternion.identity);
        isTurretInEQ = false;
    }
}
