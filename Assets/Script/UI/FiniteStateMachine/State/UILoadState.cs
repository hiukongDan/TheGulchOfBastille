using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILoadState : UIState
{
    private ButtonGroup buttonGroup;
    private List<Transform> positions;
    private List<Transform> emptys;
    private enum Selection{
        First, Second, Third,
    };
    public UILoadState(UIHandler uiHandler, GameObject parentNode, ButtonGroup buttonGroup): base(uiHandler, parentNode)
    {
        this.buttonGroup = buttonGroup;
        positions = new List<Transform>(3);
        emptys = new List<Transform>(3);
        for(int i = 0; i < 3; ++i){
            positions.Add(null);
            emptys.Add(null);
        }
    }

    public override void Enter()
    {
        base.Enter();
        for(int i = 0; i < (int)GameSaver.SaveSlot.SlotNum; ++i){
            GameSaver gameSaver = uiHandler.GM.gameSaver;
            PFontText pFontText = null;
            if(positions[i] == null){
                pFontText = buttonGroup.buttons[i].GetComponentInChildren<PFontText>();
                positions[i] = pFontText.transform;
            }
            else{
                positions[i].gameObject.SetActive(true);
                pFontText = buttonGroup.buttons[i].GetComponentInChildren<PFontText>();
            }

            if(emptys[i] == null){
                emptys[i] = positions[i].parent.Find("Empty");
                emptys[i].gameObject.SetActive(true);
            }
            else{
                emptys[i].gameObject.SetActive(true);
            }
            
            if(gameSaver.HasValidSaving((GameSaver.SaveSlot)i)){
                GameSaver.SaveSlotMeta meta = gameSaver.GetSaveSlotMeta((GameSaver.SaveSlot)i);
                string sceneCodeName = ((SceneCode)(meta.SceneCode)).ToString();
                string[] words = sceneCodeName.Split('_');
                string res = words[0] + " " + words[1];
                // string res = sceneCodeName.Split('_')[0];
                float hours = meta.elapsedMinutes / 60;
                float minutes = meta.elapsedMinutes % 60;
                res += " " + (int)hours + "H " + (int)minutes + "M";

                emptys[i].gameObject.SetActive(false);
                
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
        UIStateEventData data;
        data.index = buttonGroup.GetIndexOfCurrentSelected();
        OnClick(data);
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

        uiHandler.GM.gameSaver.isNewGame = false;
        uiHandler.GM.StartGame();
    }
}
