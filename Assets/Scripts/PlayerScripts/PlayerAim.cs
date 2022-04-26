using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerAim : MonoBehaviour
{
    public LayerMask usedLayers;
    private Vector3 mousePoint;
    // Update is called once per frame
    void Update()
    {
        RotateFromMouseVector();
    }
    private void RotateFromMouseVector()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, maxDistance: 300f, usedLayers))
        {
            var target = hitInfo.point;
            target.y = transform.position.y;
            mousePoint = target;
            transform.LookAt(target);
        }
    }

    public Vector3 GetMousePoint()
    {
        return mousePoint;
    }
}

