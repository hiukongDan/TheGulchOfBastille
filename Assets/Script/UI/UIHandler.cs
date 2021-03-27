using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHandler : MonoBehaviour
{
    public enum UIMenu{
        Main, Play, Pause, Load, Save, Inventory, Equipment, 
    };

    public UIFiniteStateMachine uiFSM { get; private set; }
    public UIEffectHandler uiEffectHandler;
    public GameObject uiPlayGO;
    public GameObject uiPauseGO;
    public GameObject uiMainGO;
    public GameObject uiLoadGO;
    public GameObject uiSaveGO;
    public GameObject uiEquipmentGO;
    public GameObject uiInventoryGO;
    public UIPlayState uiPlayState { get; private set; }
    public UIPauseState uiPauseState { get; private set; }
    public UIMainState uiMainState {get; private set;}
    public UISaveState uiSaveState {get; private set;}
    public UILoadState uiLoadState {get; private set;}
    public UIEquipmentState uiEquipmentState{get; private set;}
    public UIInventoryState uiInventoryState{get; private set;}
    public GameManager GM{get; private set;}

    void Awake()
    {
        uiFSM = new UIFiniteStateMachine();
        uiPauseState = new UIPauseState(this, uiPauseGO, uiPauseGO.GetComponentInChildren<ButtonGroup>());
        uiPlayState = new UIPlayState(this, uiPlayGO);
        uiMainState = new UIMainState(this, uiMainGO, uiMainGO.GetComponentInChildren<ButtonGroup>());
        uiSaveState = new UISaveState(this, uiSaveGO, uiSaveGO.GetComponentInChildren<ButtonGroup>());
        uiLoadState = new UILoadState(this, uiLoadGO, uiLoadGO.GetComponentInChildren<ButtonGroup>());
        uiEquipmentState = new UIEquipmentState(this, uiEquipmentGO);
        uiInventoryState = new UIInventoryState(this, uiInventoryGO);

        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void OnEnable() {
        
    }

    void Start()
    {
        uiFSM.InitStateMachine(uiMainState);
    }

    public void StartGame(){
        uiFSM.InitStateMachine(uiPlayState);
    }

    void Update()
    {
        uiFSM.Update();
    }

    /// <summary>
    /// This is a callback function for input of pause action or return action
    /// </summary>
    public void OnInteraction(){
        uiFSM.PeekState().OnInteraction();
    }

    public void OnMenuPrev(){
        uiFSM.PeekState().OnMenuPrev();
    }

    public void OnMenuNext(){
        uiFSM.PeekState().onMenuNext();
    }

    /// <summary>
    /// This is a callback function for input of pause action or return action
    /// </summary>
    public void OnPause(){
        if(uiFSM.PeekState() == uiPlayState && isPausePrerequisition()){
            uiFSM.PushState(uiPauseState);
        }
        else if(uiFSM.Count() > 1){
            uiFSM.PopState();
        }
    }

    private bool isPausePrerequisition(){
        Player player = GameObject.Find("Player")?.GetComponent<Player>();   
        return player.stateMachine.currentState != player.converseState && player.stateMachine.currentState != player.littleSunState;
    }
}
