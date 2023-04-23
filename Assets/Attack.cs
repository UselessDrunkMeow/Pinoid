using System.Collections;
using UnityEngine;

public class Attack : MonoBehaviour
{
    PlayerControler _PlayerControler;
    Vector3 _AttackRotation;
    [Header("SwingSettings")]
    public float _AttackDuration;
    public float _AttackCooldown;

    public float _RotationAmount;
    public float _RotationSpeed;
    public float _NewRotation;

    public float _ForwardMoveAmount;
    public float _ForwardMoveSpeed;

    public float _SideMoveAmount;
    public float _SideMoveSpeed;

    float posX;
    float posZ;

    [SerializeField] bool attacking;

    [SerializeField] bool returnToStartPos;

    [SerializeField] Vector3 _NewPos;
    [SerializeField] Vector3 _OldPos;

    [SerializeField] public bool _IsAttacking;
    [SerializeField] Transform WeaponRotatePoint;
    [SerializeField] Transform WeaponHolder;


    void Start()
    {
        _IsAttacking = false;
        _PlayerControler = GetComponent<PlayerControler>();
        _OldPos = WeaponHolder.localPosition;
    }

    void Update()
    {
        _NewPos = new Vector3(0, 0, -_ForwardMoveAmount);

        AttackDirections();


        StartCoroutine(Swing());

        WeaponRotatePoint.eulerAngles = _AttackRotation;
    }

    void AttackDirections()
    {
        print("Can Change Attack Direction");
        switch (_PlayerControler._LookDirection)
        {
            case LookDirectionEnum.up:
                _AttackRotation = new Vector3(0, 180, 0);
                break;
            case LookDirectionEnum.down:
                _AttackRotation = new Vector3(0, 0, 0);
                break;
            case LookDirectionEnum.left:
                _AttackRotation = new Vector3(0, 90, 0);
                break;
            case LookDirectionEnum.right:
                _AttackRotation = new Vector3(0, 270, 0);
                break;
            case LookDirectionEnum.idle:
                _AttackRotation = new Vector3(0, 0, 0);
                break;

        }
    }


    private void OnDrawGizmos()//gizmos
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawRay(transform.position, _AttackRotation);
    }

    IEnumerator Swing()
    {
        yield return new WaitForSeconds(_AttackDuration / 2);
    }


}
