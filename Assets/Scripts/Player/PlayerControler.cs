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

public enum MoveDirectionEnum
{
    up,
    down,
    left,
    right,
    idle
}

public class PlayerControler : MonoBehaviour
{
    public LookDirectionEnum _LookDirection;
    public MoveDirectionEnum _MoveDirection;
    [SerializeField] Rigidbody _RB;

    [Header("MovementSettings")]
    [SerializeField] float _Speed;
    Vector3 _Movement;
    Vector3 _Look;

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

    [Header("Refrenceses")]
    Attack _Attack;

    private void Start()
    {
        _Attack = GetComponent<Attack>();
    }

    void Update()
    {
        if (!_IsAttacking)
        {
            StartCoroutine(Dash());        
        }
        WalkAnimations();
        MoveDirection();
        if (!_Attack._IsAttacking)
        {
            LookDirection();
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
    //dont look at this, its shit, but it kinda works? i really need to learn how the unity animator works
    void WalkAnimations()
    {
        switch (_MoveDirection)
        {
            case MoveDirectionEnum.up:
                _Animator.speed = 1;
                _Animator.SetBool("Up", true);
                _Animator.SetBool("Down", false);
                _Animator.SetBool("Right", false);
                _Animator.SetBool("Idle", false);
                _Timer = 0;
                break;

            case MoveDirectionEnum.down:
                _Animator.speed = 1;
                _Animator.SetBool("Down", true);
                _Animator.SetBool("Right", false);
                _Animator.SetBool("Up", false);
                _Animator.SetBool("Idle", false);
                _Timer = 0;
                break;

            case MoveDirectionEnum.right:
                _PlayerSprite.localEulerAngles = new Vector3(0, 0, 0);
                _Animator.speed = 1;
                _Animator.SetBool("Right", true);
                _Animator.SetBool("Up", false);
                _Animator.SetBool("Down", false);
                _Animator.SetBool("Idle", false);
                _Timer = 0;
                break;

            case MoveDirectionEnum.left:
                _PlayerSprite.localEulerAngles = new Vector3(0, 180, 0);
                _Animator.speed = 1;
                _Animator.SetBool("Right", true);
                _Animator.SetBool("Up", false);
                _Animator.SetBool("Down", false);
                _Animator.SetBool("Idle", false);
                _Timer = 0;
                break;

            case MoveDirectionEnum.idle:
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

    //not used for now, might need it later, so il keep it

    //IEnumerator AttackAnimations()
    //{
    //    _Animator.speed = _AttackSpeed;
    //    _IsAttacking = true;
    //    _Animator.SetBool("Attack", true);
    //    yield return new WaitForSeconds(_AttackSpeed);
    //    _Animator.SetBool("Attack", false);
    //    _IsAttacking = false;
    //}

    //checks input to see to what direction the player has to move, also changes the movedirection enum to the correct enum
    void MoveDirection()
    {
        _Movement.x = Input.GetAxisRaw("Horizontal");
        _Movement.z = Input.GetAxisRaw("Vertical");
        if (_Movement.z > 0)
        {
            _MoveDirection = MoveDirectionEnum.up;
        }
        else if (_Movement.z < 0)
        {
            _MoveDirection = MoveDirectionEnum.down;
        }
        else if (_Movement.x < 0)
        {
            _MoveDirection = MoveDirectionEnum.left;
        }
        else if (_Movement.x > 0)
        {
            _MoveDirection = MoveDirectionEnum.right;
        }
        else
        {
            _MoveDirection = MoveDirectionEnum.idle;
        }

    }

    //checks input to see to what direction the player has to look, also changes the lookirection enum to the correct enum
    void LookDirection()
    {
        _Look.x = Input.GetAxisRaw("HorizontalArrow");
        _Look.z = Input.GetAxisRaw("VerticalArrow");
        if (_Look.z > 0)
        {
            _LookDirection = LookDirectionEnum.up;
        }
        else if (_Look.z < 0)
        {
            _LookDirection = LookDirectionEnum.down;
        }
        else if (_Look.x < 0)
        {
            _LookDirection = LookDirectionEnum.left;
        }
        else if (_Look.x > 0)
        {
            _LookDirection = LookDirectionEnum.right;
        }
        else
        {
            _LookDirection = LookDirectionEnum.idle;
        }
    }

    //fixed update for moving
    private void FixedUpdate()
    {
        _RB.MovePosition(transform.position + transform.TransformDirection(_Movement.normalized) * Time.fixedDeltaTime * _Speed);
    }
}
