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
    public int stamina = 20;
    private float staminaRegenTime = 0f;
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

        if (Input.GetKeyDown(KeyCode.Space) && !isDashing && stamina >= 10) StartDash(); 

        if (Vector3.Distance(targetVector, Vector3.zero) > 0.25f) GetComponent<Animator>().SetBool("IsRunning", true);
        else GetComponent<Animator>().SetBool("IsRunning", false);
        
        if(stamina < 20 && staminaRegenTime < Time.fixedTime)
        {
            stamina++;
            staminaRegenTime = Time.fixedTime + 0.5f;

        }

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
        ChangeDashComponentsState(false);
        stamina -= 10;
        isDashing = true;
        Invoke(nameof(EndDash), dashTime);
    }

    private void ChangeDashComponentsState(bool state)
    {
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("EnemyBullet"), !state);
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"), !state);
        GetComponent<Rigidbody>().isKinematic = !state;
    }
    private void Dash(Vector3 targetVector)
    {
        var speed = moveSpeed * Time.deltaTime * dashPower;        
        var targetPosition = transform.position + targetVector * speed;
        transform.position = targetPosition;
    }

    private void EndDash()
    {
        ChangeDashComponentsState(true);
        isDashing = false;
    }

    public bool GetIsDashing()
    {
        return isDashing;
    }
}
