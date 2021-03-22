using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInventoryState : UIState
{
    protected TabGroup tabGroup;
    protected ViewGroup viewGroup;
    protected PFontText title;
    protected PFontText content;

    protected TabSelection currentTab = TabSelection.Weapon;
    protected int selectedViewIndex = -1;
    protected int currentViewPage = 0;

    private bool isFirstEnter = true;

    private Vector2 normMoveInput;
    private float normMoveInputTimer;
    private float normMoveInputTimerMax = 0.3f;

    protected enum TabSelection{
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
        Time.timeScale = 0.0f;

        if(isFirstEnter){
            isFirstEnter = false;

            currentTab = TabSelection.Weapon;
            currentViewPage = 0;
            // TODO: selection
            DisplayWeapon();
            tabGroup.SelectTab(tabGroup.tabs[0]);

            selectedViewIndex = -1;

            title.SetText("");
            content.SetText("");
        }
        else{
            tabGroup.SelectTab(tabGroup.tabs[(int)currentTab]);
            OnClickTab((int)currentTab);
        }

        normMoveInputTimer = -1f;
    }

    public override void Exit()
    {
        base.Exit();
        Time.timeScale = 1.0f;
    }

    public override void Update()
    {
        base.Update();
        if(normMoveInputTimer >= 0.0f){
            normMoveInputTimer -= Time.unscaledDeltaTime;
        }

        if(normMoveInputTimer < 0.0f && normMoveInput != Vector2.zero){
            // first consider y axis, then x asix
            if(normMoveInput.x != 0){
                if(normMoveInput.x > 0){
                    OnDirectionMove(UIState.Direction.RIGHT);
                }
                else{
                    OnDirectionMove(UIState.Direction.LEFT);
                }
                normMoveInputTimer = normMoveInputTimerMax;
            }
            else if(normMoveInput.y != 0){
                if(normMoveInput.y > 0){
                    OnDirectionMove(UIState.Direction.UP);
                }
                else{
                    OnDirectionMove(UIState.Direction.DOWN);
                }
                normMoveInputTimer = normMoveInputTimerMax;
            }
        }
        normMoveInput = uiHandler.GM.player.InputHandler.NormMovementInput;
    }
    public override void OnInteraction()
    {
        
    }

    public override void OnDirectionMove(UIState.Direction direction){
        switch(direction){
            case UIState.Direction.UP:
            break;
            case UIState.Direction.LEFT:
            break;
            case UIState.Direction.DOWN:
            break;
            case UIState.Direction.RIGHT:
            break;
            default:
            break;
        }
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
        currentTab = (TabSelection)index;

        switch(currentTab){
            case TabSelection.Weapon:
                DisplayWeapon();
                break;
            case TabSelection.Wearable:
                DisplayWearable();
                break;
            case TabSelection.Consumable:
                DisplayConsumable();
                break;
            case TabSelection.KeyItem:
                DisplayKeyItem();
                break;
            default:
                break;
        }
    }

    protected void OnClickView(int index){
        if(selectedViewIndex != index){
            selectedViewIndex = index;
        }
        viewGroup.ClearChosen();
        viewGroup.views[index].chosen.SetActive(true);
        title.SetText("");
        content.SetText("");
        
        switch((TabSelection)currentTab){
            case TabSelection.Weapon:
                SelectWeapon(index);
                break;
            case TabSelection.Wearable:
                SelectWearable(index);
                break;
            case TabSelection.Consumable:
                SelectConsumable(index);
                break;
            case TabSelection.KeyItem:
                SelectKeyItem(index);
                break;
            default:
                break;
        }
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
        List<ItemData.WeaponRuntimeData> stock = uiHandler.GM.player.playerRuntimeData.playerStock.weaponStock;
        index = currentViewPage*viewGroup.views.Count + index;
        if(index < stock.Count){
            var data = stock[index];
            title.SetText(ParsingTitle(data.weapon.ToString()));
            content.SetText(ItemData.WeaponDescription[(int)data.weapon]);
        }
    }

    protected void DisplayWearable(){
        viewGroup.ClearViewGroup();
        List<ItemData.WearableRuntimeData> wearables = uiHandler.GM.player.playerRuntimeData.playerStock.wearableStock;
        int numToDisplay = Mathf.Min(wearables.Count, viewGroup.views.Count);
        for(int i = 0; i < numToDisplay; ++i){
            DisplayWearable(viewGroup.views[i], wearables[i]);
        }
    }

    protected void DisplayWearable(UIView view, ItemData.WearableRuntimeData wearableRuntimeData){
        view.image.gameObject.SetActive(true);
        view.image.sprite = UIIconLoader.WearableIcons[(int)wearableRuntimeData.wearable];
    }

    protected void SelectWearable(int index){
        List<ItemData.WearableRuntimeData> stock = uiHandler.GM.player.playerRuntimeData.playerStock.wearableStock;
        index = currentViewPage*viewGroup.views.Count + index;
        if(index < stock.Count){
            var data = stock[index];
            title.SetText(ParsingTitle(data.wearable.ToString()));
            content.SetText(ItemData.WearableDescription[(int)data.wearable]);
        }
    }

    protected void DisplayConsumable(){
        viewGroup.ClearViewGroup();
        List<ItemData.ConsumableRuntimeData> consumables = uiHandler.GM.player.playerRuntimeData.playerStock.consumableStock;
        int numToDisplay = Mathf.Min(consumables.Count, viewGroup.views.Count);
        for(int i = 0; i < numToDisplay; ++i){
            DisplayConsumable(viewGroup.views[i], consumables[i]);
        }
    }

    protected void DisplayConsumable(UIView view, ItemData.ConsumableRuntimeData consumableRuntimeData){
        view.image.gameObject.SetActive(true);
        view.image.sprite = UIIconLoader.ConsumableIcons[(int)consumableRuntimeData.consumable];
        view.text.gameObject.SetActive(true);
        view.text.SetText(consumableRuntimeData.count.ToString());
    }

    protected void SelectConsumable(int index){
        List<ItemData.ConsumableRuntimeData> stock = uiHandler.GM.player.playerRuntimeData.playerStock.consumableStock;
        index = currentViewPage*viewGroup.views.Count + index;
        if(index < stock.Count){
            var data = stock[index];
            title.SetText(ParsingTitle(data.consumable.ToString()));
            content.SetText(ItemData.ConsumableDescription[(int)data.consumable]);
        }
    }

    protected void DisplayKeyItem(){
        viewGroup.ClearViewGroup();
        List<ItemData.KeyItemRuntimeData> keyItems = uiHandler.GM.player.playerRuntimeData.playerStock.keyItemStock;
        int numToDisplay = Mathf.Min(keyItems.Count, viewGroup.views.Count);
        for(int i = 0; i < numToDisplay; ++i){
            DisplayKeyItem(viewGroup.views[i], keyItems[i]);
        }
    }

    protected void DisplayKeyItem(UIView view, ItemData.KeyItemRuntimeData keyItemRuntimeData){
        view.image.gameObject.SetActive(true);
        view.image.sprite = UIIconLoader.KeyItemIcons[(int)keyItemRuntimeData.keyItem];
    }

    protected void SelectKeyItem(int index){
        List<ItemData.KeyItemRuntimeData> stock = uiHandler.GM.player.playerRuntimeData.playerStock.keyItemStock;
        index = currentViewPage*viewGroup.views.Count + index;
        if(index < stock.Count){
            var data = stock[index];
            title.SetText(ParsingTitle(data.keyItem.ToString()));
            content.SetText(ItemData.KeyItemDescription[(int)data.keyItem]);
        }
    }

    protected string ParsingTitle(string str){
        string[] words = str.Split('_');
        // int count = 0;
        // string res = "";
        // foreach(string word in words){
        //     count++;
        //     res += word + (count<2?" ":"\n");
        //     count %= 2;
        // }
        // res = res.TrimEnd();
        // return res;
        return string.Join(" ", words);
    }
}
