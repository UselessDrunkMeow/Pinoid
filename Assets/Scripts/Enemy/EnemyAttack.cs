using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AttackVariants
{
    Melee,
    StationaryMelee,
    Ranged,
    StationaryRanged,
    Other,
}

public class EnemyAttack : MonoBehaviour
{
    public float _Damage;
    public float _Range;
    public float _MoveSpeed;

    public float _CooldownTime;
    public float _WindupTime;
    public float _AttackDuration;

    [SerializeField] float _CooldownTimer;
    [SerializeField] float _WindupTimer;
    [SerializeField] float _AttackDurationTimer;  

    [SerializeField] Transform _AttackBox;
    AttackVariants _AttackVariants;
    bool _IsAttacking;
    [SerializeField] EnemyBrain _Brain;

    [SerializeField] SpriteRenderer _Sprite;

    // Start is called before the first frame update
    void Start()
    {
        _Brain = GetComponent<EnemyBrain>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_IsAttacking)
        {
            switch (_AttackVariants)
            {
                case AttackVariants.Melee:
                    Melee();
                    break;
                case AttackVariants.StationaryMelee:
                    StationaryMelee();
                    break;
                case AttackVariants.Ranged:
                    Ranged();
                    break;
                case AttackVariants.StationaryRanged:
                    StationaryRanged();
                    break;
                case AttackVariants.Other:
                    break;
                default:
                    break;
            }
        }
        else
        {
            switch (_Brain._LookDirection)
            {
                case EnemyLookDirection.Up:
                    _AttackBox.rotation = new Quaternion(0, 180, 0, 0); 
                    break;
                case EnemyLookDirection.Down:
                    _AttackBox.rotation = new Quaternion(0, 0, 0, 0);
                    break;
                case EnemyLookDirection.Left:
                    _AttackBox.rotation = new Quaternion(0, -90, 0, 0);
                    break;
                case EnemyLookDirection.Right:
                    _AttackBox.rotation = new Quaternion(0, 90, 0, 0);
                    break;
                default:
                    break;
            }
            _Sprite.color = Color.white;
        }
    }

    void Melee()
    {

    }

    void StationaryMelee()
    {
        _Sprite.color = Color.green;
        _Brain._Agent.speed = _MoveSpeed;
        _WindupTimer = +Time.deltaTime;
        if (_WindupTimer > _WindupTime)
        {
            _Sprite.color = Color.red;
            _WindupTimer = 0;
            if (_AttackDurationTimer > _AttackDuration)
            {

            }
        }

    }

    void Ranged()
    {
        _Brain._Agent.speed = _MoveSpeed;
    }

    void StationaryRanged()
    {
        _Brain._Agent.speed = _MoveSpeed;
    }

    public void StartAttack()
    {
        _IsAttacking = true;
    }
}
