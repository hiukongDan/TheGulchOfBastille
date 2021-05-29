using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIEquipmentState : UIState
{
    protected ViewGroup slotViews;
    protected ViewGroup selectionViews;

    protected PFontText title;
    protected PFontText content;

    protected int currentSlot;
    protected int currentSelection;
    protected int currentPage;
    protected int itemsPerPage;
    protected SelectionLayer currentSelectionLayer;

    // private bool isFirstEnter = true;
    private Vector2 normMoveInput;
    private float normMoveInputTimer;
    private float normMoveInputTimerMax = 0.15f;

    protected PlayerRuntimeData.PlayerSlot playerSlot;
    protected PlayerRuntimeData.PlayerStock playerStock;

    protected enum SelectionLayer{
        SlotSelection, ItemSelection,
    };

    public UIEquipmentState(UIHandler uiHandler, GameObject parentNode): base(uiHandler, parentNode)
    {
        slotViews = parentNode.transform.Find("Slot_Views").GetComponent<ViewGroup>();
        selectionViews = parentNode.transform.Find("Views").GetComponent<ViewGroup>();
        title = parentNode.transform.Find("Description/Title").GetComponent<PFontText>();
        content = parentNode.transform.Find("Description/Content").GetComponent<PFontText>();

        currentSelectionLayer = SelectionLayer.SlotSelection;

        itemsPerPage = selectionViews.Row * selectionViews.Column;
    }

    public override void Enter()
    {
        Time.timeScale = 0.0f;
        base.Enter();

        playerSlot = uiHandler.GM.player.playerRuntimeData.playerSlot;
        playerStock = uiHandler.GM.player.playerRuntimeData.playerStock;


        DisplayPlayerSlot();

        SetCurrentLayer(SelectionLayer.SlotSelection);

        title.SetText("");
        content.SetText("");

        currentPage = 0;
        OnClickSlotItem(0, true);

        normMoveInputTimer = -1f;
    }

    public override void Exit()
    {
        // apply any change
        uiHandler.GM.player.playerRuntimeData.playerSlot = playerSlot;
        Time.timeScale = 1.0f;
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if(normMoveInputTimer >= 0){
            normMoveInputTimer -= Time.unscaledDeltaTime;
        }
        if(normMoveInputTimer < 0){
            normMoveInput = uiHandler.GM.player.InputHandler.NormMovementInput;
            bool timerNeedRefresh = false;
            switch(currentSelectionLayer){
                case SelectionLayer.SlotSelection:
                    // left, weapon slot
                    if(currentSlot == 0){
                        // only use right movement
                        if(normMoveInput.x > 0){
                            // change to index=1
                            OnClickSlotItem(1);
                            timerNeedRefresh = true;
                        }
                    }
                    // slot == 1 or slot == 2
                    else if(currentSlot == 1 || currentSlot == 2){
                        if(normMoveInput.x < 0){
                            // change to weapon slot
                            OnClickSlotItem(0);
                            timerNeedRefresh = true;
                        }
                        else if(currentSlot == 1 && normMoveInput.y < 0){
                            OnClickSlotItem(2);
                            timerNeedRefresh = true;
                        }
                        else if(currentSlot == 2 && normMoveInput.y > 0){
                            OnClickSlotItem(1);
                            timerNeedRefresh = true;
                        }
                    }
                break;
                case SelectionLayer.ItemSelection:
                    if(normMoveInput != Vector2.zero){
                        // first consider y axis, then x asix
                        if(normMoveInput.y != 0){
                            if(normMoveInput.y > 0){
                                OnDirectionMove(UIState.Direction.UP);
                            }
                            else{
                                OnDirectionMove(UIState.Direction.DOWN);
                            }
                            timerNeedRefresh = true;
                        }
                        else if(normMoveInput.x != 0){
                            if(normMoveInput.x > 0){
                                OnDirectionMove(UIState.Direction.RIGHT);
                            }
                            else{
                                OnDirectionMove(UIState.Direction.LEFT);
                            }
                            timerNeedRefresh = true;
                        }
                    }
                break;
                default:
                break;
            }
            if(timerNeedRefresh){
                normMoveInputTimer = normMoveInputTimerMax;
            }
        }
    }

    public override void OnDirectionMove(UIState.Direction direction){
        int currentRow = currentSelection / selectionViews.Column;
        int currentColumn = currentSelection % selectionViews.Column;

        int upRow = (currentRow - 1 + selectionViews.Row) % selectionViews.Row;
        int downRow = (currentRow + 1) % selectionViews.Row;
        int leftColumn = (currentColumn - 1 + selectionViews.Column) % selectionViews.Column;
        int rightColumn = (currentColumn + 1) % selectionViews.Column;

        int upIndex = upRow * selectionViews.Column + currentColumn;
        int downIndex = downRow * selectionViews.Column + currentColumn;
        int leftIndex = currentRow * selectionViews.Column + leftColumn;
        int rightIndex = currentRow * selectionViews.Column + rightColumn;

        int totalItems = 0;
        if(currentSlot == 0){
            totalItems = playerStock.weaponStock.Count;
        }
        else if(currentSlot == 1 || currentSlot == 2){
            totalItems = playerStock.wearableStock.Count;
        }

        int currentSelectionPos = currentPage * itemsPerPage + currentSelection;

        switch(direction){
            case UIState.Direction.UP:
                // if needed, refresh page
                if(currentPage > 0 && currentRow == 0){
                    currentPage--;
                    DisplaySelectionView(currentSlot);
                }
                OnClickSelectionItem(upIndex);
                //Debug.Log("move up");
            break;
            case UIState.Direction.LEFT:                
                OnClickSelectionItem(leftIndex);
                //Debug.Log("move left");
            break;
            case UIState.Direction.DOWN:
                if(currentRow == selectionViews.Row-1){
                    if(totalItems > (currentPage+1) * selectionViews.views.Count){
                        currentPage++;
                        DisplaySelectionView(currentSlot);
                    }
                }
                OnClickSelectionItem(downIndex);
                //Debug.Log("move down");
            break;
            case UIState.Direction.RIGHT:
                OnClickSelectionItem(rightIndex);
                //Debug.Log("move right index: " + rightIndex);
            break;
            default:
            break;
        }
    }

    public override void OnInteraction()
    {
        switch(currentSelectionLayer){
            case SelectionLayer.SlotSelection:
                ConfirmSlotSelection();
                break;
            case SelectionLayer.ItemSelection:
                ConfirmItemSelection();
                break;
            default:
            break;
        }
    }

    public override void OnClick(UIStateEventData eventData){
        switch(currentSelectionLayer){
            case SelectionLayer.SlotSelection:
                OnClickSlotItem(eventData.index);
                break;
            case SelectionLayer.ItemSelection:
                OnClickSelectionItem(eventData.index);
                break;
            default:
            break;
        }
    }

    protected void DisplayPlayerSlot(){
        slotViews.ClearViewGroup();
        // There is always a weapon at slot 0
        List<ItemData.WeaponRuntimeData> weapons = playerStock.weaponStock;
        int currentWeapon = playerSlot.weaponIndex;
        ItemData.WeaponRuntimeData weaponData = weapons[currentWeapon];
        UIInventoryState.DisplayWeapon(slotViews.views[0], weaponData);

        // slot one
        List<ItemData.WearableRuntimeData> wearables = playerStock.wearableStock;
        int currentWearableOne = playerSlot.wearableOneIndex;
        int currentWearableTwo = playerSlot.wearableTwoIndex;

        if(playerSlot.IsWearableInUse(playerStock, currentWearableOne)){
            UIInventoryState.DisplayWearable(slotViews.views[1], wearables[currentWearableOne]);
        }
        else{
            slotViews.views[1].image.gameObject.SetActive(true);
            slotViews.views[1].image.sprite = UIIconLoader.EmptySprite;
        }

        if(playerSlot.IsWearableInUse(playerStock, currentWearableTwo)){
            UIInventoryState.DisplayWearable(slotViews.views[2], wearables[currentWearableTwo]);
        }
        else{
            slotViews.views[2].image.gameObject.SetActive(true);
            slotViews.views[2].image.sprite = UIIconLoader.EmptySprite;
        }

        // Debug.Log("weapon: " + weapons[currentWeapon].weapon.ToString());
        // Debug.Log("slot one: " + wearables[currentWearableOne].wearable.ToString());
        // Debug.Log("slot two: " + wearables[currentWearableTwo].wearable.ToString());

        // slot two
    }

    protected void OnClickSlotItem(int index, bool isInit = false){
        if(index == currentSlot && !isInit){
            // repeate click, confirm selection
            ConfirmSlotSelection();
            return;
        }

        // select represented slot
        currentPage = 0;
        DisplaySelectionView(index);
        currentSlot = index;
        slotViews.ClearChosen();
        slotViews.views[currentSlot].Choose();

        // display info
        if(currentSlot == 0){
            ItemData.Weapon item = playerStock.weaponStock[playerSlot.weaponIndex].weapon;
            title.SetText(UIInventoryState.ParsingTitle(item.ToString()));
            content.SetText(ItemData.WeaponDescription[(int)item]);
        }
        else if(currentSlot == 1 && playerSlot.IsWearableInUse(playerStock, playerSlot.wearableOneIndex)){
            ItemData.Wearable item = playerStock.wearableStock[playerSlot.wearableOneIndex].wearable;
            title.SetText(UIInventoryState.ParsingTitle(item.ToString()));
            content.SetText(ItemData.WearableDescription[(int)item]);
        }
        else if(currentSlot == 2 && playerSlot.IsWearableInUse(playerStock, playerSlot.wearableTwoIndex)){
            ItemData.Wearable item = playerStock.wearableStock[playerSlot.wearableTwoIndex].wearable;
            title.SetText(UIInventoryState.ParsingTitle(item.ToString()));
            content.SetText(ItemData.WearableDescription[(int)item]);
        }
        else{
            title.SetText("");
            content.SetText("");
        }
    }

    protected void OnClickSelectionItem(int index, bool isInit = false){
        if(currentSelection == index && !isInit){
            ConfirmItemSelection();
            return;
        }

        selectionViews.ClearChosen();
        currentSelection = index;
        selectionViews.views[currentSelection].Choose();
        int offset = currentPage * itemsPerPage;
        if(currentSlot == 0){
            if(index + offset >= playerStock.weaponStock.Count){
                title.SetText("");
                content.SetText("");
            }
            else{
                ItemData.Weapon item = playerStock.weaponStock[index + offset].weapon;
                title.SetText(UIInventoryState.ParsingTitle(item.ToString()));
                content.SetText(ItemData.WeaponDescription[(int)item]);
            }
        }
        else if(currentSlot == 1 || currentSlot == 2){
            if(index + offset >= playerStock.wearableStock.Count){
                title.SetText("");
                content.SetText("");
            }
            else{
                ItemData.Wearable item = playerStock.wearableStock[index + offset].wearable;
                title.SetText(UIInventoryState.ParsingTitle(item.ToString()));
                content.SetText(ItemData.WearableDescription[(int)item]);
            }
        }
    }

    protected void ConfirmSlotSelection(){
        SetCurrentLayer(SelectionLayer.ItemSelection);
        OnClickSelectionItem(0, true);
    }

    protected void ConfirmItemSelection(){
        int index = currentPage * selectionViews.views.Count + currentSelection;
        if(currentSlot == 0){
            if(index < playerStock.weaponStock.Count){
                // swap weapon
                playerSlot.weaponIndex = index;
            }
        }
        else if(currentSlot == 1 && !playerSlot.IsWearableInUse(playerStock, index)){
            if(index < playerStock.wearableStock.Count){
                playerSlot.wearableOneIndex = index;
            }
        }
        else if(currentSlot == 2 && !playerSlot.IsWearableInUse(playerStock, index)){
            if(index < playerStock.wearableStock.Count){
                playerSlot.wearableTwoIndex = index;
            }
        }

        DisplayPlayerSlot();

        // just another hack in my code :)
        OnClickSlotItem(currentSlot, true);
        SetCurrentLayer(SelectionLayer.SlotSelection);
    }

    protected void DisplaySelectionView(int slotIndex){
        int offset = currentPage * itemsPerPage;
        if(slotIndex == 0){
            // display weapon selections
            int itemsToDisplay = Mathf.Min(playerStock.weaponStock.Count - currentPage * itemsPerPage, itemsPerPage);
            selectionViews.ClearViewGroup();
            for(int i = 0; i < itemsToDisplay; ++i){
                UIInventoryState.DisplayWeapon(selectionViews.views[i], playerStock.weaponStock[offset+i]);
            }
        }
        else{
            // display wearable selections
            int itemsToDisplay = Mathf.Min(playerStock.wearableStock.Count - currentPage * itemsPerPage, itemsPerPage);
            selectionViews.ClearViewGroup();
            for(int i = 0; i < itemsToDisplay; ++i){
                UIInventoryState.DisplayWearable(selectionViews.views[i], playerStock.wearableStock[offset+i]);
            }
        }
    }

    protected void SetCurrentLayer(SelectionLayer layer){
        currentSelectionLayer = layer;
        switch(layer){
            case SelectionLayer.SlotSelection:
                slotViews.SetClickable(true);
                selectionViews.SetClickable(false);
                break;
            case SelectionLayer.ItemSelection:
                slotViews.SetClickable(false);
                selectionViews.SetClickable(true);
                break;
            default:
            break;
        }
    }
}
