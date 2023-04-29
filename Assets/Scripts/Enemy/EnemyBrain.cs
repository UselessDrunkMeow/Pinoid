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

public class EnemyBrain : MonoBehaviour
{
    [SerializeField] NavMeshAgent _Agent;
    [SerializeField] Transform _target;

    [Header("GeneralAgentSettings")]
    [SerializeField] float _MinRange;
    [SerializeField] EnemyState _EnemyState;
    PlayerControler _Player;

    [Header("AgentMoveSpeeds")]
    [SerializeField] float _WanderSpeed;
    [SerializeField] float _ChaseSpeed;
    [SerializeField] float _AttackSpeed;

    [Header("WanderingSettings")]
    [SerializeField] float _MinRadius;
    [SerializeField] float _MaxRadius;
    [SerializeField] float _WaitTime;
    [SerializeField] float _MaxTotalTime;
    Vector3 _WanderPoint;

    [Header("Attacking conditions")]
    [SerializeField] float _MaxDistance;

    [Header("Timers")]
    [SerializeField] float WanderingWaitTimer;
    [SerializeField] float WanderingTimer;

    [Header("Events")]
    UnityEvent _Attack;

    private void OnEnable()
    {
        _Player = FindObjectOfType<PlayerControler>();
    }
    void Update()
    {
        _Agent.SetDestination(_target.position);
        States();
        if (Vector3.Distance(transform.position, _Player.transform.position) < _MaxDistance)
        {
            _EnemyState = EnemyState.Attacking;
        }
        else if(_EnemyState == EnemyState.Attacking && Vector3.Distance(transform.position, _Player.transform.position) > _MaxDistance)
        {
            _EnemyState = EnemyState.Chasing;
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
        if (Vector3.Distance(_Agent.transform.position, _target.transform.position) < _MinRange)
        {
            print("wawa");
            WanderingWaitTimer += Time.deltaTime;
            if (WanderingWaitTimer > _WaitTime)
            {
                RandomPointGenerator();
                _target.transform.position = _WanderPoint;
                WanderingWaitTimer = 0;
                WanderingTimer = 0;
            }
        }
        else if (WanderingTimer > _MaxTotalTime)
        {
            _target.transform.position = transform.position;
            WanderingTimer = 0;
        }
    }
    void Chasing()
    {
        _Agent.speed = _ChaseSpeed;
        _target.position = _Player.transform.position;
    }
    void Attacking()
    {
        _Agent.speed = _AttackSpeed;
        print("ATTACK!");
    }
    void Fleeing()
    {
        _Attack.Invoke();
    }

    void RandomPointGenerator()
    {
        Vector2 i = Random.insideUnitSphere * Random.Range(_MinRadius, _MaxRadius) + transform.position;
        _WanderPoint = new Vector3(i.x, 0, i.y);
    }

}
