using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LookDirectionEnum
{
    up,
    down,
    left,
    right
}

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] LookDirectionEnum _LookDirection;

    [SerializeField] float _Speed;
    [SerializeField] float _Dashspeed;
    [SerializeField] Rigidbody _RB;

    Vector3 _Movement;

    void Update()
    {
        _Movement.x = Input.GetAxisRaw("Horizontal");
        _Movement.z = Input.GetAxisRaw("Vertical");

        LookDirection();
        StartCoroutine(Dash());
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
        }
    }

     IEnumerator Dash()
    {
        Vector3 storedvelocity;
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            storedvelocity = _RB.velocity;
            _RB.velocity = _Movement.normalized * _Dashspeed;
            yield return new WaitForSeconds(0.15f);
            _RB.velocity = storedvelocity;
        }
        
        
    }

    void LookDirection()
    {
        if (_Movement.z > 0)
        {
            _LookDirection = LookDirectionEnum.up;
        }

        if (_Movement.z < 0)
        {
            _LookDirection = LookDirectionEnum.down;
        }

        if (_Movement.x < 0)
        {
            _LookDirection = LookDirectionEnum.left;
        }

        if (_Movement.x > 0)
        {
            _LookDirection = LookDirectionEnum.right;
        }
    }

    private void FixedUpdate()
    {
        _RB.MovePosition(_RB.position + _Movement.normalized * Time.fixedDeltaTime * _Speed);
    }
}
