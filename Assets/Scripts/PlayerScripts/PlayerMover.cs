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
    public int maxStamina = 20;
    public int staminaUsage = 10;
    public bool infiniteStamina = false;
    private int stamina;
    private float staminaRegenTime = 0f;
    public GameObject WeaponBelt;
    void Awake()
    {
        _input = GetComponent<InputHandler>();
        _playerAim = GetComponent<PlayerAim>();
        stamina = maxStamina;
    }

    void Update()
    {
        if (!PauseSystem.gameIsPaused)
        {
            if (!isDashing)
            {
                targetVector = new Vector3(_input.InputVector.x, 0, _input.InputVector.y);
                Move(targetVector);
            }
            else Dash(dashVector);

            if (Input.GetKeyDown(KeyCode.Space) && (!isDashing && (stamina >= staminaUsage || infiniteStamina))) StartDash();

            if (Vector3.Distance(targetVector, Vector3.zero) > 0.25f) GetComponent<Animator>().SetBool("IsRunning", true);
            else GetComponent<Animator>().SetBool("IsRunning", false);

            if (stamina < maxStamina && staminaRegenTime < Time.fixedTime)
            {
                stamina++;
                staminaRegenTime = Time.fixedTime + 0.5f;

            }
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
        var mousePoint = _playerAim.GetMousePoint();        //pozycja myszki na ekranie
        mousePoint.y = 0;
        dashVector = new Vector3(_input.InputVector.x, 0, _input.InputVector.y).normalized;                     //Vector kierunku wykonania uniku
        dashVector = Quaternion.Euler(0, Camera.gameObject.transform.rotation.eulerAngles.y, 0) * dashVector;   //przeliczenei Vectoru zgodnie z kamer�      
        if (dashVector == Vector3.zero)
        {
            dashVector = (mousePoint - transform.position).normalized;   //zmiana kierunku dashu w strone myszki w przypadku gdy grasz si� nie porusza
        }
        ChangeDashComponentsState(false);                   //zmiana odpowiednich komponent�w gracza na czas uniku
        if(!infiniteStamina)stamina -= staminaUsage;        //odj�cie staminy 
        isDashing = true;                                   //zmiana stanu prouszania na unik
        Invoke(nameof(EndDash), dashTime);                  //zako�czenie uniku po okre�lonym czasie
    }

    private void ChangeDashComponentsState(bool state)
    {
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("EnemyBullet"), !state);
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"), !state);
        WeaponBelt.SetActive(state);
        GetComponent<PlayerAim>().enabled = state;
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
