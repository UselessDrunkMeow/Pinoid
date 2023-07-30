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
    [Header("AttackSettings")]
    public int _Damage;
    public float _Range;
    public float _MoveSpeed;
    public float _KnockBack;

    [Header("Timers")]
    public float _CooldownTime;
    public float _WindupTime;
    public float _AttackDuration;
    public float _KnockBackDuration;

    float _CooldownTimer;
    float _WindupTimer;
    float _AttackDurationTimer;

    [Header("General Data")]
    [SerializeField] Transform _AttackBox;
    [SerializeField] AttackVariants _AttackVariants;
    public bool _IsAttacking;
    [SerializeField] EnemyBrain _Brain;

    [SerializeField] SpriteRenderer _Sprite;

    void Start()
    {
        _Brain = GetComponent<EnemyBrain>();
    }

    void Update()
    {
        if ( _IsAttacking || _AttackVariants == AttackVariants.Melee && _AttackVariants == AttackVariants.Ranged)
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
                    _AttackBox.localEulerAngles = new Vector3(0, 180, 0);
                    break;
                case EnemyLookDirection.Down:
                    _AttackBox.localEulerAngles = new Vector3(0, 0, 0);
                    break;
                case EnemyLookDirection.Left:
                    _AttackBox.localEulerAngles = new Vector3(0, 90, 0);
                    break;
                case EnemyLookDirection.Right:
                    _AttackBox.localEulerAngles = new Vector3(0, -90, 0);
                    break;
                default:
                    break;
            }
            _Sprite.color = Color.white;
        }
    }

    void Melee()
    {
        _WindupTimer += Time.deltaTime;
        _Sprite.color = Color.red;
        _Brain._Agent.speed = _MoveSpeed;
        if (_WindupTimer > _WindupTime)
        {
            _CooldownTimer += Time.deltaTime;
            _Sprite.color = Color.green;
            _AttackBox.gameObject.SetActive(true);
            if (_CooldownTimer > _CooldownTime)
            {
                _AttackBox.gameObject.SetActive(false);
                _IsAttacking = false;
                _Brain._EnemyState = EnemyState.Chasing;
                _WindupTimer = 0;
                _CooldownTimer = 0;
                _Sprite.color = Color.white;
            }
        }
    }

    void StationaryMelee()
    {
        _WindupTimer += Time.deltaTime;
        _Sprite.color = Color.red;
        _Brain._Agent.speed = 0;
        if (_WindupTimer > _WindupTime)
        {
            _CooldownTimer += Time.deltaTime;
            _Sprite.color = Color.green;
            _AttackBox.gameObject.SetActive(true);
            if (_CooldownTimer > _CooldownTime)
            {
                _AttackBox.gameObject.SetActive(false);
                _IsAttacking = false;
                _Brain._EnemyState = EnemyState.Chasing;
                _WindupTimer = 0;
                _CooldownTimer = 0;
                _Sprite.color = Color.white;
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
        _Brain._Agent.speed = _MoveSpeed;
    }

    IEnumerator KnockBack(Rigidbody prb)
    {
        Vector3 storedvelocity;//The velocity the player is moving at before the dash
        storedvelocity = prb.velocity;//Stores the velocity the player is moving at
        var direction = transform.position - prb.position;
        prb.velocity = -direction.normalized * _KnockBack;
        yield return new WaitForSeconds(_KnockBackDuration);
        prb.velocity = storedvelocity;//Returns the velocity the player is moving at before the dash          
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            var playerhp = other.GetComponent<Health>();
            var playerrb = other.GetComponent<Rigidbody>();
            StartCoroutine(KnockBack(playerrb));
            playerhp.DecreaseHealth(_Damage);

        }
    }

}
