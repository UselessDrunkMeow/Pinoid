using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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

    [Header("WanderingSettings")]
    [SerializeField] float _MinRadius;
    [SerializeField] float _MaxRadius;
    [SerializeField] float _WaitTime;
    [SerializeField] float _MaxTotalTime;
    Vector3 _WanderPoint;

    [Header("Timers")]
    [SerializeField] float WanderingWaitTimer;
    [SerializeField] float WanderingTimer;


    private void OnEnable()
    {
        _Player = FindObjectOfType<PlayerControler>();
    }
    void Update()
    {
        _Agent.SetDestination(_target.position);
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
            case EnemyState.Fleeing:
                Fleeing();
                break;
            case EnemyState.Attacking:
                Attacking();
                break;
        }
    }
    void Idle()
    {
        _Agent.SetDestination(_Agent.transform.position);
    }
    void Wandering()
    {
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
        else if(WanderingTimer > _MaxTotalTime)
        {
            _target.transform.position = transform.position;
            WanderingTimer = 0;
        }
    }
    void Chasing()
    {
        _target.position = _Player.transform.position;
    }
    void Fleeing()
    {

    }
    void Attacking()
    {

    }

    void RandomPointGenerator()
    {
        Vector2 i = Random.insideUnitSphere * Random.Range(_MinRadius, _MaxRadius) + transform.position;
        _WanderPoint = new Vector3(i.x, 0, i.y);
    }

}
