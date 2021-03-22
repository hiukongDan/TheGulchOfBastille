using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInventoryState : UIState
{
    protected TabGroup tabGroup;
    protected ViewGroup viewGroup;
    protected PFontText title;
    protected PFontText content;
    public UIInventoryState(UIHandler uiHandler, GameObject parentNode): base(uiHandler, parentNode)
    {
        this.tabGroup = parentNode.GetComponentInChildren<TabGroup>();
        this.viewGroup = parentNode.transform.Find("Views").GetComponent<ViewGroup>();
        Transform description = parentNode.transform.Find("Description");
        title = description.Find("Title").GetComponent<PFontText>();
        content = description.Find("Content").GetComponent<PFontText>();
    
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
        
    }

    public override void OnMenuPrev()
    {
        tabGroup.SelectPrevious();
    }

    public override void onMenuNext()
    {
        tabGroup.SelectNext();
    }

    public override void OnClick(UIStateEventData eventData){
        switch(eventData.widgetType){
            case UIState.WidgetType.Tab:

                break;
            case UIState.WidgetType.View:
                break;
            default:
                break;
        }

    }

    protected void OnClickTab(int index){

    }

    protected void OnClickView(int index){
        
    }
}
