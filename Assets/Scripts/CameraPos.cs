using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPos : MonoBehaviour
{
    Vector3 cameraOffset;
    public GameObject playerObject;
    void Start()
    {        
        cameraOffset = transform.position - playerObject.transform.position;        
    }
   
    void Update()
    {
        
        
        if (playerObject)
        {
            transform.position = Vector3.Lerp(transform.position, playerObject.transform.position + cameraOffset, 0.1f);
        }
        
        //transform.position = cameraOffset + new Vector3(playerObject.transform.position.x,0,playerObject.transform.position.z);
    }
}
