using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    PlayerControler _Player;
    public enum GameState
    {
        Playing,
        Paused,
        Dead,
        End,
        Crashed
    }

    private static GameManager _instance;

    public static GameManager Instance { get { return _instance; } }

    public GameState _GameState;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    void Start()
    {
        _Player = FindObjectOfType<PlayerControler>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_GameState == GameState.Paused)
            {
                Time.timeScale = 1;
                _GameState = GameState.Playing;
            }
            else
            {
                Time.timeScale = 0;
                _GameState = GameState.Paused;
            }
        }
        switch (_GameState)
        {
            case GameState.Playing:
                break;
            case GameState.Paused:
                break;
            case GameState.Dead:                
                break;
            case GameState.Crashed:
                break;
            default:
                break;
        }
        if(_Player == null)
        {
            _GameState = GameState.Dead;
        }
    }
}
