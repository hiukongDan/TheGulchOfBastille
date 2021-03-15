using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public UIHandler uiHandler{get; private set;}

    public PlayerCinemaMovement playerCinemaMovement { get; private set; }
    public bool IsDebug = true;

#region REFERNECES
    private Player player;
#endregion

#region INTERNAL VARIABLES
    private string gameScene;
    private SceneCode currentSceneCode = SceneCode.Gulch_Main;
#endregion

    public void ReloadGame()
    {
        SceneManager.LoadScene(gameScene);
    }

    void Awake(){
        uiHandler = GetComponent<UIHandler>();
        playerCinemaMovement = GetComponent<PlayerCinemaMovement>();

        player = GameObject.Find("/Player").transform.Find("Player").GetComponent<Player>();
        
        gameScene = SceneManager.GetActiveScene().name;
    }

    void Start(){

    }

    void StartGame(){
        LoadSceneCode(player.playerRuntimeData.currentSceneCode);
        currentSceneCode = player.playerRuntimeData.currentSceneCode;

        playerCinemaMovement.StartGameScene();
    }

    public void LoadSceneCode(SceneCode sceneCode){
        // TODO:
        // player last position
        // Disable
        GameObject oldSceneGO = GameObject.Find("/Scenes").transform.Find(currentSceneCode.ToString()).gameObject;
        oldSceneGO?.SetActive(false);

        // Enable
        GameObject currentSceneGO = GameObject.Find("/Scenes").transform.Find(sceneCode.ToString()).gameObject;
        currentSceneGO?.SetActive(true);
        currentSceneCode = sceneCode;
        Camera.main.GetComponent<BasicFollower>().cameraClamp = currentSceneGO.GetComponent<SceneCodeUtil>().CameraClamp;

        player.playerRuntimeData.currentSceneCode = sceneCode;
    }


    public void QuitGame()
    {
        Application.Quit();
    }

    public bool CanPlayerAction()
    {
        if (uiHandler != null)
        {
            return uiHandler.uiFSM.PeekState() == uiHandler.uiPlayState;
        }

        return true;
    }

}
