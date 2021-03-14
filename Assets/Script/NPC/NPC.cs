using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public NPCEventHandler npcEventHandler { get; private set; }
    public NPCPlayerDetect npcPlayerDetect { get; private set; }
    public NPCConversationHandler npcConversationHandler { get; private set; }

    void Awake()
    {
        npcEventHandler = GetComponentInChildren<NPCEventHandler>();
        npcPlayerDetect = GetComponentInChildren<NPCPlayerDetect>();
        npcConversationHandler = GetComponentInChildren<NPCConversationHandler>();

        npcConversationHandler.gameObject.SetActive(false);
    }
}
