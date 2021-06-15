using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCAnor : NPCConversationHandler
{
    public override void OnEndInteraction()
    {
        // if the first conversation, required abandoned key
        if(npcConversation == npcConversations[0]){
            if(!player.playerRuntimeData.playerStock.weaponStock.Contains(new ItemData.WeaponRuntimeData(ItemData.Weapon.Sunlight_Stick, 0))){
                Loot.OnPickUpLoot(player, ItemData.Weapon.Sunlight_Stick, 0);
            }
        }

        base.OnEndInteraction();
    }
}
