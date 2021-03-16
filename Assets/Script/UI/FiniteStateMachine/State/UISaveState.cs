using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISaveState : UIState
{
    private ButtonGroup buttonGroup;
    private enum Selection{
        First, Second, Third,
    };
    public UISaveState(UIHandler uiHandler, GameObject parentNode, ButtonGroup buttonGroup):base(uiHandler, parentNode)
    {
        this.buttonGroup = buttonGroup;
    }

    public override void Enter(){
        parentNode.SetActive(true);
    }

    public override void Exit(){
        parentNode.SetActive(false);
    }

    public override void Update(){

    }

    public override void OnInteraction(){
        buttonGroup.OnClick();
    }

    public override void OnClick(UIStateEventData eventData){
        switch((Selection)eventData.index){
            case Selection.First:
            uiHandler.GM.gameSaver.currentSaveSlot = GameSaver.SaveSlot.First;
            break;
            case Selection.Second:
            uiHandler.GM.gameSaver.currentSaveSlot = GameSaver.SaveSlot.Second;
            break;
            case Selection.Third:
            uiHandler.GM.gameSaver.currentSaveSlot = GameSaver.SaveSlot.Third;
            break;
            default:
            break;
        }

        uiHandler.GM.gameSaver.isNewGame = true;
        uiHandler.GM.StartGame();
    }
}
