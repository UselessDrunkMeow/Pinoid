using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class Animation : MonoBehaviour
{
    EnemyBrain _Enemy;
    Animator _Animator;
    SpriteRenderer _Sprite;
    void Start()
    {
        _Enemy = GetComponent<EnemyBrain>();
        _Animator = GetComponentInChildren<Animator>();
        _Sprite = GetComponentInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (_Enemy._LookDirection)
        {
            case EnemyLookDirection.Up://for up set down to true, otherway for down. dont ask. idk.
                _Animator.SetBool("Up", false);
                _Animator.SetBool("Down", true);
                _Animator.SetBool("Right", false);             
                _Sprite.transform.localEulerAngles = new Vector3(0, 0, 0);
                break;
            case EnemyLookDirection.Down:
                _Animator.SetBool("Up", true);
                _Animator.SetBool("Down", false);
                _Animator.SetBool("Right", false);               
                _Sprite.transform.localEulerAngles = new Vector3(0, 0, 0);
                break;
            case EnemyLookDirection.Left:
                _Animator.SetBool("Up", false);
                _Animator.SetBool("Down", false);
                _Animator.SetBool("Right", true);          
                _Sprite.transform.localEulerAngles = new Vector3(0, 0, 0);
                break;
            case EnemyLookDirection.Right:
                _Animator.SetBool("Up", false);
                _Animator.SetBool("Down", false);
                _Animator.SetBool("Right", true);          
                _Sprite.transform.localEulerAngles = new Vector3(0, 180, 0);
                break;
            default:
                break;
        }
        switch (_Enemy._EnemyState)
        {
            case EnemyState.Idle:                
                _Animator.SetBool("Idle", true);
                _Animator.SetBool("Attack", false);
                break;
            case EnemyState.Wandering:
                _Animator.SetBool("Idle", false);
                _Animator.SetBool("Attack", false);
                break;
            case EnemyState.Chasing:
                _Animator.SetBool("Idle", false);
                _Animator.SetBool("Attack", false);
                break;
            case EnemyState.Fleeing:
                _Animator.SetBool("Idle", false);
                _Animator.SetBool("Attack", false);
                break;
            case EnemyState.Attacking:
                _Animator.SetBool("Idle", false);
                _Animator.SetBool("Attack", true);
                break;
            default:
                break;
        }
    }   
}
