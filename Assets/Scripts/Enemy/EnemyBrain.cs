using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public enum EnemyState
{
    Idle,
    Wandering,
    Chasing,
    Fleeing,
    Attacking,
}

public enum EnemyLookDirection
{
    Up,
    Down,
    Left,
    Right,
}

public class EnemyBrain : MonoBehaviour
{
    public NavMeshAgent _Agent;
    [SerializeField] Transform _Target;
    [SerializeField] Transform _LookPoint;

    [Header("GeneralAgentSettings")]
    [SerializeField] float _MinRange;
    public EnemyState _EnemyState;
    public EnemyLookDirection _LookDirection;
    PlayerControler _Player;
    EnemyAttack _EnemyAttack;

    [Header("AgentMoveSpeeds")]
    [SerializeField] float _WanderSpeed;
    [SerializeField] float _ChaseSpeed;

    [Header("WanderingSettings")]
    [SerializeField] float _MinRadius;
    [SerializeField] float _MaxRadius;
    [SerializeField] float _WaitTime;
    [SerializeField] float _MaxTotalTime;
    Vector3 _WanderPoint;

    [Header("Attacking conditions")]
    [SerializeField] float _MaxDistance;

    [Header("Timers")]
    float WanderingWaitTimer;
    float WanderingTimer;

    [Header("Events")]
    public UnityEvent _Attack;

    private void OnEnable()
    {
        _Player = FindObjectOfType<PlayerControler>();
        _EnemyAttack = GetComponent<EnemyAttack>();
    }
    void Update()
    {
        if (!_EnemyAttack._IsAttacking)
        {          
            _Agent.SetDestination(_Target.position);
            States();
            PlayerDistanceChecker();
            LineOfSight();
            LookDirection();
        }
    }

    void States()
    {
        switch (_EnemyState)
        {
            case EnemyState.Idle:
                Idle();
                break;
            case EnemyState.Wandering:
                Wandering();
                break;
            case EnemyState.Chasing:
                Chasing();
                break;
            case EnemyState.Attacking:
                Attacking();
                break;
            case EnemyState.Fleeing:
                Fleeing();
                break;
        }
    }
    void Idle()
    {
        _Agent.SetDestination(_Agent.transform.position);
    }
    void Wandering()
    {
        _Agent.speed = _WanderSpeed;
        WanderingTimer += Time.deltaTime;
        if (Vector3.Distance(_Agent.transform.position, _Target.transform.position) < _MinRange)
        {
            WanderingWaitTimer += Time.deltaTime;
            if (WanderingWaitTimer > _WaitTime)
            {
                RandomPointGenerator();
                _Target.transform.position = _WanderPoint;
                WanderingWaitTimer = 0;
                WanderingTimer = 0;
            }
        }
        else if (WanderingTimer > _MaxTotalTime)
        {
            _Target.transform.position = transform.position;
            WanderingTimer = 0;
        }
    }
    void Chasing()
    {
        _Agent.speed = _ChaseSpeed;
        _Target.position = _Player.transform.position;
    }
    void Attacking()
    {
        _Attack.Invoke();        
    }
    void Fleeing()
    {

    }

    void PlayerDistanceChecker()//checks the distance between the player and the enemy, and changes the enemystate accordingly
    {
        if (_EnemyState == EnemyState.Chasing && Vector3.Distance(transform.position, _Player.transform.position) < _MaxDistance)
        {
            _EnemyState = EnemyState.Attacking;
        }
        else if (_EnemyState == EnemyState.Attacking && Vector3.Distance(transform.position, _Player.transform.position) > _MaxDistance)
        {
            _EnemyState = EnemyState.Chasing;
        }
    }

    void LineOfSight()//right now it just checks the distance between the player and enemy no matter the line of sight, wil fix it later
    {
        if (_EnemyState == EnemyState.Wandering)
        {
            if (Vector3.Distance(transform.position, _Player.transform.position) < _MaxDistance * 5)
            {
                _EnemyState = EnemyState.Chasing;
            }
        }
    }

    void LookDirection()//checks where the enemy is looking
    {
        if ((transform.localEulerAngles.y >= 45 && transform.localEulerAngles.y <= 135))
        {
            _LookDirection = EnemyLookDirection.Right;
        }
        else if ((transform.localEulerAngles.y >= 135 && transform.localEulerAngles.y <= 255))
        {
            _LookDirection = EnemyLookDirection.Down;
        }
        else if ((transform.localEulerAngles.y >= 225 && transform.localEulerAngles.y <= 315))
        {
            _LookDirection = EnemyLookDirection.Left;
        }
        else
        {
            _LookDirection = EnemyLookDirection.Up;
        }
    }

    void RandomPointGenerator()//generates a random point in the wanted area
    {
        Vector2 i = Random.insideUnitSphere * Random.Range(_MinRadius, _MaxRadius) + transform.position;
        _WanderPoint = new Vector3(i.x, 0, i.y);
    }

}
