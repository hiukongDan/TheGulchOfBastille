using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISaveState : UIState
{
    private ButtonGroup buttonGroup;
    private List<Transform> positions;
    private List<Transform> emptys;
    protected float timer = -1f;
    protected float timerMax = 0.1f;
    private enum Selection{
        First, Second, Third,
    };
    public UISaveState(UIHandler uiHandler, GameObject parentNode, ButtonGroup buttonGroup):base(uiHandler, parentNode)
    {
        this.buttonGroup = buttonGroup;
        positions = new List<Transform>(3);
        emptys = new List<Transform>(3);
        for(int i = 0; i < 3; ++i){
            positions.Add(null);
            emptys.Add(null);
        }
    }

    public override void Enter(){
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
                // string sceneCodeName = ((SceneCode)(meta.SceneCode)).ToString();
                // string[] words = sceneCodeName.Split('_');
                // string res = words[0] + " " + words[1];
                // string res = sceneCodeName.Split('_')[0];
                string res = SceneCodeDisplayName.names[(int)meta.SceneCode];
                float hours = meta.elapsedSeconds / 3600;
                float minutes = meta.elapsedSeconds / 60 % 60;
                res += " " + (int)hours + "H " + (int)minutes + "M";

                emptys[i].gameObject.SetActive(false);
                
                pFontText.SetText(res);
            }
            else{
                pFontText.gameObject.SetActive(false);
            }
        }
    }

    public override void Exit(){
        base.Exit();
    }

    public override void Update(){
        base.Update();
        if(timer >= 0){
            timer -= Time.unscaledDeltaTime;
        }
    }

    public override void OnInteraction(){
        UIStateEventData data;
        data.index = buttonGroup.GetIndexOfCurrentSelected();
        data.widgetType = UIState.WidgetType.Button;
        OnClick(data);
    }

    public override void OnClick(UIStateEventData eventData){
        if(timer >= 0){
            return;
        }
        else{
            timer = timerMax;
        }

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
        
        if(uiHandler.uiFSM.PeekPrevState() == uiHandler.uiMainState){
            uiHandler.GM.gameSaver.isNewGame = true;
            uiHandler.GM.StartGame();
        }
        else{
            // uiHandler.GM.gameSaver.SaveAll();
            UIEventListener.Instance.OnInfomationChange(
                    new UIEventListener.InfomationChangeData("Game not save, use little sun to save you game")
            );
            uiHandler.uiFSM.PopState();
        }

    }
}
