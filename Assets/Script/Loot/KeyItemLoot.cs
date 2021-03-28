using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyItemLoot : Loot
{
    public ItemData.KeyItem keyItem;
    public override void OnPickUpLoot(Player player)
    {
        base.OnPickUpLoot(player);
        OnPickUpLoot(player, keyItem);
    }
}
