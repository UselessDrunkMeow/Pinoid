using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public enum LookDirectionEnum
{
    up,
    down,
    left,
    right,
    idle
}

public class PlayerControler : MonoBehaviour
{
    [SerializeField] LookDirectionEnum _LookDirection;
    [SerializeField] Rigidbody _RB;

    [Header("MovementSettings")]
    [SerializeField] float _Speed;
    Vector3 _Movement;

    [Header("AttackSettings")]
    [SerializeField] float _AttackSpeed;  
    Vector3 _AttackRotation;
    bool _IsAttacking;

    [Header("DashSettings")]
    [SerializeField] float _Dashspeed;
    [SerializeField] float _DashDuration;
    bool _IsDashing;

    [Header("Animator")]
    [SerializeField] Animator _Animator;
    [SerializeField] Transform _PlayerSprite;
    float _Timer;

    void Update()
    {        
        if (!_IsAttacking)
        {
            Movement();
            StartCoroutine(Dash());
            WalkAnimations();
            StartCoroutine(Atack());
        }
        LookDirection();
        AttackDirections();
    }

    void Movement()
    {
        _Movement.x = Input.GetAxisRaw("Horizontal");
        _Movement.z = Input.GetAxisRaw("Vertical");
    }

    IEnumerator Atack()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {            
            StartCoroutine(AttackAnimations());
            yield return new WaitForSeconds(_AttackSpeed);
        }
    }

    IEnumerator Dash()
    {
        Vector3 storedvelocity;//The velocity the player is moving at before the dash
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            _IsDashing = true;
            storedvelocity = _RB.velocity;//Stores the velocity the player is moving at
            _RB.velocity = _Movement.normalized * _Dashspeed;
            yield return new WaitForSeconds(_DashDuration);
            _RB.velocity = storedvelocity;//Returns the velocity the player is moving at before the dash
            _IsDashing = false;   
        }
    }
    void WalkAnimations()
    {
        switch (_LookDirection)
        {
            case LookDirectionEnum.up:
                _Animator.speed = 1;
                _Animator.SetBool("Up", true);
                _Animator.SetBool("Down", false);
                _Animator.SetBool("Right", false);
                _Animator.SetBool("Idle", false);
                _Timer = 0;
                break;

            case LookDirectionEnum.down:
                _Animator.speed = 1;
                _Animator.SetBool("Down", true);
                _Animator.SetBool("Right", false);
                _Animator.SetBool("Up", false);
                _Animator.SetBool("Idle", false);
                _Timer = 0;
                break;

            case LookDirectionEnum.right:
                _PlayerSprite.localEulerAngles = new Vector3(0, 0, 0);
                _Animator.speed = 1;
                _Animator.SetBool("Right", true);
                _Animator.SetBool("Up", false);
                _Animator.SetBool("Down", false);
                _Animator.SetBool("Idle", false);
                _Timer = 0;
                break;

            case LookDirectionEnum.left:
                _PlayerSprite.localEulerAngles = new Vector3(0, 180, 0);
                _Animator.speed = 1;
                _Animator.SetBool("Right", true);
                _Animator.SetBool("Up", false);
                _Animator.SetBool("Down", false);
                _Animator.SetBool("Idle", false);
                _Timer = 0;
                break;

            case LookDirectionEnum.idle:
                float switchToIdleTime = 0.25f;
                _Timer += Time.deltaTime;
                if (_Timer > switchToIdleTime)
                {
                    _Animator.SetBool("Idle", true);
                    _Animator.SetBool("Up", false);
                    _Animator.SetBool("Down", false);
                    _Animator.SetBool("Right", false);
                }

                break;

        }
    }

    IEnumerator AttackAnimations()
    {
        _Animator.speed = _AttackSpeed;
        _IsAttacking = true;
        _Animator.SetBool("Attack", true);
        yield return new WaitForSeconds(_AttackSpeed);
        _Animator.SetBool("Attack", false);
        _IsAttacking = false;
    }

    void LookDirection()
    {
        if (_Movement.z > 0){
            _LookDirection = LookDirectionEnum.up;
        }
        else if (_Movement.z < 0){
            _LookDirection = LookDirectionEnum.down;
        }
        else if (_Movement.x < 0){
            _LookDirection = LookDirectionEnum.left;
        }
        else if (_Movement.x > 0){
            _LookDirection = LookDirectionEnum.right;
        }
        else{
            _LookDirection = LookDirectionEnum.idle;
        }

    }
    void AttackDirections()
    {
        switch (_LookDirection)
        {
            case LookDirectionEnum.up:
                _AttackRotation = new Vector3(0, 0, 90);
                break;
            case LookDirectionEnum.down:
                _AttackRotation = new Vector3(0, 0, -90);
                break;
            case LookDirectionEnum.left:
                _AttackRotation = new Vector3(-90, -0, 0);
                break;
            case LookDirectionEnum.right:
                _AttackRotation = new Vector3(90, 0, 0);
                break;
            case LookDirectionEnum.idle:
                _AttackRotation = new Vector3(0, 0, -90);
                break;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawRay(transform.position, _AttackRotation);
        print("WaAaAaAaAaA");
    }
    private void FixedUpdate()
    {
        if (!_IsAttacking)
        {
            _RB.MovePosition(transform.position + transform.TransformDirection(_Movement.normalized) * Time.fixedDeltaTime * _Speed);
        }
    }
}
