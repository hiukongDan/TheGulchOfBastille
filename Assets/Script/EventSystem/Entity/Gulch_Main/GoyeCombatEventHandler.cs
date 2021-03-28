using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoyeCombatEventHandler : EntityEventHandler
{

    private GC1_CombatTrigger combatTrigger;
    private GoyeCombat1 goyeCombat;

    private void Awake(){
        combatTrigger = transform.parent.GetComponentInChildren<GC1_CombatTrigger>();
        goyeCombat = GetComponentInParent<GoyeCombat1>();
        Gulch.GameEventListener.Instance.OnPlayerDeadHandler += OnPlayerDead;
    }
    private void OnEnable() {
        if(goyeCombat.GetComponent<EnemySaveData>().IsAlive()){
            combatTrigger.enabled = true;
            combatTrigger.ResetTrigger();
        }
    }

    public override void OnDead()
    {
        GoyeCombat1 goye = transform.parent.GetComponent<GoyeCombat1>();
        // Destroy(goye.transform.Find("Combat Field").gameObject);
        goye.transform.Find("Combat Field").gameObject.SetActive(false);
        Camera.main.GetComponent<BasicFollower>().RestoreCameraFollowing();
        goye.GetComponent<EnemySaveData>().Save(false);
        Invoke("ResetDialogue", 1f);
    }

    public void OnPlayerDead(){
        if(goyeCombat.refPlayer.miscData.conversationIndex.ContainsKey(goyeCombat.npc.npcConversationHandler.GetHashCode())){
            goyeCombat.refPlayer.miscData.conversationIndex[goyeCombat.npc.npcConversationHandler.GetHashCode()] = 0;
        }
        else{
            goyeCombat.refPlayer.miscData.conversationIndex.Add(goyeCombat.npc.npcConversationHandler.GetHashCode(),0);
        }
    }

    private void ResetDialogue(){
        goyeCombat.npc.gameObject.SetActive(true);
        goyeCombat.npc.transform.position = goyeCombat.aliveGO.transform.position;
    }
}
