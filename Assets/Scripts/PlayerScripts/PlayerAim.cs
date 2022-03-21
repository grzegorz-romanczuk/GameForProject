using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerAim : MonoBehaviour
{

    private InputHandler inputo;

    // Start is called before the first frame update
    void Start()
    {
        inputo = GetComponent<InputHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        RotateFromMouseVector();

    }
    private void RotateFromMouseVector()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, maxDistance: 100f))
        {
            var target = hitInfo.point;
            target.y = transform.position.y;
            transform.LookAt(target);
        }
    }
}

