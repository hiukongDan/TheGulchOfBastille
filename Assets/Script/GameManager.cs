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
    public SceneCode currentSceneCode{get; private set;}
    public GameSaver gameSaver{get; private set;}
    public float elapsedMinutes = 0f;
    private float elapsedSeconds = 0f;
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

        gameSaver = GetComponent<GameSaver>();

        currentSceneCode = SceneCode.Gulch_Main;
    }

    public void StartGame(){
        GameObject sceneGO = GameObject.Find("/Scenes");
        for(int i = 0; i < sceneGO.transform.childCount; ++i){
            sceneGO.transform.GetChild(i).gameObject?.SetActive(false);
        }

        if(gameSaver.isNewGame){
            LoadSceneCode();
            player.gameObject.SetActive(true);
        }
        else{
            player.gameObject.SetActive(true);
            gameSaver.Load();
            currentSceneCode = player.playerRuntimeData.currentSceneCode;
            elapsedMinutes = gameSaver.GetSaveSlotMeta(gameSaver.currentSaveSlot).elapsedMinutes;
            LoadSceneCode();
            // Debug.Log("load");
        }
        
        uiHandler.StartGame();
        playerCinemaMovement.StartGameScene();

        player.InputHandler.ResetAll();
    }

    public void LoadSceneCode(){
        LoadSceneCode(currentSceneCode);
    }

    public void ExitSceneCode(SceneCode sceneCode){
        GameObject sceneGO = GameObject.Find("/Scenes").transform.Find(sceneCode.ToString()).gameObject;
        sceneGO?.SetActive(false);
    }

    public void EnterSceneCode(SceneCode sceneCode){
        GameObject sceneGO = GameObject.Find("/Scenes").transform.Find(sceneCode.ToString()).gameObject;
        sceneGO?.SetActive(true);

        Camera.main.GetComponent<BasicFollower>().cameraClamp = sceneGO.GetComponent<SceneCodeUtil>().CameraClamp;
    }

    public void LoadSceneCode(SceneCode sceneCode){
        ExitSceneCode(currentSceneCode);
        EnterSceneCode(sceneCode);
        currentSceneCode = sceneCode;
        
        player.playerRuntimeData.currentSceneCode = sceneCode;
    }

    public void OnApplicationQuit(){
        gameSaver.SaveAll();
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ExitGame(){
        gameSaver.SaveAll();
        player.gameObject.SetActive(false);

        ExitSceneCode(currentSceneCode);

        uiHandler.uiFSM.InitStateMachine(uiHandler.uiMainState);
    }

    public bool CanPlayerAction()
    {
        if (uiHandler != null)
        {
            return uiHandler.uiFSM.PeekState() == uiHandler.uiPlayState;
        }

        return true;
    }

    void Update(){
        if(CanPlayerAction()){
            elapsedSeconds += Time.deltaTime;
            if(elapsedSeconds >= 60f){
                elapsedSeconds = 0f;
                elapsedMinutes += 1;
            }
        }
    }
}
