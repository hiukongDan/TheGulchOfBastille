using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private string currentScene;

    private UIHandler uiHandler;

    public void ReloadCurrentScene()
    {
        SceneManager.LoadScene(currentScene);
    }

    void Awake()
    {
        uiHandler = GetComponent<UIHandler>();
    }

    void Start()
    {
        EnterScene();
    }

    void OnDisable()
    {
        ExitScene();
    }

    public void SetPlayerDecay(int amount)
    {
        PlayerPrefs.SetFloat("DecayAmount", amount);
    }

    public float GetPlayerDecay() => PlayerPrefs.GetFloat("DecayAmount");

    public void PerformAreaTransmission(SceneCode sceneCode)
    {
        if(currentScene != sceneCode.ToString())
        {
            SceneManager.LoadScene(sceneCode.ToString());
        }
    }

    public void ExitScene()
    {
        // SceneManagement

        AreaTransmissionHandler.Instance.performAreaTransmissionHandler -= PerformAreaTransmission;
    }

    public void EnterScene()
    {
        currentScene = SceneManager.GetActiveScene().name;

        Application.targetFrameRate = 60;

        if (PlayerPrefs.GetFloat("DecayAmount", -1) == -1)
        {
            SetPlayerDecay(0);
        }

        AreaTransmissionHandler.Instance.performAreaTransmissionHandler += PerformAreaTransmission;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private bool _canPlayerAction;
    public bool CanPlayerAction()
    {
        if (uiHandler != null)
        {
            return uiHandler.uiFSM.PeekState() == uiHandler.uiPlayState;
        }

        return true;
    }
}
