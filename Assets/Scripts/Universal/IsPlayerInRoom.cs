using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsPlayerInRoom : MonoBehaviour
{
    public Transform _CameraPoint;
    public Camera _Cam;
    bool _IsInRoom;
    private void Start()
    {
        _Cam = FindObjectOfType<Camera>();
    }

    private void Update()
    {
        if (_IsInRoom)
        {
            _Cam.transform.position = _CameraPoint.position;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            _IsInRoom = true;
        }
    }

    //private void OnTriggerStay(Collider other)
    //{
    //    if (other.transform.CompareTag("Player"))
    //    {
    //        print("Sill in room");
    //    }
    //}

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            _IsInRoom = false;
        }
    }

}
