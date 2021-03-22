using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UIState
{
    protected GameObject parentNode;
    protected UIHandler uiHandler;

    public UIState(UIHandler uiHandler, GameObject parentNode)
    {
        this.uiHandler = uiHandler;
        this.parentNode = parentNode;
    }

    public virtual void Enter()
    {
        parentNode.SetActive(true);
    }

    public virtual void Exit()
    {
        parentNode.SetActive(false);
    }

    public virtual void Update()
    {

    }

    public virtual void OnInteraction(){
        
    }

    public virtual void onMenuNext(){

    }

    public virtual void OnMenuPrev(){

    }

    public virtual void OnClick(UIStateEventData eventData){

    }

    public virtual void ShowView(){
        
    }

    public virtual void OnDirectionMove(Direction direction){
        
    }

    public enum WidgetType{
        Tab, Button, View,
    };

    public enum Direction{
        LEFT, UP, RIGHT, DOWN,
    };

    public struct UIStateEventData{
        public int index;
        public WidgetType widgetType;
    }
}
