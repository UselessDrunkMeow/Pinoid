using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour
{
    string _SceneName;
    private void Start()
    {
        _SceneName = SceneManager.GetActiveScene().name;
    }
    public void LoadScene(string _SceneName)
    {
        SceneManager.LoadScene(_SceneName);
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(_SceneName);
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Application.Quit!");
    }

}
