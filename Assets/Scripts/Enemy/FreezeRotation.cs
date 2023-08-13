using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeRotation : MonoBehaviour
{
    void Update()
    {
        var rotation = Quaternion.LookRotation(Vector3.forward, Vector3.forward);
        transform.rotation = rotation;
    }
}
