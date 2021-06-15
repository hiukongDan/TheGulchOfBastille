using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LittleSunHandler : MonoBehaviour
{
    public Animator InfoSignAnim{get; private set;}
    public Animator LittleSunAnim{get; private set;}
    public Animator BloodAnim{get; private set;}

    public LittleSunData littleSunData{get; private set;}
    public LittleSunMenu littleSunMenu{get; private set;}

    private bool isTeleported = false;

    void Awake()
    {
        littleSunData = GetComponent<LittleSunData>();
        littleSunMenu = GetComponentInChildren<LittleSunMenu>();
        InfoSignAnim = transform.Find("InfoSign Parent").GetComponentInChildren<Animator>();
        LittleSunAnim = transform.Find("Alive").GetComponent<Animator>();
        BloodAnim = transform.Find("Blood").GetComponent<Animator>();

        // Debug.Log(littleSunMenu.name);
    }

    void OnEnable() {
        InitLittleSunState();
    }

    private void InitLittleSunState()
    {
        if (!littleSunData.IsActive())
        {
            LittleSunAnim.Play(LittleSunAnimHash.BEFORE_ACTIVE_0);
        }
        else
        {
            LittleSunAnim.Play(LittleSunAnimHash.IDLE_0);
        }
    }

    public void OnLittleSunInteraction()
    {
        FindObjectOfType<Player>().playerRuntimeData.lastLittleSunID = littleSunData.LittleSunID;

        InfoSignAnim.Play(InfoSignAnimHash.OUTRO);

        if (!littleSunData.IsActive())
        {
            GameObject.Find("GameManager").GetComponent<GameManager>().playerCinemaMovement.LightLittleSun(this);
            littleSunData.OnLightLittleSun();
        }
        else
        {
            Player player = GameObject.Find("Player").GetComponent<Player>();
            //StartCoroutine(refreshScene(player));
            player.stateMachine.SwitchState(player.littleSunState);
            // transfer control authority to littleSunState
        }

        // save whenever interaction happends
        GameObject.Find("GameManager").GetComponent<GameManager>().gameSaver.SaveAll();
        UIEventListener.Instance.OnInfomationChange(new UIEventListener.InfomationChangeData("Game Saved"));
    }

    // protected IEnumerator refreshScene(Player player){
    //     // player.playerRuntimeData.lastLittleSunID = littleSunData.LittleSunID;
    //     // EnemySaveData.ResetRevivableEnemy();
    //     // player.stateMachine.SwitchState(player.cinemaState);
    //     // GameManager gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    //     // yield return new WaitForSeconds(gameManager.uiHandler.uiEffectHandler.OnPlayUIEffect(UIEffect.Transition_CrossFadeWhite, UIEffectAnimationClip.start));

    //     // gameManager.LoadSceneCode(player.playerRuntimeData.currentSceneCode);

    //     // yield return new WaitForSeconds(gameManager.uiHandler.uiEffectHandler.OnPlayUIEffect(UIEffect.Transition_CrossFadeWhite, UIEffectAnimationClip.end));
    //     // player.stateMachine.SwitchState(player.littleSunState);
    //     // yield return null;
    // }

    public void OnLittleSunTravel(){
        LittleSunData data = littleSunMenu.GetCurrentSelectedLittleSun();
        Player player = GameObject.Find("Player").GetComponent<Player>();
        player.playerRuntimeData.lastLittleSunID = data.LittleSunID;
        player.SetLittleSunHandler(null);
        littleSunMenu.Deactivate();
        GameObject.Find("GameManager").GetComponent<GameManager>().ReloadScene();
        EnemySaveData.ResetRevivableEnemy();
        isTeleported = true;
    }

    public void PlayerEnterTrigerArea(Player player, bool isEnter)
    {
        if (isEnter)
        {
            player?.SetLittleSunHandler(this);
            InfoSignAnim.Play(InfoSignAnimHash.INTRO);
            isTeleported = false;
        }
        else
        {
            player?.SetLittleSunHandler(null);
            if(!isTeleported && InfoSignAnim.GetCurrentAnimatorStateInfo(0).shortNameHash != InfoSignAnimHash.EMPTY)
            {
                InfoSignAnim.Play(InfoSignAnimHash.OUTRO);
            }
        }
    }

    public bool IsSunActive() => littleSunData.IsActive();
}
