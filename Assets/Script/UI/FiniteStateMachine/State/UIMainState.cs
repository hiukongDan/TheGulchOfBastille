﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMainState : UIState
{
    private enum Selection{
        NewGame, Load, Options, Credit, Exit,
    };
    private ButtonGroup buttonGroup;
    public UIMainState(UIHandler uiHandler, GameObject parentNode, ButtonGroup buttonGroup):base(uiHandler, parentNode)
    {
        this.buttonGroup = buttonGroup;
    }

    public override void Enter()
    {
        base.Enter();

        //AudioSource audioSource = parentNode.transform.parent.Find("MainMenu BGM").GetComponent<AudioSource>();
        // if(!audioSource.isPlaying){
        //     audioSource.Play();
        // }

        AkSoundEngine.PostEvent("Play_Music", uiHandler.GM.gameObject);

        if(!uiHandler.GM.gameSaver.HasValidSaving()){
            buttonGroup.DisableButton((int)Selection.Load);
            // Debug.Log("disable");
        }
        else{
            buttonGroup.EnableButton((int)Selection.Load);
        }

        // play bgm
    }

    public override void Exit()
    {
        base.Exit();

        // stop bgm
    }

    public override void OnInteraction()
    {
        UIStateEventData data;
        data.index = buttonGroup.GetIndexOfCurrentSelected();
        data.widgetType = UIState.WidgetType.Button;
        OnClick(data);
    }

    public override void OnClick(UIStateEventData eventData)
    {
        switch((Selection)eventData.index){
            case Selection.NewGame:
                uiHandler.uiFSM.PushState(uiHandler.uiSaveState);
            break;
            case Selection.Load:
                uiHandler.uiFSM.PushState(uiHandler.uiLoadState);
            break;
            case Selection.Options:
            break;
            case Selection.Credit:
            break;
            case Selection.Exit:
                uiHandler.GM.QuitGame();
            break;
            default:
            break;
        }
    }

}
