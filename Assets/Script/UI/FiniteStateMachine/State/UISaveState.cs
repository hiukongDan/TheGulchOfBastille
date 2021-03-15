using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISaveState : UIState
{
    private enum Selection{
        First, Second, Third,
    };
    public UISaveState(UIHandler uiHandler, GameObject parentNode):base(uiHandler, parentNode)
    {

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
        
    }

    public override void OnClick(UIStateEventData eventData){
        switch((Selection)eventData.index){
            case Selection.First:
            break;
            case Selection.Second:
            break;
            case Selection.Third:
            break;
            default:
            break;
        }
    }
}
