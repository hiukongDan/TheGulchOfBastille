using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCBelial : NPCConversationHandler
{
    public NPCConversation decayOptionConversation;
    public NPCConversation decayCompulsiveConversation;
    public float cleanseWaitTime = 1f;

    public override void OnBeginInteraction()
    {
        Player player = GameObject.Find("Player").GetComponent<Player>();
        base.OnBeginInteraction();

        if(player.playerRuntimeData.currentDecayPoints == player.playerData.PD_maxDecayPoint){
            decayCompulsiveConversation.ResetIndex();
            npcConversation = decayCompulsiveConversation;
            getNextSentence();
        }
        else if(player.playerRuntimeData.currentDecayPoints > 0){
            decayOptionConversation.ResetIndex();
            npcConversation = decayOptionConversation;
            getNextSentence();
        }
    }

    protected override void confirmSelection()
    {
        if(npcConversation == decayOptionConversation && currentSelection == 0){
            StartCoroutine(CleansePlayer());
        }

        base.confirmSelection();
        // yes
    }

    public override void OnEndInteraction()
    {
        if(npcConversation == decayCompulsiveConversation){
            StartCoroutine(CleansePlayer());
        }
        // if the first conversation, required abandoned key
        else if(npcConversation == npcConversations[0]){
            Loot.OnPickUpLoot(player, ItemData.KeyItem.Abandoned_Door_Key);
        }

        if(npcConversation == decayCompulsiveConversation || npcConversation == decayOptionConversation){
            currentConversationIndex = npcConversations.Count - 1;
        }

        base.OnEndInteraction();
    }

    protected IEnumerator CleansePlayer(){
        // Uilos Required: currentUilos * (currentDecayPoint / currentDecayPointMax)
        player.stateMachine.SwitchState(player.cinemaState);
        int uilosSpent = Mathf.CeilToInt(player.playerRuntimeData.currentUilos * (player.playerRuntimeData.currentDecayPoints / player.playerData.PD_maxDecayPoint));
        player.OnAquireUilos(-uilosSpent);
        player.playerRuntimeData.currentDecayPoints = 0f;

        yield return new WaitForSeconds(gameManager.uiHandler.uiEffectHandler.OnPlayUIEffect(UIEffect.Transition_CrossFadeWhite, UIEffectAnimationClip.start));
        yield return new WaitForSeconds(cleanseWaitTime);
        player.ResetPlayerStatus();
        
        yield return new WaitForSeconds(gameManager.uiHandler.uiEffectHandler.OnPlayUIEffect(UIEffect.Transition_CrossFadeWhite, UIEffectAnimationClip.end));

        player.stateMachine.SwitchState(player.idleState);
    }
}
