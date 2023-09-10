using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public enum TypeOfRoom
    {
        _NormalRoom,
        _ItemRoom,
        _BossRoom,
        _EndRoom,
    }
    public TypeOfRoom _RoomType;

    public Transform _CameraPoint;
    public Camera _Cam;
    public bool _IsInRoom;
    public List<Door> _Doors;
    public List<SpawnPoint> _SpawnPoint;
    public List<EnemyBrain> _Enemy;
    bool _Activate = true;
    bool Enemies = true;
    int EnemiesInRoom;

    float time = 1;
   public float timer;
    private void Start()
    {
        _Cam = FindObjectOfType<Camera>();
    }

    private void Update()
    {
        if (_IsInRoom)
        {
            _Cam.transform.position = _CameraPoint.position;
            if (_Activate)
            {
                SpawnEnemies();
                LockDoors();           
                _Activate = false;
            }
            if (_Enemy.Count == 0)
            {
                if (_RoomType == TypeOfRoom._EndRoom && timer > time)
                {
                    print("aaa");
                    GameManager.Instance._GameState = GameManager.GameState.End;
                }
                UnlockDoors();
            }
            else
            {
                LockDoors();
            }
            _Enemy.RemoveAll(s => s == null);
            timer = timer + Time.deltaTime;
        }


    }

    void LockDoors()
    {
        for (int i = 0; i < _Doors.Count; i++)
        {
            _Doors[i]._CanExit = false;
        }
    }
    void UnlockDoors()
    {
        for (int i = 0; i < _Doors.Count; i++)
        {
            _Doors[i]._CanExit = true;
        }
    }

    void SpawnEnemies()
    {
        for (int i = 0; i < _SpawnPoint.Count; i++)
        {
            _SpawnPoint[i].SpawnEnemy();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            _IsInRoom = true;
        }
        if (other.transform.CompareTag("Door"))
        {
            _Doors.Add(other.transform.GetComponent<Door>());
        }
        if (other.transform.CompareTag("SpawnPoint"))
        {
            _SpawnPoint.Add(other.transform.GetComponent<SpawnPoint>());
        }
        if (other.transform.CompareTag("Enemy"))
        {
            _Enemy.Add(other.transform.GetComponent<EnemyBrain>());
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            _IsInRoom = false;
        }
        if (other.transform.CompareTag("Enemy"))
        {
            _Enemy.Remove(other.transform.GetComponent<EnemyBrain>());
        }
    }
}
