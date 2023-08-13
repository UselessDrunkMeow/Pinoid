using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackDamage : MonoBehaviour
{
    public int _Damage;

    private void OnCollisionEnter(Collision collision)
    {
        print("Hit!");
        if (collision.transform.CompareTag("Enemy"))
        {
            print("Hit Enemy!");
            collision.collider.GetComponent<Health>().DecreaseHealth(_Damage);
        }
    }
}
