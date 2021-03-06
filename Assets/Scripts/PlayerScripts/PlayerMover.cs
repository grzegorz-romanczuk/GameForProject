using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(InputHandler))]
[RequireComponent(typeof(PlayerAim))]
public class PlayerMover : MonoBehaviour
{
    private InputHandler _input;
    private PlayerAim _playerAim;
    Animator animator;
    
    [SerializeField]
    public float moveSpeed;

    [SerializeField]
    private Camera Camera;

    public bool isDoubleSpeed = false;
    private Vector3 dashVector;
    private Vector3 targetVector;
    private bool isDashing = false;
    public float dashTime = 0.5f;
    public float dashPower = 2f;    
    public int maxStamina = 20;
    public int staminaUsage = 10;
    public bool infiniteStamina = false;
    public int stamina;
    private float staminaRegenTime = 0f;
    public GameObject WeaponBelt;
    public int maxGrenades = 3;
    private int grenades = 1;
    public int throwStrength = 5;
    [Header("Aniamtion Config")]
    public float dashAnimationTime = 0.8f;
    public float force = 10f;
    public GameObject AhtungObj;
    void Awake()
    {
        _input = GetComponent<InputHandler>();
        _playerAim = GetComponent<PlayerAim>();
        stamina = maxStamina;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (!PauseSystem.gameIsPaused)
        {
            if (!isDashing)
            {
                targetVector = new Vector3(_input.InputVector.x, 0, _input.InputVector.y);
                Move(targetVector);

                
                
                var animationVector = (Quaternion.AngleAxis(-transform.localEulerAngles.y, Vector3.up) * (Quaternion.Euler(0, Camera.gameObject.transform.rotation.eulerAngles.y, 0) * targetVector));

                animator.SetFloat("x", animationVector.x);
                animator.SetFloat("y", animationVector.z);

            }
            else Dash(dashVector);

            if (Input.GetKeyDown(KeyCode.Mouse1) && grenades > 0) TryThrowAhtung();

               
            if (Input.GetKeyDown(KeyCode.Space) && (!isDashing && (stamina >= staminaUsage || infiniteStamina))) StartDash();
              

               
                      
            //if (targetVector.x < 0) GetComponent<Animator>().SetBool("IsRunningLeft", true);
            //else GetComponent<Animator>().SetBool("IsRunningLeft", false);

            //if (targetVector.z > 0) GetComponent<Animator>().SetBool("IsRunning", true);
            //else GetComponent<Animator>().SetBool("IsRunning", false);

            //if (targetVector.z < 0) GetComponent<Animator>().SetBool("IsRuningBack", true);
            //else GetComponent<Animator>().SetBool("IsRuningBack", false);

            //if (targetVector.x > 0) GetComponent<Animator>().SetBool("IsRuningRight", true);
            //else GetComponent<Animator>().SetBool("IsRuningRight", false);

            //if (Vector3.Distance(targetVector, Vector3.zero) > 0.25f) GetComponent<Animator>().SetBool("IsRunning", true);
            //else GetComponent<Animator>().SetBool("IsRunning", false);



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
        if (isDoubleSpeed) speed *= 2;
        targetVector = Quaternion.Euler(0, Camera.gameObject.transform.rotation.eulerAngles.y, 0) * targetVector;
        var targetPosition = transform.position + targetVector * speed;
        transform.position = targetPosition;
    }
    private void StartDash()
    {
        animator.SetFloat("dashMultiplier", dashAnimationTime / dashTime );
        animator.SetTrigger("Dashing");
        animator.SetBool("IsDashing", true);
        var mousePoint = _playerAim.GetMousePoint();        //pozycja myszki na ekranie
        mousePoint.y = 0;
        dashVector = new Vector3(_input.InputVector.x, 0, _input.InputVector.y).normalized;                     //Vector kierunku wykonania uniku
        dashVector = Quaternion.Euler(0, Camera.gameObject.transform.rotation.eulerAngles.y, 0) * dashVector;   //przeliczenei Vectoru zgodnie z kamer???      
        if (dashVector == Vector3.zero)
        {
            dashVector = (mousePoint - transform.position).normalized;   //zmiana kierunku dashu w strone myszki w przypadku gdy grasz si??? nie porusza
        }
        ChangeDashComponentsState(false);                   //zmiana odpowiednich komponent???w gracza na czas uniku
        if(!infiniteStamina)stamina -= staminaUsage;        //odj???cie staminy 
        isDashing = true;                                   //zmiana stanu prouszania na unik
        Invoke(nameof(EndDash), dashTime);                  //zako???czenie uniku po okre???lonym czasie
    }

    private void ChangeDashComponentsState(bool state)
    {
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("EnemyBullet"), !state);
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"), !state);
        //WeaponBelt.SetActive(state);
        //GetComponent<PlayerAim>().enabled = state;
        //GetComponent<Rigidbody>().isKinematic = !state;
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
        GetComponent<Animator>().SetBool("IsDashing", false);

    }

    public bool GetIsDashing()
    {
        return isDashing;
    }

    public void infSTMDisabler()
    {
        StopCoroutine(nameof(disableInfSTM));
        StartCoroutine(nameof(disableInfSTM), 10f);
    }

    IEnumerator disableInfSTM(float time)
    {
        yield return new WaitForSeconds(time);
        infiniteStamina = false;
    }

    public void DBSpeedDisabler()
    {
        StopCoroutine(nameof(disableDBSPD));        
        StartCoroutine(nameof(disableDBSPD), 10f);
    }

    IEnumerator disableDBSPD(float time)
    {
        yield return new WaitForSeconds(time);
        isDoubleSpeed = false;
    }

    void TryThrowAhtung()
    {
        Ahtung_Script ahtung = GetComponent<Ahtung_Script>();
        //animator.SetTrigger("AhtungComing");
       
        
        
        Vector3 tmp = transform.position;
        tmp = tmp + transform.forward;

        var grenade = Instantiate(AhtungObj, tmp, Quaternion.identity);
        grenade.GetComponent<Rigidbody>().AddRelativeForce(transform.forward * throwStrength * 10);
        grenades--;
    }
    public int getGrenades()
    {
        return grenades;
    }
    public void addGrenade()
    {
        if(grenades < maxGrenades)
        {        
        grenades++;
        }
        
    }
}
