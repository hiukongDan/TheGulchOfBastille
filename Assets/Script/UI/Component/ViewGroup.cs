using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewGroup : MonoBehaviour
{
    public List<UIView> views;

    public GameManager gameManager;

    public int Row;
    public int Column;

    void Awake(){
        gameManager = FindObjectOfType<GameManager>();
    }
    void OnEnable() {
        if(views == null){
            views = new List<UIView>();
        }
        else{
            views.Clear();
        }

        foreach(UIView view in transform.GetComponentsInChildren<UIView>()){
            views.Add(view);
        }
    }
    public void OnClick(UIView view){
        int index = GetIndexOfView(view);

        UIState.UIStateEventData data = new UIState.UIStateEventData();
        data.index = GetIndexOfView(view);
        data.widgetType = UIState.WidgetType.View;
        gameManager.uiHandler.uiFSM.PeekState()?.OnClick(data);
    }

    public void ClearViewGroup(){
        foreach(UIView view in views){
            view.ClearView();
        }
    }

    public void ClearChosen(){
        foreach(UIView view in views){
            view.ClearChosen();
        }
    }
    
    int GetIndexOfView(UIView view) => views.IndexOf(view);
}
