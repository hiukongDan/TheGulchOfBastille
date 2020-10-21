using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
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

        if (PlayerPrefs.GetFloat("DecayAmount", -1) == -1)
        {
            SetPlayerDecay(0);
        }
    }

    public void SetPlayerDecay(int amount)
    {
        PlayerPrefs.SetFloat("DecayAmount", amount);
    }

    public float GetPlayerDecay() => PlayerPrefs.GetFloat("DecayAmount");

}
