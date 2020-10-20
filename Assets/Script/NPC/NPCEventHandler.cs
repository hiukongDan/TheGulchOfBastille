using System;
using UnityEngine;

public class NPCEventHandler : MonoBehaviour
{
    public event Action NPCInteraction;
    public event Action NPCEnterInteraction;
    public event Action NPCExitInteraction;

    private NPC npc;

    void Start()
    {
        NPCInteraction += npcIntearctionHandler;
        NPCEnterInteraction += npcEnterInteractionHandler;

        npc = GetComponentInParent<NPC>();
    }

    #region HANDLER
    private void npcIntearctionHandler()
    {
        NPCExitInteraction -= npcExitInteractionHandler;
        NPCInteraction -= npcIntearctionHandler;

        npc.npcConversationHandler.OnBeginInteraction();

        NPCInteraction += npc.npcConversationHandler.OnInteraction;
    }

    private void npcEnterInteractionHandler()
    {
        NPCExitInteraction += npcExitInteractionHandler;

        npc.npcConversationHandler.OnEnterInteractionArea();
    }

    private void npcExitInteractionHandler()
    {
        npc.npcConversationHandler.OnExitInteractionArea();
    }
    #endregion



    #region PUBLIC INTERFACE    
    public void OnNPCInteraction()
    {
        NPCInteraction?.Invoke();
    }

    public void OnNPCEnterInteraction()
    {
        NPCEnterInteraction?.Invoke();
    }

    public void OnNPCExitInteraction()
    {
        NPCExitInteraction?.Invoke();
    }

    #endregion
}
