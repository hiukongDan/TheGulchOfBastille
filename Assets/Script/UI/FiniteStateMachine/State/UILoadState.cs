using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILoadState : UIState
{
    private ButtonGroup buttonGroup;
    private enum Selection{
        First, Second, Third,
    };
    public UILoadState(UIHandler uiHandler, GameObject parentNode, ButtonGroup buttonGroup): base(uiHandler, parentNode)
    {
        this.buttonGroup = buttonGroup;
    }

    public override void Enter()
    {
        base.Enter();
        for(int i = 0; i < (int)GameSaver.SaveSlot.SlotNum; ++i){
            GameSaver gameSaver = uiHandler.GM.gameSaver;
            PFontText pFontText = buttonGroup.buttons[i].GetComponentInChildren<PFontText>();
            if(gameSaver.HasValidSaving((GameSaver.SaveSlot)i)){
                string sceneCodeName = ((SceneCode)(gameSaver.GetSaveSlotMeta((GameSaver.SaveSlot)i).SceneCode)).ToString();
                string res = string.Join(" ", sceneCodeName.Split('_'));
                res += " 1h 25m";
                // Debug.Log(res);
                pFontText.transform.parent.Find("Empty").gameObject.SetActive(false);
                pFontText.SetText(res);
            }
            else{
                pFontText.gameObject.SetActive(false);
            }
        }
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
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
    }
}
