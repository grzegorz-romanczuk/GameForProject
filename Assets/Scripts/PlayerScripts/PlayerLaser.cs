using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLaser : MonoBehaviour
{
    public LineRenderer laserRender;
    public float laserWidth = 0.1f;
    public float laserMaxLength = 5f;
    Vector3 laserOrigin;
    Vector3 laserEnd;
    public GameObject gunPoint;
    // Start is called before the first frame update
    void Start()
    {
        laserOrigin = gunPoint.transform.position;
        laserEnd = gunPoint.transform.forward * 20 + gunPoint.transform.position;
        Vector3[] laserPositions = new Vector3[2] { laserOrigin, laserEnd };
        laserRender.SetPositions(laserPositions);
        laserRender.startWidth = laserWidth;
        laserRender.endWidth = laserWidth;
    }
    // Update is called once per frame
    void Update()
    {
        laserOrigin = gunPoint.transform.position;
        laserEnd = gunPoint.transform.forward * 20 + gunPoint.transform.position;
        Vector3[] laserPositions = new Vector3[2] { laserOrigin, laserEnd };
        laserRender.SetPositions(laserPositions);
    }
}