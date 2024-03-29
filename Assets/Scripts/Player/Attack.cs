using System.Collections;
using UnityEngine;

public class Attack : MonoBehaviour
{
    PlayerControler _PlayerControler;
    AttackDamage _AttackDamage;
    Vector3 _AttackRotation;
    [Header("General Attack Settings")]
    public float _AttackDuration;
    public float _AttackCooldown;
    public int _Damage;

    [Header("Rotation Settings")]
    public float _NewRotation;
    public float _RotationSpeed;
    public bool _Flip;

    [Header("Rotation Settings")]
    public float _ForwardMoveAmount;
    public float _ForwardMSpeed;

    //public float _SideMoveAmount;
    //public float _SideMoveSpeed; 

    bool _CanAttack;
    bool returnToStartPos;
    public bool _IsAttacking;

    public float _ForwardTime;
    public float _SwingTime;
    public float _CooldownTime;

    Vector3 _NewPos;
    Vector3 _OldPos;

    [NonReorderable]
    Vector3 _NewRot;

    [SerializeField] Transform WeaponRotatePoint;
    [SerializeField] GameObject WeaponHolder;

    bool _Stop;

    void Start()
    {      
        _AttackDamage = FindObjectOfType<AttackDamage>();
        _IsAttacking = false;
        _CanAttack = true;
        _PlayerControler = GetComponent<PlayerControler>();
        _OldPos = WeaponHolder.transform.localPosition;
    }

    void FixedUpdate()
    {
        _AttackDamage._Damage = _Damage;
        if (_CanAttack)
        {
            AttackDirections();
        }
        if (_IsAttacking)
        {
            WeaponHolder.SetActive(true);
            StartSwing();
            _ForwardTime += Time.deltaTime;
            _SwingTime += Time.deltaTime;
        }
        else
        {
            WeaponHolder.SetActive(false);
            _SwingTime = 0;
            _ForwardTime = 0;
        }
        if (_ForwardTime >= _AttackDuration / 2 + _AttackCooldown)
        {
            _Stop = true;
            returnToStartPos = false;
            _ForwardTime = 0;
            _CanAttack = true;
            _IsAttacking = false;
            StartCoroutine(Stop());
        }

        _NewPos.z = -_ForwardMoveAmount;
        WeaponRotatePoint.eulerAngles = new Vector3(0, _AttackRotation.y + 180, 0);//DO NOT REMOVE THE +180. IT IS IMPORTANT.
    }

    IEnumerator Stop()
    {
        yield return new WaitForSeconds(0.01f);
        _Stop = false;
    }
    void AttackDirections()
    {
        switch (_PlayerControler._LookDirection)
        {
            case LookDirectionEnum.up:
                _AttackRotation = new Vector3(0, 180, 0);

                StartSwing();
                break;

            case LookDirectionEnum.down:
                _AttackRotation = new Vector3(0, 0, 0);

                StartSwing();
                break;

            case LookDirectionEnum.left:
                _AttackRotation = new Vector3(0, 90, 0);

                StartSwing();
                break;

            case LookDirectionEnum.right:
                _AttackRotation = new Vector3(0, 270, 0);

                StartSwing();
                break;

            case LookDirectionEnum.idle:
                _AttackRotation = new Vector3(0, 0, 0);
                break;

        }
    }

    void StartSwing()
    {
        SwingMovement();
        ForwardMovement();
        SideMovement();
    }

    void ForwardMovement()
    {
        if (!_Stop)
        {
            if (!returnToStartPos)
            {
                _IsAttacking = true;
                _CanAttack = false;
                WeaponHolder.transform.localPosition = Vector3.MoveTowards(WeaponHolder.transform.localPosition, _NewPos, _ForwardMSpeed * Time.deltaTime);
                if (_ForwardTime >= _AttackDuration / 2)
                {
                    _ForwardTime = 0;
                    returnToStartPos = true;
                }
            }

            else if (returnToStartPos)
            {
                WeaponHolder.transform.localPosition = Vector3.MoveTowards(WeaponHolder.transform.localPosition, _OldPos, _ForwardMSpeed * Time.deltaTime);
                if (_ForwardTime >= _AttackDuration / 2 + _AttackCooldown)
                {
                    returnToStartPos = false;
                    _ForwardTime = 0;
                    _CanAttack = true;
                    _IsAttacking = false;
                 
                }
            }
        }
    }
    void SwingMovement()
    {
        WeaponHolder.transform.localEulerAngles = new Vector3(0, _NewRot.y, 0);
        if (_NewRot.y < _NewRotation && !_Flip)
        {
            _NewRot.y += _RotationSpeed;
        }

        if (_NewRot.y > -_NewRotation && _Flip)
        {
            _NewRot.y -= _RotationSpeed;
        }

        if (_SwingTime >= _AttackDuration + _AttackCooldown-0.05f)
        {
            _Flip = !_Flip;
            _SwingTime = 0;
        }
    }

    void SideMovement()
    {

    }
}
