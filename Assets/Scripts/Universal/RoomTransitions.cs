using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTransitions : MonoBehaviour
{
    public RoomTransitions _AttachedDoor;
    PlayerControler _Player;
    public Transform _PlayerTeleportPoint;
    public LayerMask layerMask;
    void Start()
    {
        _Player = FindObjectOfType<PlayerControler>(); 
        StartCoroutine(WaitForAllRoomsToExsist());
    }
    
    void Update()
    {
        
    }

    IEnumerator WaitForAllRoomsToExsist()
    {
        yield return new WaitForSeconds(0.5f);
        CheckAttatchedDoor();
    }

    void CheckAttatchedDoor()
    {
        RaycastHit hit;
        Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 30, layerMask);
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.magenta);
        _AttachedDoor = hit.transform.gameObject.GetComponent<RoomTransitions>();
        print(hit.transform.position);                
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Player"))
        {            
            _Player.transform.position = new Vector3(_AttachedDoor._PlayerTeleportPoint.position.x, _Player.transform.position.y, _AttachedDoor._PlayerTeleportPoint.position.z);
        }
    }
}
