using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIEquipmentState : UIState
{
    public UIEquipmentState(UIHandler uiHandler, GameObject parentNode): base(uiHandler, parentNode)
    {

    }

    public override void Enter()
    {
        base.Enter();

    }

    public override void Exit()
    {
        base.Exit();

    }

    public override void Update()
    {
        base.Update();
    }

    public override void OnInteraction()
    {
        // UIStateEventData data;
        // data.index = buttonGroup.GetIndexOfCurrentSelected();
        // OnClick(data);
    }

    public override void OnClick(UIStateEventData eventData){
        // switch((Selection)eventData.index){
        //     case Selection.Save:
        //         uiHandler.uiFSM.PushState(uiHandler.uiSaveState);
        //         break;
        //     case Selection.Equipment:
        //     break;
        //     case Selection.Inventory:
        //     break;
        //     case Selection.Options:
        //     break;
        //     case Selection.Exit:
        //         uiHandler.GM.ExitGame();
        //     break;
        //     default:
        //     break;
        // }

    }
}
