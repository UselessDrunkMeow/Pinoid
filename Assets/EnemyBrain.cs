using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBrain : MonoBehaviour
{
    [SerializeField] NavMeshAgent _Agent;
    [SerializeField] Transform _target;
    
    void Update()
    {
        _Agent.SetDestination(_target.position);
    }
}
