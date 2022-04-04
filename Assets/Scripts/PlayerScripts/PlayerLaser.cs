using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLaser : MonoBehaviour
{
    public LineRenderer laserRender;
    public float laserWidth = 0.1f;
    public float laserMaxLength = 15f;
    Vector3 laserOrigin;
    Vector3 laserEnd;
    public GameObject gunPoint;
    Vector3[] laserPositions = new Vector3[2];
    // Start is called before the first frame update
    void Start()
    {
        laserRender.startWidth = laserWidth;
        laserRender.endWidth = 0f;
    }
    // Update is called once per frame
    void Update()
    {
        laserEndFinder();
    }
    void laserEndFinder()
    {
        laserOrigin = gunPoint.transform.position;
        laserEnd = gunPoint.transform.forward * laserMaxLength + gunPoint.transform.position;
        if (Physics.Raycast(laserOrigin, laserEnd, out RaycastHit hitInfo, laserMaxLength))
        {
            laserEnd = hitInfo.point;
        }
        else
        {
            laserEnd = gunPoint.transform.forward * laserMaxLength + gunPoint.transform.position;
        }
        laserProjector();
    }
    void laserProjector()
    {
        laserPositions[0] = laserOrigin;
        laserPositions[1] = laserEnd;
        laserRender.SetPositions(laserPositions);
    }
    public void laserDisable()
    {
        laserRender.enabled = false;
    }
    public void laserEnable()
    {
        laserRender.enabled = true;
    }
}
