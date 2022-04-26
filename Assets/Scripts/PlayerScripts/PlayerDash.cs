using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InputHandler))]
public class PlayerDash : MonoBehaviour
{
    private InputHandler _input;

    [SerializeField]
    private float moveSpeed;

    [SerializeField]
    private Camera Camera;

    void Awake()
    {
        _input = GetComponent<InputHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
