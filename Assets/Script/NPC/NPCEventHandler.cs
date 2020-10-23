using System;
using UnityEngine;

public class NPCEventHandler : MonoBehaviour
{
    public event Action NPCInteraction;
    public event Action NPCEndInteraction;
    public event Action NPCEnterInteraction;
    public event Action NPCExitInteraction;
    public event SelectionHandler NPCSelection;

    private NPC npc;

    public delegate void SelectionHandler(int direction);

    void Start()
    {
        NPCInteraction += npcIntearctionHandler;
        NPCEnterInteraction += npcEnterInteractionHandler;

        npc = GetComponentInParent<NPC>();
    }

    #region HANDLER
    private void npcIntearctionHandler()
    {
        NPCInteraction -= npcIntearctionHandler;

        npc.npcConversationHandler.OnBeginInteraction();

        NPCInteraction += npc.npcConversationHandler.OnInteraction;

        NPCEndInteraction += npc.npcConversationHandler.OnEndInteraction;
        NPCEndInteraction += npcEndInteractionHandler;
    }

    private void npcEndInteractionHandler()
    {
        NPCEndInteraction -= npcEndInteractionHandler;
        NPCEndInteraction -= npc.npcConversationHandler.OnInteraction;
        NPCEndInteraction -= npc.npcConversationHandler.OnEndInteraction;

        NPCInteraction -= npc.npcConversationHandler.OnInteraction;

        NPCInteraction += npcIntearctionHandler;

        var player = GameObject.Find("Player").GetComponent<Player>();
        player.stateMachine.SwitchState(player.idleState);
    }

    private void npcEnterInteractionHandler()
    {
        NPCExitInteraction += npcExitInteractionHandler;
        
        npc.npcConversationHandler.OnEnterInteractionArea();
    }

    private void npcExitInteractionHandler()
    {
        NPCExitInteraction -= npcExitInteractionHandler;

        npc.npcConversationHandler.OnExitInteractionArea();
    }

    #endregion



    #region PUBLIC INTERFACE    
    public void OnNPCInteraction()
    {
        NPCInteraction?.Invoke();
    }

    public void OnNPCEndInteraction()
    {
        NPCEndInteraction?.Invoke();
    }

    public void OnNPCEnterInteraction()
    {
        NPCEnterInteraction?.Invoke();
    }

    public void OnNPCExitInteraction()
    {
        NPCExitInteraction?.Invoke();
    }

    public void OnNPCSelection(int direction)
    {
        NPCSelection?.Invoke(direction);
    }


    #endregion
}
