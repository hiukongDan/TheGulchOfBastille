﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITabGroup{
    void SelectTab(IUITab uiTab);
    void UnselectTab(IUITab uiTab);
    int SelectNext();
    int SelectPrevious();
    void ClearTab();
}

public class TabGroup : MonoBehaviour, ITabGroup
{
    public List<UITab> tabs;

    private GameManager gameManager;

    public UITab selectedTab;

    void Awake(){
        gameManager = FindObjectOfType<GameManager>();
    }

    void OnEnable() {
        if(tabs == null){
            tabs = new List<UITab>();
        }
        else{
            tabs.Clear();
        }

        foreach(UITab uiTab in transform.GetComponentsInChildren<UITab>()){
            tabs.Add(uiTab);
        }
    }

    void OnDisable() {
        foreach(UITab tab in tabs){
            tab.gameObject.SetActive(true);
        }    
    }

    public void OnClick(){
        var data = new UIState.UIStateEventData();
        data.index = GetIndexOfTab(selectedTab);
        data.widgetType = UIState.WidgetType.Tab;
        gameManager.uiHandler.uiFSM.PeekState()?.OnClick(data);
    }

    public int GetIndexOfTab(UITab uiTab) => tabs.IndexOf(uiTab);

    public void SelectTab(IUITab uiTab){
        if(uiTab == null){
            return;
        }

        try{
            UITab tab = (UITab)uiTab;
            if(selectedTab != tab){
                ClearTab();
                tab.gameObject.SetActive(false);
                selectedTab = tab;
            }
        }
        catch(UnityException ex){
            Debug.Log(ex.StackTrace);
        }
    }

    public void UnselectTab(IUITab uiTab){
        try{
            UITab tab = (UITab)uiTab;
            tab.gameObject.SetActive(true);
        }
        catch(UnityException ex){
            Debug.Log(ex.StackTrace);
        }
    }

    /// <Returns>
    /// The index of the selected tab
    /// </Returns>
    public int SelectNext(){
        if(selectedTab == null){
            ClearTab();
            SelectTab(tabs[0]);
            return 0;
        }

        int nextIndex = (GetIndexOfTab(selectedTab)+1) % tabs.Count;
        SelectTab(tabs[nextIndex]);
        return nextIndex;
    }

    /// <Returns>
    /// The index of the selected tab
    /// </Returns>
    public int SelectPrevious(){
        if(selectedTab == null){
            ClearTab();
            SelectTab(tabs[0]);
            return 0;
        }

        int prevIndex = (tabs.Count + GetIndexOfTab(selectedTab)-1) % tabs.Count;
        SelectTab(tabs[prevIndex]);
        return prevIndex;
    }

    public void ClearTab(){
        try{
            foreach(UITab tab in tabs){
                // set all tabs to silhouette
                tab.gameObject.SetActive(true);
            }
            selectedTab = null;
        }
        catch(UnityException ex){
            Debug.Log(ex.StackTrace);
        }

    }


}
