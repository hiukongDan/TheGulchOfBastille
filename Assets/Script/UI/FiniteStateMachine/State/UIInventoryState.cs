using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInventoryState : UIState
{
    protected TabGroup tabGroup;
    protected ViewGroup viewGroup;
    protected PFontText title;
    protected PFontText content;

    protected int selectedIndex = -1;
    protected int currentPage = 0;

    private enum TabSelection{
        Weapon, Wearable, Consumable, KeyItem,
    };

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

        OnClickTab(0);
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
        OnClickTab(tabGroup.SelectPrevious());
    }

    public override void onMenuNext()
    {
        OnClickTab(tabGroup.SelectNext());
    }

    public override void OnClick(UIStateEventData eventData){
        switch(eventData.widgetType){
            case UIState.WidgetType.Tab:
                OnClickTab(eventData.index);
                break;
            case UIState.WidgetType.View:
                OnClickView(eventData.index);
                break;
            default:
                break;
        }

    }

    protected void OnClickTab(int index){
        switch((TabSelection)index){
            case TabSelection.Weapon:
                DisplayWeapon();
                break;
            case TabSelection.Wearable:
                DisplayWearable();
                break;
            case TabSelection.Consumable:
                
                break;
            case TabSelection.KeyItem:
                break;
            default:
                break;
        }
    }

    protected void OnClickView(int index){
        

    }

    protected void DisplayWeapon(){
        viewGroup.ClearViewGroup();
        List<ItemData.WeaponRuntimeData> weapons = uiHandler.GM.player.playerRuntimeData.playerStock.weaponStock;
        int numToDisplay = Mathf.Min(weapons.Count, viewGroup.views.Count);
        for(int i = 0; i < numToDisplay; ++i){
            DisplayWeapon(viewGroup.views[i], weapons[i]);
        }
    }

    protected void DisplayWeapon(UIView view, ItemData.WeaponRuntimeData weaponRuntimeData){
        if(weaponRuntimeData.level > 0){
            view.text.gameObject.SetActive(true);
            view.text.SetText("*"+weaponRuntimeData.level);
        }

        view.image.gameObject.SetActive(true);
        view.image.sprite = UIIconLoader.WeaponIcons[(int)weaponRuntimeData.weapon];
    }

    protected void SelectWeapon(int index){
        if(selectedIndex != index){
            selectedIndex = index;
        }
        
    }

    protected void DisplayWearable(){
        viewGroup.ClearViewGroup();
        List<ItemData.WearableRuntimeData> wearables = uiHandler.GM.player.playerRuntimeData.playerStock.wearableStock;
    }

    protected void SelectWearable(int index){

    }

    protected void DisplayConsumable(){
        viewGroup.ClearViewGroup();
        List<ItemData.ConsumableRuntimeData> weapons = uiHandler.GM.player.playerRuntimeData.playerStock.consumableStock;
    }

    protected void SelectConsumable(int index){

    }

    protected void DisplayKeyItem(){
        viewGroup.ClearViewGroup();
        List<ItemData.KeyItemRuntimeData> keyItems = uiHandler.GM.player.playerRuntimeData.playerStock.keyItemStock;
    }

    protected void SelectKeyItem(int index){

    }
}
