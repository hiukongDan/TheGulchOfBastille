using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyItemLoot : Loot
{
    public ItemData.KeyItem keyItem;
    public override void OnPickUpLoot(Player player)
    {
        base.OnPickUpLoot(player);
        player.playerRuntimeData.playerStock.Pick(new ItemData.KeyItemRuntimeData(keyItem));
        string info = "Pick up " + string.Join(" ", keyItem.ToString().Split('_'));
        UIEventListener.Instance.OnInfomationChange(new UIEventListener.InfomationChangeData(info));
    }
}
