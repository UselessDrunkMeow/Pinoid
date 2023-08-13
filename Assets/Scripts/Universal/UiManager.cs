using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    public enum Screen
    {
        None,
        Pause,
        GameOver
    }

    public GameObject _PauseScreen;
    public GameObject _GameOverScreen;

    Screen _screen;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()   
    {
        if (GameManager.Instance._GameState == GameManager.GameState.Dead)
        {
            _screen = Screen.GameOver;
        }
        if (GameManager.Instance._GameState == GameManager.GameState.Paused)
        {
            _screen = Screen.Pause;
        }
        if (GameManager.Instance._GameState == GameManager.GameState.Playing)
        {
            _screen = Screen.None;
        }

        switch (_screen)
        {
            case Screen.None:
                _PauseScreen.SetActive(false);
                _GameOverScreen.SetActive(false);
                break;
            case Screen.Pause:
                _PauseScreen.SetActive(true);
                _GameOverScreen.SetActive(false);
                break;
            case Screen.GameOver:
                _PauseScreen.SetActive(false);
                _GameOverScreen.SetActive(true);
                break;
            default:
                break;
        }
    }
}
