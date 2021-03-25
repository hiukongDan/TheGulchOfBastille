using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public UIHandler uiHandler{get; private set;}

    public PlayerCinemaMovement playerCinemaMovement { get; private set; }
    public bool IsDebug = true;

    public float demonSceneCodeInterval = 10f;
    public float DarkWaitDuration = 1f;

    public SceneCode[] demonScenes = {SceneCode.Gulch_Main, SceneCode.Gulch_SunTower, SceneCode.Gulch_Goye};

    [Tooltip("Should also place Default Start Point to corresponding position")]
    public SceneCode DefaultStartScene = SceneCode.Gulch_SunTower;
    public Transform DefaultStartPoint;
    public List<LittleSunData> LittleSuns;

#region REFERNECES
    public Player player{get; private set;}
#endregion

#region INTERNAL VARIABLES
    private string gameScene;
    public SceneCode currentSceneCode{get; private set;}
    public GameSaver gameSaver{get; private set;}
    public float elapsedSeconds{get; private set;}
    private Coroutine demonCoroutine;
#endregion

    public void ReloadScene()
    {
        // player not finding any little suns
        if(player.playerRuntimeData.lastLittleSunID < 0){
            StartCoroutine(ReloadScene(DefaultStartScene, DefaultStartPoint.position));
        }
        else{
            LittleSunData data = LittleSuns[player.playerRuntimeData.lastLittleSunID];
            StartCoroutine(ReloadScene(data.sceneCode, data.TeleportPoint.position));
        }
    }

    private IEnumerator ReloadScene(SceneCode sceneCode, Vector3 position){
        yield return new WaitForSeconds(uiHandler.uiEffectHandler.OnPlayUIEffect(UIEffect.Transition_CrossFade, UIEffectAnimationClip.start));

        player.ResetPlayerStatus();
        LoadSceneCode(sceneCode);
        player.SetPosition(position);

        yield return new WaitForEndOfFrame();
        Camera.main.GetComponent<BasicFollower>().ClampCamera(player.transform.position);
        Camera.main.GetComponent<BasicFollower>().UpdateCameraFollowing(player.transform);

        yield return new WaitForSeconds(DarkWaitDuration);
        player.stateMachine.SwitchState(player.wakeupState);
        
        yield return new WaitForSeconds(uiHandler.uiEffectHandler.OnPlayUIEffect(UIEffect.Transition_CrossFade, UIEffectAnimationClip.end));
    }

    void OnEnable(){
        uiHandler = GetComponent<UIHandler>();
        playerCinemaMovement = GetComponent<PlayerCinemaMovement>();

        player = GameObject.Find("/Player").transform.Find("Player").GetComponent<Player>();
        
        gameScene = SceneManager.GetActiveScene().name;

        gameSaver = GetComponent<GameSaver>();

        currentSceneCode = SceneCode.Gulch_Main;

        demonCoroutine = StartCoroutine(DemonRandomSceneCode());
        elapsedSeconds = 0.0f;
        // DemonSceneCode(demonScenes[Mathf.FloorToInt(Random.Range(0, demonScenes.Length))]);
    }

    public void StartGame(){
        StopCoroutine(demonCoroutine);
        StartCoroutine(StartGame(gameSaver.isNewGame));
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
        // gameSaver.SaveAll();
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ExitGame(){
        StartCoroutine(exitGame());
    }

    private IEnumerator exitGame(){
        gameSaver.SaveAll();
        Time.timeScale = 1f;
        yield return new WaitForSeconds(uiHandler.uiEffectHandler.OnPlayUIEffect(UIEffect.Transition_CrossFade, UIEffectAnimationClip.start));
        demonCoroutine = StartCoroutine(DemonRandomSceneCode());
        uiHandler.uiFSM.InitStateMachine(uiHandler.uiMainState);
        player.gameObject.SetActive(false);
        yield return new WaitForSeconds(DarkWaitDuration);
        yield return new WaitForSeconds(uiHandler.uiEffectHandler.OnPlayUIEffect(UIEffect.Transition_CrossFade, UIEffectAnimationClip.end));
    }

    public bool CanPlayerAction()
    {
        if (uiHandler != null)
        {
            return uiHandler.uiFSM.PeekState() == uiHandler.uiPlayState;
        }
        return true;
    }
    IEnumerator StartGame(bool isNewGame){
        yield return new WaitForSeconds(uiHandler.uiEffectHandler.OnPlayUIEffect(UIEffect.Transition_CrossFade, UIEffectAnimationClip.start));
        ResetAllSceneCode();

        // uiHandler.uiEffectHandler.OnPlayUIEffect(UIEffect.Transition_CrossFade, UIEffectAnimationClip.dark);
        if(isNewGame){
            player.gameObject.SetActive(true);
            if(DefaultStartPoint){
                player.SetPosition(DefaultStartPoint.position);
            }
            currentSceneCode = DefaultStartScene;
        }
        else{
            player.gameObject.SetActive(true);
            gameSaver.LoadAll();
            currentSceneCode = player.playerRuntimeData.currentSceneCode;
            elapsedSeconds = gameSaver.GetSaveSlotMeta(gameSaver.currentSaveSlot).elapsedSeconds;
        }

        LoadSceneCode();
        yield return new WaitForEndOfFrame();
        Camera.main.GetComponent<BasicFollower>().ClampCamera(player.transform.position);
        Camera.main.GetComponent<BasicFollower>().UpdateCameraFollowing(player.transform);
        
        uiHandler.StartGame();

        player.InputHandler.ResetAll();
        yield return new WaitForSeconds(uiHandler.uiEffectHandler.OnPlayUIEffect(UIEffect.Transition_CrossFade, UIEffectAnimationClip.end));
    }

    IEnumerator DemonRandomSceneCode(){
        int index = Random.Range(0, demonScenes.Length);
        while(true){
            yield return new WaitForSeconds(uiHandler.uiEffectHandler.OnPlayUIEffect(UIEffect.Transition_CrossFade, UIEffectAnimationClip.start));
            index = (index + 1) % demonScenes.Length;
            DemonSceneCode(demonScenes[index]);
            yield return new WaitForSeconds(uiHandler.uiEffectHandler.OnPlayUIEffect(UIEffect.Transition_CrossFade, UIEffectAnimationClip.end));
            yield return new WaitForSeconds(demonSceneCodeInterval);
        }
    }

    private void DemonSceneCode(SceneCode sceneCode){
        ResetAllSceneCode();
        Transform centerPoint = GameObject.Find("/Scenes").transform.Find(sceneCode.ToString()).Find("CenterPoint");
        if(centerPoint == null){
            centerPoint = new GameObject("CenterPoint").transform;
            centerPoint.position = Vector3.zero;
            centerPoint.transform.parent = GameObject.Find("/Scenes").transform.Find(sceneCode.ToString());
        }

        EnterSceneCode(sceneCode);
        //Debug.Log(demonScenes[index].ToString());
        Camera.main.transform.position = new Vector3(centerPoint.position.x, centerPoint.position.y, Camera.main.transform.position.z);
        Camera.main.GetComponent<BasicFollower>().UpdateCameraFollowing(centerPoint);
    }

    public void ResetAllSceneCode(){
        GameObject sceneGO = GameObject.Find("/Scenes");
        foreach(SceneCodeUtil util in sceneGO.GetComponentsInChildren<SceneCodeUtil>()){
            util.gameObject.SetActive(false);
        }
    }

    void Update(){
        if(CanPlayerAction()){
            elapsedSeconds += Time.deltaTime;
        }
    }
}
