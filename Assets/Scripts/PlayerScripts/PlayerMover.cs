using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(InputHandler))]
[RequireComponent(typeof(PlayerAim))]
public class PlayerMover : MonoBehaviour
{
    private InputHandler _input;
    private PlayerAim _playerAim;

    [SerializeField]
    private float moveSpeed;

    [SerializeField]
    private Camera Camera;

    private Vector3 dashVector;
    private Vector3 targetVector;
    private bool isDashing = false;
    public float dashTime = 0.5f;
    public float dashPower = 2f;

    void Awake()
    {
        _input = GetComponent<InputHandler>();
        _playerAim = GetComponent<PlayerAim>();
    }

    void Update()
    {

        if (!isDashing) 
        {
            targetVector = new Vector3(_input.InputVector.x, 0, _input.InputVector.y);
            Move(targetVector);
        }
        else Dash(dashVector);

        if (Input.GetKeyDown(KeyCode.Space) && !isDashing) StartDash(); 

        if (Vector3.Distance(targetVector, Vector3.zero) > 0.25f) GetComponent<Animator>().SetBool("IsRunning", true);
        else GetComponent<Animator>().SetBool("IsRunning", false);
        
        

    }
    private void Move(Vector3 targetVector)
    {
        var speed = moveSpeed * Time.deltaTime;        
        targetVector = Quaternion.Euler(0, Camera.gameObject.transform.rotation.eulerAngles.y, 0) * targetVector;
        var targetPosition = transform.position + targetVector * speed;
        transform.position = targetPosition;
    }
    private void StartDash()
    {
        var mousePoint = _playerAim.GetMousePoint();
        mousePoint.y = 0;
        dashVector = new Vector3(_input.InputVector.x, 0, _input.InputVector.y);
        dashVector = Quaternion.Euler(0, Camera.gameObject.transform.rotation.eulerAngles.y, 0) * dashVector;        
        if (dashVector == Vector3.zero)
        {
            dashVector = -(mousePoint - transform.position).normalized;            
        }
        isDashing = true;
        Invoke(nameof(EndDash), dashTime);
    }

    private void Dash(Vector3 targetVector)
    {
        var speed = moveSpeed * Time.deltaTime * dashPower;        
        var targetPosition = transform.position + targetVector * speed;
        transform.position = targetPosition;
    }

    private void EndDash()
    {
        isDashing = false;
    }

    public bool GetIsDashing()
    {
        return isDashing;
    }
}
