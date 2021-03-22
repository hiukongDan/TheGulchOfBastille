using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInventoryState : UIState
{
    protected TabGroup tabGroup;
    protected ViewGroup viewGroup;
    protected ViewGroup viewCountGroup;
    protected ViewGroup viewSelectGroup;
    protected PFontText title;
    protected PFontText content;
    public UIInventoryState(UIHandler uiHandler, GameObject parentNode): base(uiHandler, parentNode)
    {
        this.tabGroup = parentNode.GetComponentInChildren<TabGroup>();
        this.viewGroup = parentNode.transform.Find("Views").GetComponent<ViewGroup>();
        this.viewCountGroup = parentNode.transform.Find("Views_Count").GetComponent<ViewGroup>();
        this.viewSelectGroup = parentNode.transform.Find("Views_Selection").GetComponent<ViewGroup>();
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
