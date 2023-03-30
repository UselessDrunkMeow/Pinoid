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

    [Header("Speed")]
    [SerializeField] float _Speed;

    [Header("DashSettings")]
    [SerializeField] float _Dashspeed;
    [SerializeField] float _DashDuration;

    [Header("Animator")]
    [SerializeField] Animator _Animator;
    [SerializeField] Transform _PlayerSprite;

    Vector3 _Movement;

    void Update()
    {        
        Movement();
        StartCoroutine(Dash());
        Animations();
        LookDirection();
    }

    void Movement()
    {
        _Movement.x = Input.GetAxisRaw("Horizontal");
        _Movement.z = Input.GetAxisRaw("Vertical");
    }

    IEnumerator Dash()
    {
        Vector3 storedvelocity;
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            storedvelocity = _RB.velocity;
            _RB.velocity = _Movement.normalized * _Dashspeed;
            yield return new WaitForSeconds(0.15f);
            _RB.velocity = storedvelocity;
        }
    }

    void Animations()
    {
        switch (_LookDirection)
        {
            case LookDirectionEnum.up:
                _Animator.speed = 1;
                _Animator.SetBool("Up", true);
                _Animator.SetBool("Down", false);
                _Animator.SetBool("Right", false);
                _Animator.SetBool("Idle", false);
                break;

            case LookDirectionEnum.down:
                _Animator.speed = 1;
                _Animator.SetBool("Down", true);
                _Animator.SetBool("Right", false);
                _Animator.SetBool("Up", false);
                _Animator.SetBool("Idle", false);
                break;

            case LookDirectionEnum.right:
                _Animator.speed = 1;
                _Animator.SetBool("Right", true);
                _Animator.SetBool("Up", false);
                _Animator.SetBool("Down", false);
                _Animator.SetBool("Idle", false);
                _PlayerSprite.rotation = new Quaternion(0, 0, 0, 0);
                break;

            case LookDirectionEnum.left:
                _Animator.speed = 1;
                _Animator.SetBool("Right", true);
                _Animator.SetBool("Up", false);
                _Animator.SetBool("Down", false);
                _Animator.SetBool("Idle", false);
                _PlayerSprite.rotation = new Quaternion(0, 180, 0,0);
                break;

            case LookDirectionEnum.idle:
                _Animator.SetBool("Idle", true);
                _Animator.speed = 0;
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
        _RB.MovePosition(_RB.position + _Movement.normalized * Time.fixedDeltaTime * _Speed);
    }
}
