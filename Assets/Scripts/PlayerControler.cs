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

    [Header("AttackSettings")]
    [SerializeField] float _AttackSpeed;
    bool _IsAttacking;

    [Header("DashSettings")]
    [SerializeField] float _Dashspeed;
    [SerializeField] float _DashDuration;
    bool _IsDashing;

    [Header("Animator")]
    [SerializeField] Animator _Animator;
    [SerializeField] Transform _PlayerSprite;
    float _Timer;

    Vector3 _Movement;

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
            _Animator.SetBool("Attack", true);
            _IsAttacking = true;
            yield return new WaitForSeconds(1);
            _IsAttacking = false;
            _Animator.SetBool("Attack", false);
        }
    }
    IEnumerator Dash()
    {
        Vector3 storedvelocity;
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            _IsDashing = true;
            storedvelocity = _RB.velocity;
            _RB.velocity = _Movement.normalized * _Dashspeed;
            yield return new WaitForSeconds(0.15f);
            _RB.velocity = storedvelocity;
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
                _Animator.speed = 1;
                _Animator.SetBool("Right", true);
                _Animator.SetBool("Up", false);
                _Animator.SetBool("Down", false);
                _Animator.SetBool("Idle", false);
                _Timer = 0;
                _PlayerSprite.rotation = new Quaternion(0, 0, 0, 0);
                break;

            case LookDirectionEnum.left:
                _Animator.speed = 1;
                _Animator.SetBool("Right", true);
                _Animator.SetBool("Up", false);
                _Animator.SetBool("Down", false);
                _Animator.SetBool("Idle", false);
                _Timer = 0;
                _PlayerSprite.rotation = new Quaternion(0, 180, 0, 0);
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
    void LookDirection()
    {
        if (_Movement.z > 0)
        {
            _LookDirection = LookDirectionEnum.up;
        }

        else if (_Movement.z < 0)
        {
            _LookDirection = LookDirectionEnum.down;
        }

        else if (_Movement.x < 0)
        {
            _LookDirection = LookDirectionEnum.left;
        }

        else if (_Movement.x > 0)
        {
            _LookDirection = LookDirectionEnum.right;
        }
        else
        {
            _LookDirection = LookDirectionEnum.idle;
        }

    }

    private void FixedUpdate()
    {
        if (!_IsAttacking)
        {
            _RB.MovePosition(_RB.position + _Movement.normalized * Time.fixedDeltaTime * _Speed);
        }
    }
}
