using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private string currentScene;

    public void ReloadCurrentScene()
    {
        SceneManager.LoadScene(currentScene);
    }

    void Start()
    {
        currentScene = SceneManager.GetActiveScene().name;

        Application.targetFrameRate = 60;
    }
}
