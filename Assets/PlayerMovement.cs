using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float _Speed;
    [SerializeField] Rigidbody _RB;

    Vector3 _Movement;

    void Update()
    {
        _Movement.x = Input.GetAxisRaw("Horizontal");
        _Movement.z = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            _RB.MovePosition(_RB.position + _Movement * Time.fixedDeltaTime * _Speed*5);
        }
    }

    private void FixedUpdate()
    {
        _RB.MovePosition(_RB.position + _Movement * Time.fixedDeltaTime* _Speed);
    }
}
